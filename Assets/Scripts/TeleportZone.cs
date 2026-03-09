using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportZone : MonoBehaviour
{
   public Transform targetDestination; // Arrastra aquí el objeto vacío de la zona verde

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Lo movemos a la zona verde
            other.transform.position = targetDestination.position;

            // 2. Le quitamos toda la velocidad para que no siga "cayendo"
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero; 
            }
        }
    }
}