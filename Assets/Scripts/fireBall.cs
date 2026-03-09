using UnityEngine;
using UnityEngine.InputSystem;

public class fireBall : MonoBehaviour
{
    #region Fields

    public float speed = 12f;
    public float damage = 25f;
    public float tiempoDeVida = 3f;

    #endregion

    #region Properties
    #endregion

    #region Unity Callbacks

    private void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            skeleton enemigo = collision.GetComponent<skeleton>();

            if (enemigo != null)
            {
                enemigo.TakeDamage(damage);
            }

            Destroy(gameObject);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}
