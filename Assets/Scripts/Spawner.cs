using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabEnemy;
    public float spawnTime = 2;

    float lastSpawnTime;

    void Start()
    {

    }

    void Update()
    {
        if (lastSpawnTime + spawnTime > Time.time)
        {
            return;
        }
        lastSpawnTime = Time.time;

        GameObject enemy = Instantiate(prefabEnemy, null);
        enemy.transform.rotation = transform.rotation;
        float x = UnityEngine.Random.Range(-10, 10);
        float z = UnityEngine.Random.Range(-10, 10);
        enemy.transform.position = new Vector3(x, transform.position.y + 1, z);
    }
}
