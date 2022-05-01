using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHealth = 50;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem dieParticle;

    void OnParticleCollision(GameObject other)
    {
        HitEnemy();
        Instantiate(hitParticle, transform.position, Quaternion.identity);
    }

    private void HitEnemy()
    {
        enemyHealth--;
        if (enemyHealth <= 0)
        {
            Instantiate(dieParticle, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }
    }
}
