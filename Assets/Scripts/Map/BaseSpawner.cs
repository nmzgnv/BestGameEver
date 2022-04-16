using System.Collections;
using UnityEngine;

public class BaseSpawner : MonoBehaviour
{
    [SerializeField]
    private int spawnCount = 0;

    [SerializeField]
    private GameObject objectToSpawn;

    [SerializeField]
    [Tooltip("Object will be spawned on center of spawner by default")]
    private Transform target;

    [SerializeField]
    private float delaySeconds;


    private IEnumerator Spawn()
    {
        for (var c = 0; c < spawnCount; c++)
        {
            var spawned = Instantiate(objectToSpawn, target.position, Quaternion.identity, transform);
            HandleSpawnedObject(spawned);
            yield return new WaitForSeconds(delaySeconds);
        }
    }

    protected virtual void HandleSpawnedObject(GameObject spawnedObject)
    {
    }

    public void StartSpawn()
    {
        StartCoroutine(Spawn());
    }

    private void Awake()
    {
        if (target == null)
            target = transform;
    }
}