using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] ParticleSystem towerExplosionParticle;
    EnemyHealth enemyHealth;
    [SerializeField] Pathfinder pathfinder;
    [SerializeField] float enemySpeed = 1f;
    [SerializeField] float stepSpeed = 1f;

    Vector3 targetPos;

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        
        // pathfinder = FindObjectOfType<Pathfinder>();

        pathfinder = this.transform.parent.GetComponent<Pathfinder>();

        var path = pathfinder.GetPath();
        StartCoroutine(Move(path));
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * stepSpeed);
    }
    IEnumerator Move(List<Waypoint> path)
    {
        foreach (var waypoint in path)
        {
            transform.LookAt(waypoint.transform);
            targetPos = waypoint.transform.position;
            yield return new WaitForSeconds(enemySpeed);
        }
        enemyHealth.DestroyEnemy(towerExplosionParticle);
        Castle.Instance.GetDamage();
    }

}
