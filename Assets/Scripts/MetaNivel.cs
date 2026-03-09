using UnityEngine;

public class MetaNivel : MonoBehaviour
{
    #region Fields

    private UIController ui;
    private AudioSettingsController audioControl;

    #endregion

    #region Properties
    #endregion

    #region Unity Callbacks

    private void Start()
    {
        ui = Object.FindFirstObjectByType<UIController>();
        audioControl = Object.FindFirstObjectByType<AudioSettingsController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GanarNivel();
        }
    }

    #endregion

    #region Public Methods
    #endregion

    #region Private Methods

    private void GanarNivel()
    {
        if (audioControl != null)
        {
            audioControl.PlayVictoria();
        }

        if (ui != null)
        {
            ui.MostrarVictoria();
        }
    }

    #endregion
}
