using UnityEngine;

public class PlataformaPatrulla : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public bool activo = false; // Solo se mueve si marcas esto en el nivel
    public float velocidad = 3f;
    public float distanciaMovimiento = 5f; // Cuánto se mueve desde el inicio
    public bool moverEnX = true; // Si es falso, se moverá en Y

    private Vector3 posicionInicial;
    private int direccion = 1;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        if (!activo) return;

        // Calculamos el límite basado en la posición inicial
        float limiteDerecho = posicionInicial.x + distanciaMovimiento;
        float limiteIzquierdo = posicionInicial.x - distanciaMovimiento;

        // Movimiento simple
        transform.Translate(Vector2.right * direccion * velocidad * Time.deltaTime);

        // Comprobar si alcanzó los límites para dar la vuelta
        if (transform.position.x >= limiteDerecho)
        {
            direccion = -1;
        }
        else if (transform.position.x <= limiteIzquierdo)
        {
            direccion = 1;
        }
    }
}