using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[SelectionBase]
public class Tower : MonoBehaviour
{
    [SerializeField] Transform topTower;
    [SerializeField] Transform enemyTarget;
    [SerializeField] float shootRange = 10f;
    [SerializeField] ParticleSystem bulletParticles;
    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        SetTargetEnemy();
        if (enemyTarget && CalculateDistanceToEnemy() <= shootRange)
        {
            Fire(true);
        }
        else
        {
            Fire(false);
        }
    }

    private void SetTargetEnemy()
    {

        var allEnemys = FindObjectsOfType<Enemy>();
        if (allEnemys.Length == 0) return;

        Transform closestEnemy = allEnemys[0].transform;
        foreach (var enemy in allEnemys)
        {
            closestEnemy = GetClosestEnemy(closestEnemy.transform, enemy.transform);
        }
        enemyTarget = closestEnemy;
    }

    private Transform GetClosestEnemy(Transform enemyA, Transform enemyB)
    {
        var distToA = Vector3.Distance(enemyA.position, transform.position);
        var distToB = Vector3.Distance(enemyB.position, transform.position);
        if (distToA < distToB)
        {
            return enemyA;
        }
        return enemyB;
    }

    private void Fire(bool isActive)
    {
        topTower.LookAt(enemyTarget);
        var emission = bulletParticles.emission;
        emission.enabled = isActive;
    }

    private float CalculateDistanceToEnemy()
    {
        float distance = Vector3.Distance(this.transform.position, enemyTarget.transform.position);
        return distance;
    }
}