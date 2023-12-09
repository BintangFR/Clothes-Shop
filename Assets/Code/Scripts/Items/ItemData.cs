using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemData")]
public class ItemData : ScriptableObject
{
    public int Price;
    public string ItemName;
    public AnimatorOverrideController Override;
    public Sprite Icon;

    public int GetPrice()
    {
        return Price;
    }
}
