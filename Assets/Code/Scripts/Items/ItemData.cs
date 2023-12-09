using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    private int Price;
    private string Name;

    public ItemData(int price, string name)
    {
        Name = name;
        Price = price;
    }

    public int GetPrice()
    {
        return Price;
    }
}
