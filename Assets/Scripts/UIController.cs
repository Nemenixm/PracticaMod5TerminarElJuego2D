using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour
{
    [Header("Paneles de la Interfaz")]
    public GameObject panelVictoria;
    public GameObject panelPausa;

    [Header("Configuración de Tiempo")]
    public float tiempoDeEspera = 10f;

    private bool estaPausado = false;

    void Start()
    {
        if (panelVictoria != null) panelVictoria.SetActive(false);
        if (panelPausa != null) panelPausa.SetActive(false);

        Time.timeScale = 1f;
    }

    public void TogglePause()
    {
        if (panelVictoria != null && panelVictoria.activeSelf)
            return;

        estaPausado = !estaPausado;

        if (panelPausa != null)
            panelPausa.SetActive(estaPausado);

        Time.timeScale = estaPausado ? 0f : 1f;
        Cursor.visible = estaPausado;
        Cursor.lockState = estaPausado ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void BotonReanudar()
    {
        if (estaPausado)
            TogglePause();
    }

    public void MostrarVictoria()
    {
        if (panelVictoria != null)
            panelVictoria.SetActive(true);

        StartCoroutine(EsperarYReiniciar());
    }

    IEnumerator EsperarYReiniciar()
    {
        yield return new WaitForSecondsRealtime(tiempoDeEspera);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}