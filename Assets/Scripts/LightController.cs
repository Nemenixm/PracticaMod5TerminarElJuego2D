using UnityEngine;
using UnityEngine.Rendering.Universal; // Necesario para Light2D

public class LightController : MonoBehaviour
{
    [Header("Referencias")]
    public Light2D globalLight;
    public Camera mainCamera;

    [Header("Configuración")]
    public float velocidadCambio = 2f;
    
    private float intensidadObjetivo = 1f;
    private Color colorOriginalFondo;
    private bool zonaOscuraActiva = false;

    void Start()
    {
        // Guardamos el color que pusiste en la cámara (ese marrón) al empezar
        if (mainCamera != null)
        {
            colorOriginalFondo = mainCamera.backgroundColor;
        }
    }

    void Update()
    {
        // 1. Control de la Luz Global
        if (globalLight != null)
        {
            globalLight.intensity = Mathf.MoveTowards(globalLight.intensity, intensidadObjetivo, velocidadCambio * Time.deltaTime);
        }

        // 2. Control del Color de Fondo de la Cámara
        if (mainCamera != null)
        {
            // Si la intensidad objetivo es 0, el color destino es negro. Si es 1, es el original.
            Color colorDestino = (intensidadObjetivo > 0.5f) ? colorOriginalFondo : Color.black;
            
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, colorDestino, velocidadCambio * Time.deltaTime);
        }
    }

    public void ApagarLuz() 
    { 
        intensidadObjetivo = 0f; 
    }

    public void EncenderLuz() 
    { 
        intensidadObjetivo = 1f; 
    }
}