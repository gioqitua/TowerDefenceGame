using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField][Range(1, 10f)] float spawnInterval = 10f;
    [SerializeField] AudioClip spawnSound;
    [SerializeField] List<Enemy> enemys = new List<Enemy>();
    [SerializeField] Enemy bossPrefab;
    int bossSpawnIndex = 55;
    bool canSpawn = true;
    static int spawnerCount = 0;

    void Start()
    {
        bossSpawnIndex = 55;
        StartCoroutine(SpawnEnemy());
    }
    IEnumerator SpawnEnemy()
    {
        while (canSpawn)
        {
            spawnerCount++;

            if (spawnerCount >= bossSpawnIndex && bossPrefab != null)
            {
                spawnerCount = 0;

                var boss = Instantiate(bossPrefab, transform.position, Quaternion.identity);

                boss.transform.parent = transform;

                yield return new WaitForSeconds(spawnInterval);
            }
            spawnInterval -= CalculateSpawnInterval(spawnInterval);

            GetComponent<AudioSource>().PlayOneShot(spawnSound);

            var randomIndex = UnityEngine.Random.Range(0, enemys.Count);

            var enemy = Instantiate(enemys[randomIndex], transform.position, Quaternion.identity);

            enemy.transform.parent = transform;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
    float CalculateSpawnInterval(float spawnInterval)
    {
        if (spawnInterval <= 1)
        {
            return 0;
        }
        else
        {
            return spawnInterval / 100;
        }
    }
}
