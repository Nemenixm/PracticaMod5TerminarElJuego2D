using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class camera : MonoBehaviour
{
    #region Fields

    [Header("Target")]
    public Transform target;

    [Header("Follow")]
    public Vector3 offset = new Vector3(0f, 5f, -10f);
    public float smoothTime = 0.15f;

    private Vector3 velocity = Vector3.zero;

    #endregion

    #region Properties
    #endregion

    #region Unity Callbacks

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }

    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}
