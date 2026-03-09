using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportZone : MonoBehaviour
{
    #region Fields

    public Transform targetDestination;

    #endregion

    #region Properties
    #endregion

    #region Unity Callbacks

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = targetDestination.position;

            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}
