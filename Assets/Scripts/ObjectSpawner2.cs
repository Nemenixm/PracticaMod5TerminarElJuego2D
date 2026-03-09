using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectSpawner2 : MonoBehaviour
{
    public GameObject[] prefabs;
    public float interval = 2f;
    public float rangeX = 5f;

    void Start()
    {
        if (prefabs == null || prefabs.Length == 0)
        {
            Debug.LogError("No hay prefabs asignados en " + gameObject.name);
            return;
        }

        InvokeRepeating(nameof(Spawn), 1f, interval);
    }

    void Spawn()
    {
        if (prefabs == null || prefabs.Length == 0) return;

        float randomX = transform.position.x + Random.Range(-rangeX, rangeX);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0f);

        int randomIndex = Random.Range(0, prefabs.Length);
        Instantiate(prefabs[randomIndex], spawnPosition, Quaternion.identity);
    }
}
