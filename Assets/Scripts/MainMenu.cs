using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Fields

    [Header("Paneles de Interfaz")]
    [SerializeField] private GameObject panelMain;
    [SerializeField] private GameObject panelAjustes;
    [SerializeField] private GameObject panelSalir;

    [Header("Configuración")]
    [SerializeField] private string escenaJuego = "game";

    #endregion

    #region Properties
    #endregion

    #region Unity Callbacks

    private void Start()
    {
        RegresarAlMenu();
    }

    #endregion

    #region Public Methods

    public void Jugar()
    {
        Debug.Log("Cargando escena: " + escenaJuego);
        SceneManager.LoadScene(escenaJuego);
    }

    public void AbrirAjustes()
    {
        SetPanels(false, true, false);
    }

    public void AbrirSalir()
    {
        SetPanels(false, false, true);
    }

    public void RegresarAlMenu()
    {
        SetPanels(true, false, false);
    }

    public void ConfirmarSalida()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    #endregion

    #region Private Methods

    private void SetPanels(bool main, bool ajustes, bool salir)
    {
        if (panelMain != null)
        {
            panelMain.SetActive(main);
        }

        if (panelAjustes != null)
        {
            panelAjustes.SetActive(ajustes);
        }

        if (panelSalir != null)
        {
            panelSalir.SetActive(salir);
        }
    }

    #endregion
}
