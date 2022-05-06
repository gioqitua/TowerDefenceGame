using UnityEngine.UI;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public static Castle Instance;
    [SerializeField] float castleHealth = 10;
    [SerializeField] int incomingDamagePerEnemy = 1;
    [SerializeField] AudioClip castleDamageSoundFX;
    [SerializeField] GameObject healthbarUI;
    [SerializeField] Slider healthBarSlider;
    AudioSource audioSource;
    float maxHealth;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        maxHealth = castleHealth;
        healthBarSlider.value = CalculateHealthPercentage();
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("more than 1 castle in scene");
            return;
        }
        Instance = this;
    }
    float CalculateHealthPercentage()
    {
        return castleHealth / maxHealth;
    }
    public float GetCurrentHealth()
    {
        return castleHealth;
    }
    public void GetDamage()
    {
        castleHealth -= incomingDamagePerEnemy;
        audioSource.PlayOneShot(castleDamageSoundFX);
        UI_System.Instance.SetCastleHealth(castleHealth);
        healthBarSlider.value = CalculateHealthPercentage();
        if (castleHealth <= 0)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        Debug.Log("Game Over");
    }
}
