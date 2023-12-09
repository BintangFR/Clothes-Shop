using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;

    public UnityEvent<int> OnPurchase;
    public UnityEvent<int> InitData;

    private void Start()
    {
        OnPurchase.AddListener(UpdateMoneyText);
    }
    
    private void UpdateMoneyText(int PlayerMoney)
    {
        MoneyText.text = "Money: " + PlayerMoney.ToString();
    }
}
