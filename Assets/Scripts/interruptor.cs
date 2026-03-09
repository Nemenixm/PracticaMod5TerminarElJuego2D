using UnityEngine;
using UnityEngine.InputSystem;

public class interruptor : MonoBehaviour
{
    #region Fields

    public LightController controller;
    public bool apagarAlEntrar = true;

    #endregion

    #region Properties
    #endregion

    #region Unity Callbacks

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (apagarAlEntrar)
            {
                controller.ApagarLuz();
            }
            else
            {
                controller.EncenderLuz();
            }
        }
    }

    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}
