using UnityEngine;

public class SpawnerMap : MonoBehaviour
{
    #region Fields

    public GameObject[] prefabs;
    public float interval = 2f;
    public float rangeX = 5f;

    #endregion

    #region Properties
    #endregion

    #region Unity Callbacks

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 1f, interval);
    }

    #endregion

    #region Public Methods
    #endregion

    #region Private Methods

    private void Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(-rangeX, rangeX), transform.position.y, 0f);
        int index = Random.Range(0, prefabs.Length);
        Instantiate(prefabs[index], pos, Quaternion.identity);
    }

    #endregion
}
