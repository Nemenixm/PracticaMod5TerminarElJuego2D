using UnityEngine;

public class SpawnerMap : MonoBehaviour
{
  public GameObject[] prefabs;
    public float interval = 2f;
    public float rangeX = 5f;

    void Start() { InvokeRepeating("Spawn", 1f, interval); }

    void Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(-rangeX, rangeX), transform.position.y, 0);
        Instantiate(prefabs[Random.Range(0, prefabs.Length)], pos, Quaternion.identity);
    }
}