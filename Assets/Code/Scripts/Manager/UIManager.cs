using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;

    public UnityEvent<int> OnPurchase;

    private void Start()
    {
        OnPurchase.AddListener(UpdateMoneyText);
    }

    public void Init(int PlayerMoney)
    {
        UpdateMoneyText(PlayerMoney);
    }


    
    private void UpdateMoneyText(int PlayerMoney)
    {
        MoneyText.text = "Money: " + PlayerMoney.ToString();
    }
}
