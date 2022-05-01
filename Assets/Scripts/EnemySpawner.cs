using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField][Range(0.1f, 20f)] float spawnInterval = 2f;
    [SerializeField] EnemyHealth enemyPrefab;
    bool canSpawn = true;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (canSpawn)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }

    }

}
