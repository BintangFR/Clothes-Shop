using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private int Money;
    private List<ItemData> Inventory;
    private int MaxInventorySize = 6;
    private ItemData EquippedOutfit;

    
    public PlayerData(int money, int maxInventorySize)
    {
        Money = money;
        MaxInventorySize = maxInventorySize;
        Inventory = new List<ItemData>();
    }

    public int GetMoney()
    {
        return Money;
    }

    public List<ItemData> GetInventory()
    {
        return Inventory;
    }

    public int GetMaxInventorySize()
    {
        return MaxInventorySize;
    }

    public void EquipOutfit(ItemData item)
    {
        EquippedOutfit = item;
    }

    public void BuyItem(ItemData item)
    {
        Inventory.Add(item);
        Money -= item.GetPrice();
    }

    public void SellItem(ItemData item)
    {
        Inventory.Remove(item);
        Money += item.GetPrice();
    }
}
