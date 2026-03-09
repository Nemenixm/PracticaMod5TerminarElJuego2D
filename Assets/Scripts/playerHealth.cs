using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    #region Fields

    [Header("Vida")]
    public int maxHealth = 100;
    public int currentHealth = 100;

    [Header("UI")]
    public Slider vidaSlider;

    [Header("Daño por caída")]
    public float fallHeightThreshold = 6f;
    public int fallDamage = 5;

    [Header("Ground Check (2D)")]
    public Transform groundCheck;
    public float groundRadius = 0.15f;
    public LayerMask groundLayer;

    private float highestY;
    private bool wasGrounded;

    #endregion

    #region Properties
    #endregion

    #region Unity Callbacks

    private void Start()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (vidaSlider != null)
        {
            vidaSlider.maxValue = maxHealth;
            vidaSlider.value = currentHealth;
        }

        highestY = transform.position.y;
    }

    private void Update()
    {
        bool grounded = IsGrounded2D();

        if (!grounded)
        {
            highestY = Mathf.Max(highestY, transform.position.y);
        }

        if (!wasGrounded && grounded)
        {
            float landedY = transform.position.y;
            float fallDistance = highestY - landedY;

            if (fallDistance >= fallHeightThreshold)
            {
                TakeDamage(fallDamage);
            }

            highestY = transform.position.y;
        }

        if (grounded)
        {
            highestY = transform.position.y;
        }

        wasGrounded = grounded;

        if (Keyboard.current != null && Keyboard.current.kKey.wasPressedThisFrame)
        {
            TakeDamage(20);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    #endregion

    #region Public Methods

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);

        if (vidaSlider != null)
        {
            vidaSlider.value = currentHealth;
        }

        if (currentHealth == 0)
        {
            Die();
        }
    }

    #endregion

    #region Private Methods

    private bool IsGrounded2D()
    {
        if (groundCheck == null)
        {
            return false;
        }

        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    private void Die()
    {
        Debug.Log("Player murió");
    }

    #endregion
}