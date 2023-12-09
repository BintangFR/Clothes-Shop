using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public ItemData itemData;
    private BoxCollider2D boxCollider2D;

    public void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
}
