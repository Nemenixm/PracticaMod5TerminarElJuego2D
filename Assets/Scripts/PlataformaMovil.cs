using UnityEngine;

public class PlataformaPatrulla : MonoBehaviour
{
    #region Fields

    [Header("Configuración de Movimiento")]
    public bool activo = false;
    public float velocidad = 3f;
    public float distanciaMovimiento = 5f;
    public bool moverEnX = true;

    private Vector3 posicionInicial;
    private int direccion = 1;

    #endregion

    #region Properties
    #endregion

    #region Unity Callbacks

    private void Start()
    {
        posicionInicial = transform.position;
    }

    private void Update()
    {
        if (!activo)
        {
            return;
        }

        float limiteDerecho = posicionInicial.x + distanciaMovimiento;
        float limiteIzquierdo = posicionInicial.x - distanciaMovimiento;

        transform.Translate(Vector2.right * direccion * velocidad * Time.deltaTime);

        if (transform.position.x >= limiteDerecho)
        {
            direccion = -1;
        }
        else if (transform.position.x <= limiteIzquierdo)
        {
            direccion = 1;
        }
    }

    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}
