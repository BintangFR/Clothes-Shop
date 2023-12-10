using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopKeeperController : MonoBehaviour
{
    public GameObject ShopBanner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //TODO: Refactor with compare by object type
        if (other.gameObject.CompareTag("Player"))
        {
            ShopBanner.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ShopBanner.SetActive(false);
        }
    }
}
