using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemData")]
public class ItemData : ScriptableObject
{
    public int Price;
    public string Name;
    public AnimatorOverrideController Override;

    public int GetPrice()
    {
        return Price;
    }
}
