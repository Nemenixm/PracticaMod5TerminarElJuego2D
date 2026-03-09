using UnityEngine;
using UnityEngine.InputSystem;

public class item : MonoBehaviour
{
    #region Fields

    public enum ItemType
    {
        VidaMas,
        VidaMenos,
        Power,
        TP
    }

    public ItemType tipo;
    public Transform tpStartPoint;

    [Header("Ajustes de Velocidad")]
    public float fallSpeed = 3f;

    private Rigidbody2D rb;

    #endregion

    #region Properties
    #endregion

    #region Unity Callbacks

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0f;
        }
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0f, -fallSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("ZonaSegura"))
            {
                return;
            }

            ApplyEffect(other.gameObject);
            Destroy(gameObject);
        }

        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Public Methods
    #endregion

    #region Private Methods

    private void ApplyEffect(GameObject player)
    {
        playerHealth health = player.GetComponent<playerHealth>();
        playerController controller = player.GetComponent<playerController>();

        switch (tipo)
        {
            case ItemType.VidaMas:
                if (health != null)
                {
                    health.TakeDamage(-20);
                }
                else
                {
                    Debug.LogWarning("El jugador no tiene el componente playerHealth");
                }
                break;

            case ItemType.VidaMenos:
                if (controller != null)
                {
                    controller.TakeDamage(30);
                }
                else
                {
                    Debug.LogWarning("El jugador no tiene el componente playerController");
                }
                break;

            case ItemType.Power:
                if (controller != null)
                {
                    controller.AddResist(0.5f);
                }
                break;

            case ItemType.TP:
                if (player != null)
                {
                    player.transform.position = tpStartPoint != null ? tpStartPoint.position : Vector3.zero;
                }
                break;
        }
    }

    #endregion
}
