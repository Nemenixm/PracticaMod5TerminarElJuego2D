using UnityEngine;
using UnityEngine.InputSystem;

public class ZonaSegura : MonoBehaviour
{
    #region Fields
    #endregion

    #region Properties
    #endregion

    #region Unity Callbacks

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.layer = LayerMask.NameToLayer("ZonaSegura");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}
