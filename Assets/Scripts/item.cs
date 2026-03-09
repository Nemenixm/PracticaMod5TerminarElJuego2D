using UnityEngine;
using UnityEngine.InputSystem;

public class item : MonoBehaviour
{
 
    public enum ItemType { VidaMas, VidaMenos, Power, TP }
    public ItemType tipo;
    public Transform tpStartPoint; // Solo para el ítem TP

    [Header("Ajustes de Velocidad")]
    public float fallSpeed = 3f; // La velocidad será constante
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Ponemos la gravedad en 0 para que no acelere al caer
        if (rb != null) rb.gravityScale = 0; 
    }

    private void FixedUpdate()
    {
        // Aplicamos velocidad constante hacia abajo
        if (rb != null) rb.linearVelocity = new Vector2(0, -fallSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // SI ESTÁ EN ZONA SEGURA, NO HACE NADA (Añadido para evitar TP)
            if (other.gameObject.layer == LayerMask.NameToLayer("ZonaSegura"))
            {
                return; 
            }

            ApplyEffect(other.gameObject);
            Destroy(gameObject);
        }
        
        // Se destruye al tocar el suelo de abajo (Tag: Ground)
        if (other.CompareTag("Ground")) Destroy(gameObject);
    }

    void ApplyEffect(GameObject player)
{
    playerHealth health = player.GetComponent<playerHealth>();
    playerController controller = player.GetComponent<playerController>();

    switch (tipo)
    {
        case ItemType.VidaMas: 
            if (health != null) health.TakeDamage(-20); 
            else Debug.LogWarning("El jugador no tiene el componente playerHealth");
            break;

        case ItemType.VidaMenos: 
            if (controller != null) controller.TakeDamage(30); 
            else Debug.LogWarning("El jugador no tiene el componente playerController");
            break; 

        case ItemType.Power: 
            if (controller != null) controller.AddResist(0.5f); 
            break; 

        case ItemType.TP: 
            // Verificamos que el objeto player no sea nulo (por seguridad extra)
            if (player != null) {
                player.transform.position = tpStartPoint != null ? tpStartPoint.position : new Vector3(0,0,0); 
            }
            break;
    }
}
}