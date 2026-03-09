using UnityEngine;
using UnityEngine.InputSystem;

public class ZonaSegura : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.layer = LayerMask.NameToLayer("ZonaSegura");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Vuelve a la capa por defecto (asegúrate de que tu Player sea "Default")
            other.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}