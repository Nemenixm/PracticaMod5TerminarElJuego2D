using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject panelAjustes;
    [SerializeField] private GameObject panelSalir;
    [SerializeField] private GameObject panelInstrucciones;

    [Header("Scene To Load")]
    [SerializeField] private string gameSceneName = "game";

    private void Awake()
    {
        // Arranca con todo cerrado (por si acaso)
        if (panelAjustes != null) panelAjustes.SetActive(false);
        if (panelSalir != null) panelSalir.SetActive(false);
    }

    // BOTÓN: JUGAR
    public void OnPlay()
    {
        // Si venías de pausa, por si acaso:
        Time.timeScale = 1f;

        SceneManager.LoadScene(gameSceneName);
    }

    // BOTÓN: AJUSTES
    public void OnOpenAjustes()
    {
        if (panelAjustes != null) panelAjustes.SetActive(true);
    }

    public void OnCloseAjustes()
    {
        if (panelAjustes != null) panelAjustes.SetActive(false);
    }
    //BOTÓN INSTRUCCIONES
    public void OnOpenInstrucciones()
    {
        if (panelInstrucciones != null) panelInstrucciones.SetActive(true);
    }

    public void OnCloseInstrucciones()
    {
        if (panelInstrucciones != null) panelInstrucciones.SetActive(false);
    }

    // BOTÓN: SALIR
    public void OnOpenSalir()
    {
        if (panelSalir != null) panelSalir.SetActive(true);
    }

    public void OnCloseSalir()
    {
        if (panelSalir != null) panelSalir.SetActive(false);
    }

    // Opcional: confirmar salida desde PanelSalir
    public void OnQuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}