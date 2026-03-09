using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class playerHealth : MonoBehaviour
{
    [Header("Vida")]
    public int maxHealth = 100;
    public int currentHealth = 100;

    [Header("UI")]
    public Slider vidaSlider;

    [Header("Daño por caída")]
    public float fallHeightThreshold = 6f;
    public int fallDamage = 5;

    private float highestY;
    private bool wasGrounded;

    // Si ya tienes un script de ground check, puedes reemplazar esto por tu variable grounded.
    [Header("Ground Check (2D)")]
    public Transform groundCheck;
    public float groundRadius = 0.15f;
    public LayerMask groundLayer;

    void Start()
    {
        // Si quieres que arranque con lo que pusiste en el Inspector, no lo piso.
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        vidaSlider.maxValue = maxHealth;
        vidaSlider.value = currentHealth;

        highestY = transform.position.y;
    }

    void Update()
    {
        bool grounded = IsGrounded2D();

        if (!grounded)
            highestY = Mathf.Max(highestY, transform.position.y);

        if (!wasGrounded && grounded)
        {
            float landedY = transform.position.y;
            float fallDistance = highestY - landedY;

            if (fallDistance >= fallHeightThreshold)
                TakeDamage(fallDamage);

            highestY = transform.position.y;
        }

        if (grounded)
            highestY = transform.position.y;

        wasGrounded = grounded;

        // TEST rápido: aprieta K y mira si baja la barra
       if (Keyboard.current != null && Keyboard.current.kKey.wasPressedThisFrame)
        TakeDamage(20);
    }

    bool IsGrounded2D()
    {
        if (!groundCheck) return false;
        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        if (vidaSlider) vidaSlider.value = currentHealth;

        if (currentHealth == 0) Die();
    }

    void Die()
    {
        Debug.Log("Player murió 💀");
    }

    void OnDrawGizmosSelected()
    {
        if (!groundCheck) return;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}