using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    #region Fields

    [Header("Referencias")]
    public Light2D globalLight;
    public Camera mainCamera;

    [Header("Configuración")]
    public float velocidadCambio = 2f;

    private float intensidadObjetivo = 1f;
    private Color colorOriginalFondo;
    private bool zonaOscuraActiva = false;

    #endregion

    #region Properties
    #endregion

    #region Unity Callbacks

    private void Start()
    {
        if (mainCamera != null)
        {
            colorOriginalFondo = mainCamera.backgroundColor;
        }
    }

    private void Update()
    {
        if (globalLight != null)
        {
            globalLight.intensity = Mathf.MoveTowards(globalLight.intensity, intensidadObjetivo, velocidadCambio * Time.deltaTime);
        }

        if (mainCamera != null)
        {
            Color colorDestino = intensidadObjetivo > 0.5f ? colorOriginalFondo : Color.black;
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, colorDestino, velocidadCambio * Time.deltaTime);
        }
    }

    #endregion

    #region Public Methods

    public void ApagarLuz()
    {
        intensidadObjetivo = 0f;
    }

    public void EncenderLuz()
    {
        intensidadObjetivo = 1f;
    }

    #endregion

    #region Private Methods
    #endregion
}
