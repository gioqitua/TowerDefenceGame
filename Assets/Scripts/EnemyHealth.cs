using UnityEngine.UI;
using UnityEngine;

[SelectionBase]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float enemyHealth = 50;
    [SerializeField] ParticleSystem hitParticle;
    [SerializeField] ParticleSystem dieParticle;
    [SerializeField] AudioClip hitSoundFx;
    [SerializeField] AudioClip enemyDestroySoundFx;
    [SerializeField] GameObject healthbarUI;
    [SerializeField] Slider healthBarSlider;
    float maxHealth;

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        maxHealth = enemyHealth;
        healthBarSlider.value = CalculateHealthPercentage();
    }
    float CalculateHealthPercentage()
    {
        return enemyHealth / maxHealth;
    }
    void OnParticleCollision(GameObject other)
    {
        audioSource.PlayOneShot(hitSoundFx);

        HitEnemy();
        var hitFx = Instantiate(hitParticle, transform.position, Quaternion.identity);
        hitFx.transform.parent = transform;
    }
    private void HitEnemy()
    {
        enemyHealth--;
        healthBarSlider.value = CalculateHealthPercentage();
        if (enemyHealth <= 0)
        {
            AudioSource.PlayClipAtPoint(enemyDestroySoundFx, Camera.main.transform.position);
            DestroyEnemy(dieParticle);
            UI_System.Instance.SetScore();
        }
    }
    public void DestroyEnemy(ParticleSystem fx)
    {
        var deathFx = Instantiate(fx, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }
}
