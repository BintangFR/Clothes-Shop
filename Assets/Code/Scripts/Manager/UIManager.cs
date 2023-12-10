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
        InventoryUIController.Init();
    }

    public void InitAction(UnityAction<string> onEquip, UnityAction onMenuClosed)
    {
        InventoryUIController.InitAction(onEquip, onMenuClosed);
    }

    public void SetCurrentState(PlayerState playerState)
    {
        InventoryUIController.UpdateCurrentPlayerState(playerState);
    }

    public void OpenMenu(CurrentInteractable type)
    {
        InventoryUIController.OpenMenu(type);
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
