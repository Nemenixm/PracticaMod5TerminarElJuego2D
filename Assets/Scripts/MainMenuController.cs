using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    #region Fields

    [Header("Panels")]
    [SerializeField] private GameObject panelAjustes;
    [SerializeField] private GameObject panelSalir;
    [SerializeField] private GameObject panelInstrucciones;

    [Header("Scene To Load")]
    [SerializeField] private string gameSceneName = "game";

    #endregion

    #region Properties
    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        if (panelAjustes != null)
        {
            panelAjustes.SetActive(false);
        }

        if (panelSalir != null)
        {
            panelSalir.SetActive(false);
        }

        if (panelInstrucciones != null)
        {
            panelInstrucciones.SetActive(false);
        }
    }

    #endregion

    #region Public Methods

    public void OnPlay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnOpenAjustes()
    {
        if (panelAjustes != null)
        {
            panelAjustes.SetActive(true);
        }
    }

    public void OnCloseAjustes()
    {
        if (panelAjustes != null)
        {
            panelAjustes.SetActive(false);
        }
    }

    public void OnOpenInstrucciones()
    {
        if (panelInstrucciones != null)
        {
            panelInstrucciones.SetActive(true);
        }
    }

    public void OnCloseInstrucciones()
    {
        if (panelInstrucciones != null)
        {
            panelInstrucciones.SetActive(false);
        }
    }

    public void OnOpenSalir()
    {
        if (panelSalir != null)
        {
            panelSalir.SetActive(true);
        }
    }

    public void OnCloseSalir()
    {
        if (panelSalir != null)
        {
            panelSalir.SetActive(false);
        }
    }

    public void OnQuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #endregion

    #region Private Methods
    #endregion
}
