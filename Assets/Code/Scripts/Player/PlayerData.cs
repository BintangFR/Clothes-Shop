using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private int Money;
    private List<ItemData> Inventory;

    
    public PlayerData(int money)
    {
        Money = money;
        Inventory = new List<ItemData>();
    }

    public int GetMoney()
    {
        return Money;
    }

    public void BuyItem(ItemData item)
    {
        Inventory.Add(item);
        Money -= item.GetPrice();
    }
}
