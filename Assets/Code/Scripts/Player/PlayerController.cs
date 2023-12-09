using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    private float movementX;
    private float movementY;
    public float speed = 1;
    public int money = 0;
    public int maxInventorySize = 6;
    public Animator animator;
    private PlayerData playerData;
    private PlayerInput playerInput;
    private ItemController currentItem;
    public UIManager uiManager;
    private PlayerState playerState;
    private AnimatorOverrideController DefaultOutfit;

    //public AnimatorOverrideController AnimatorOverride;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerData = new PlayerData(money, maxInventorySize);
        playerInput.actions["Interact"].performed += BuyItem;
        playerInput.actions["OpenInventory"].performed += delegate { uiManager.OnOpenInventory.Invoke(); };
        uiManager.Init(playerData.GetMoney(), EquipOutfit);
        
    }

    private void BuyItem(InputAction.CallbackContext context)
    {
      
        if (playerData != null && currentItem != null && playerData.GetInventory().Count < playerData.GetMaxInventorySize())
        {
            playerData.BuyItem(currentItem.itemData);
            uiManager.OnPurchase.Invoke(currentItem.itemData, playerData.GetMoney());
            Destroy(currentItem.gameObject);
        }
    }

    private void EquipOutfit(string itemName)
    {
        foreach (var item in playerData.GetInventory())
        {
            if (item.ItemName == itemName)
            {
                animator.runtimeAnimatorController = item.Override;
                break;
            }
        }
        //animator.runtimeAnimatorController = currentItem.itemData.Override;
    }

    private void OnMove(InputValue inputValue)
    {
        if (playerState != PlayerState.Interacting)
        {
            Vector2 movementVector = inputValue.Get<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("Horizontal", movementX);
        animator.SetFloat("Vertical", movementY);
        animator.SetFloat("Speed", rb.velocity.sqrMagnitude);
    }

    void FixedUpdate()
    {
        UpdateAnimation();
        Vector2 movement = new Vector2(movementX, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //TODO: Refactor with compare by object type
        if (other.gameObject.CompareTag("Item"))
        {
            currentItem = other.gameObject.GetComponent<ItemController>();
        }
        else if (other.gameObject.CompareTag("ShopKeeper"))
        {

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            currentItem = null;
        }
    }
}

public enum PlayerState
{
    Idle,
    Walking,
    Interacting
}
