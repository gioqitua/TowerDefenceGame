using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField][Range(0.1f, 20f)] float spawnInterval = 2f;
    [SerializeField] EnemyHealth enemyPrefab;
    [SerializeField] AudioClip spawnSound;
    bool canSpawn = true;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (canSpawn)
        {
            GetComponent<AudioSource>().PlayOneShot(spawnSound);
            
            var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);            

            enemy.transform.parent = transform;

            yield return new WaitForSeconds(spawnInterval);
        }

    }

}
