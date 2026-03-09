using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class playerController : MonoBehaviour
{
    [Header("Estadísticas de Vida")]
    public float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [Header("Ataque")]
    public GameObject fireballPrefab;
    public Transform firePoint;

    [Header("Movimiento")]
    public float moveSpeed = 4f;
    public float jumpForce = 12f;

    [Header("Sistema de Vuelo")]
    public float maxFlyTime = 2.0f;
    public float flyCooldown = 2.0f;
    public float flyForce = 10f;
    private float currentFlyTime;
    private float cooldownTimer;
    private bool isInputFlying = false;
    private bool canFly = true;
    private bool jumpRequested = false;

    [Header("Interfaz")]
    public Slider flySlider;

    [Header("Panel de Pausa")]
    public GameObject panelPausa;
    private bool estaPausado = false;

    [Header("Detección de suelo")]
    public Transform groundCheck;
    public Vector2 boxSize = new Vector2(0.6f, 0.2f);
    public LayerMask groundLayer;

    [Header("Componentes")]
    public Animator animator;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool grounded;
    private bool dead = false;

    private AudioSettingsController audioControl;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (!animator)
            animator = GetComponentInChildren<Animator>();

        currentHealth = maxHealth;
        currentFlyTime = maxFlyTime;

        if (flySlider != null)
        {
            flySlider.maxValue = maxFlyTime;
            flySlider.value = maxFlyTime;
        }

        if (panelPausa != null)
            panelPausa.SetActive(false);

        audioControl = Object.FindFirstObjectByType<AudioSettingsController>();
    }

    void Update()
    {
        if (dead) return;

        bool wasGrounded = grounded;
        grounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0f, groundLayer);

        if (grounded && !wasGrounded)
        {
            if (audioControl != null)
                audioControl.PlaySuelo();
        }

        bool estaVolandoAhora = !grounded && isInputFlying && canFly && currentFlyTime > 0f && !estaPausado;

        if (audioControl != null)
        {
            if (estaVolandoAhora)
                audioControl.StartFly();
            else
                audioControl.StopFly();
        }

        HandleFlightLogic();
        UpdateAnimations();

        if (flySlider != null)
            flySlider.value = currentFlyTime;

        if (moveInput.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
    }

    void FixedUpdate()
    {
        if (dead || estaPausado) return;

        float horizontal = moveInput.x * moveSpeed;
        float vertical = rb.linearVelocity.y;

        // Ejecuta el salto dentro de la física para que no se pise con la velocidad del frame
        if (jumpRequested)
        {
            vertical = jumpForce;
            jumpRequested = false;
        }

        // Evita arrastrar una pequeña caída residual cuando ya estás en el suelo
        if (grounded && !isInputFlying && vertical < 0f)
        {
            vertical = 0f;
        }

        // SOLO vuela en el aire mientras mantienes pulsado
        if (!grounded && isInputFlying && canFly && currentFlyTime > 0f)
        {
            vertical = flyForce;
            currentFlyTime -= Time.fixedDeltaTime;

            if (currentFlyTime <= 0f)
                StartCooldown();
        }

        rb.linearVelocity = new Vector2(horizontal, vertical);
    }

    private void HandleFlightLogic()
    {
        if (estaPausado) return;

        if (!canFly)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f)
            {
                canFly = true;
                currentFlyTime = maxFlyTime;
            }
        }
        else if (!isInputFlying && grounded && currentFlyTime < maxFlyTime)
        {
            currentFlyTime = Mathf.MoveTowards(currentFlyTime, maxFlyTime, Time.deltaTime);
        }
    }

    private void UpdateAnimations()
    {
        if (animator == null) return;

        animator.SetBool("isWalking", Mathf.Abs(moveInput.x) > 0.1f && grounded && !estaPausado);
        animator.SetBool("isFlying", !grounded && !estaPausado);
    }

    private void StartCooldown()
    {
        canFly = false;
        cooldownTimer = flyCooldown;
        isInputFlying = false;
    }

    public void OnMove(InputValue value)
    {
        if (dead || estaPausado) return;
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (dead || estaPausado) return;

        isInputFlying = value.isPressed;

        // Salto normal solo al pulsar en suelo
        if (value.isPressed && grounded && canFly)
        {
            jumpRequested = true;

            if (animator != null)
                animator.SetTrigger("jump");
        }
    }

    public void OnAttack()
    {
        if (dead || estaPausado) return;

        if (audioControl != null)
            audioControl.PlayAtaque();

        if (animator != null)
            animator.SetTrigger("attack");

        if (fireballPrefab != null && firePoint != null)
        {
            GameObject bola = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);

            if (transform.localScale.x < 0)
                bola.transform.Rotate(0, 180, 0);
        }
    }

    public void OnPause(InputValue value)
    {
        if (dead) return;
        if (!value.isPressed) return;

        TogglePause();
    }

    public void TogglePause()
    {
        estaPausado = !estaPausado;

        if (panelPausa != null)
            panelPausa.SetActive(estaPausado);

        if (estaPausado)
        {
            Time.timeScale = 0f;
            moveInput = Vector2.zero;
            isInputFlying = false;
            jumpRequested = false;
            rb.linearVelocity = Vector2.zero;

            if (audioControl != null)
                audioControl.StopFly();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void BotonReanudar()
    {
        if (estaPausado)
            TogglePause();
    }

    public void TakeDamage(float damage)
    {
        if (dead) return;

        playerHealth healthScript = GetComponent<playerHealth>();
        if (healthScript != null)
            healthScript.TakeDamage((int)damage);

        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
        else if (animator != null)
            animator.SetTrigger("hurt");
    }

    private void Die()
    {
        dead = true;
        estaPausado = false;
        Time.timeScale = 1f;
        isInputFlying = false;
        jumpRequested = false;

        if (audioControl != null)
            audioControl.StopFly();

        if (panelPausa != null)
            panelPausa.SetActive(false);

        if (animator != null)
        {
            animator.SetBool("isDead", true);
            animator.SetTrigger("isDead");
        }

        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(RestartAfterDelay());
    }

    private IEnumerator RestartAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddResist(float amount)
    {
        currentFlyTime = Mathf.Clamp(currentFlyTime + amount, 0, maxFlyTime);

        if (flySlider != null)
            flySlider.value = currentFlyTime;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, boxSize);
    }
}