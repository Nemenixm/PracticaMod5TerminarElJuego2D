using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Paneles de Interfaz")]
    [SerializeField] private GameObject panelMain;
    [SerializeField] private GameObject panelAjustes;
    [SerializeField] private GameObject panelSalir;

    [Header("Configuración")]
    [SerializeField] private string escenaJuego = "game";

    private void Start()
    {
        // Aseguramos que al empezar solo se vea el menú principal
        RegresarAlMenu();
    }

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
        // Nota: Application.Quit no funciona dentro del editor de Unity, solo en el build final.
    }

    // Método auxiliar para no repetir código (DRY - Don't Repeat Yourself)
    private void SetPanels(bool main, bool ajustes, bool salir)
    {
        if(panelMain) panelMain.SetActive(main);
        if(panelAjustes) panelAjustes.SetActive(ajustes);
        if(panelSalir) panelSalir.SetActive(salir);
    }
}