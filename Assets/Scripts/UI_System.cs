using UnityEngine;
using TMPro;

public class UI_System : MonoBehaviour
{
    public static UI_System Instance;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text castleHealthText;
    [SerializeField] int score = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("more than 1 UI_System in scene");
            return;
        }
        Instance = this;
    }
    void Start()
    {
        scoreText.SetText(score.ToString());
        var castleHealth = Castle.Instance.GetCurrentHealth();
        castleHealthText.SetText(castleHealth.ToString());
    }
    internal void SetCastleHealth(float health)
    {
        castleHealthText.SetText(health.ToString());
    }
    public void SetScore()
    {
        score++;
        scoreText.SetText(score.ToString());
    }
}