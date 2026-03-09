using UnityEngine;

public class MetaNivel : MonoBehaviour
{
    private UIController ui;
    private AudioSettingsController audioControl;

    void Start()
    {
        // Buscamos ambos controladores en la escena al iniciar
        ui = Object.FindFirstObjectByType<UIController>();
        audioControl = Object.FindFirstObjectByType<AudioSettingsController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificamos si el objeto que toca la meta es el Player
        if (collision.CompareTag("Player"))
        {
            GanarNivel();
        }
    }

    private void GanarNivel()
    {
        // 1. Detenemos la música y ponemos el efecto de victoria
        if (audioControl != null)
        {
            audioControl.PlayVictoria();
        }

        // 2. Mostramos el panel de victoria a través del UIController
        if (ui != null)
        {
            ui.MostrarVictoria();
        }
    }
}