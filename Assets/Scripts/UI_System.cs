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