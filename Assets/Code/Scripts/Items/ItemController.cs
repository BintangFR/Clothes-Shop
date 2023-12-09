using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public ItemData itemData;
    private BoxCollider2D boxCollider2D;
    public TextMeshPro itemPriceText;

    public void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        itemPriceText.text = itemData.Price.ToString()+"$";
    }
}
