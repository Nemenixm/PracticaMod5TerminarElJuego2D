using UnityEngine;
using UnityEngine.InputSystem;

public class fireBall : MonoBehaviour
{
public float speed = 12f;
    public float damage = 25f;
    public float tiempoDeVida = 3f;

    void Start() {
        Destroy(gameObject, tiempoDeVida);
    }

    void Update() {
        // Se mueve hacia donde apunte su eje derecho (X)
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Enemy")) {
            skeleton enemigo = collision.GetComponent<skeleton>();
            if (enemigo != null) {
                enemigo.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        
        // Destruir si toca paredes o suelo
        if (collision.gameObject.layer == LayerMask.NameToLayer("Default")) {
            Destroy(gameObject);
        }
    }
}