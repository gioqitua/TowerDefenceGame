using UnityEngine;
using TMPro;
using System;

public class UI_System : MonoBehaviour
{
    public static UI_System Instance;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] TMP_Text castleHealthText;
    [SerializeField] TMP_Text totalTowers;
    [SerializeField] TMP_Text buyButtonText;
    [SerializeField] float money = 0;
    [SerializeField] GameObject Tower1Button;
    private float towerStartPrice = 10f;

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
        money = 0;
        towerStartPrice = 10f;
        moneyText.SetText("$ " + money.ToString());
        var castleHealth = Castle.Instance.GetCurrentHealth();
        castleHealthText.SetText(castleHealth.ToString());
        totalTowers.SetText(TowerPool.Instance.towerQueue.Count.ToString());
        buyButtonText.SetText(CalculateTowerPrice().ToString() + " $");
    }
    internal void SetCastleHealth(float health)
    {
        castleHealthText.SetText(health.ToString());
    }
    public void SetScore(int scoreToAdd)
    {
        money += scoreToAdd;
        moneyText.SetText("$ " + money.ToString());
    }
    public void SetTotalTowerText(int activeTowerCount, int maxTowerCount)
    {
        totalTowers.SetText(activeTowerCount.ToString() + "/" + maxTowerCount.ToString());
    }
    float CalculateTowerPrice()
    {
        float price = TowerPool.Instance.maxTowerCount * towerStartPrice;

        return price;
    }
    public void BuyNewTower()
    {
        OpenMarket();

    }

    private void OpenMarket()
    {
        if (money >= CalculateTowerPrice())
        {
            money -= CalculateTowerPrice();
            moneyText.SetText("$ " + money.ToString());
            TowerPool.Instance.SetMaxTowerCount(1);
            buyButtonText.SetText(CalculateTowerPrice().ToString() + " $");
        }
        else
        {
            return;
        }
    }
}