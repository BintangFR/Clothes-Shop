using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;

    public InventoryUIController InventoryUIController;

    public UnityEvent<ItemData, int> OnPurchase;
    public UnityEvent OnOpenInventory;


    public void Init(int PlayerMoney, UnityAction<string> onEquip)
    {
        UpdateMoneyText(PlayerMoney);
        OnPurchase.AddListener(Transaction);
        OnOpenInventory.AddListener(() => InventoryUIController.gameObject.SetActive(true));
        InventoryUIController.Init(onEquip);
    }

    public void Transaction(ItemData itemData, int playerMoney)
    {
        InventoryUIController.AddItem(itemData);
        UpdateMoneyText(playerMoney);
    }
    
    private void UpdateMoneyText(int PlayerMoney)
    {
        MoneyText.text = "Money: " + PlayerMoney.ToString();
    }
}
