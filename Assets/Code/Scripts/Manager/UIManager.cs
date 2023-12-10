using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI MoneyText;

    public InventoryUIController InventoryUIController;

    //public UnityAction<ItemData, int> OnPurchase;
    //public UnityAction OnOpenInventory;


    public void Init(int PlayerMoney)
    {
        UpdateMoneyText(PlayerMoney);
    }

    public void InitAction(UnityAction<string> onEquip)
    {
        InventoryUIController.Init(onEquip);
    }

    public void OpenInventory()
    {
        InventoryUIController.gameObject.SetActive(true);
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
