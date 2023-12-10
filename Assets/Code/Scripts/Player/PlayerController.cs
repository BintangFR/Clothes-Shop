using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.CullingGroup;

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
    private PlayerState playerState;
    private CurrentInteractable currentInteractable;
    private AnimatorOverrideController DefaultOutfit;
    public UnityAction<ItemData, int> OnPurchase;
    public UnityAction OpenShop;
    public UnityAction<PlayerState> OnStateChanged;


    public PlayerData Init()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerData = new PlayerData(money, maxInventorySize);
        
        
       // uiManager.Init(playerData.GetMoney(), EquipOutfit);
        return playerData;
    }

    public void InitAction(UnityAction<ItemData, int> onPurchase, UnityAction <CurrentInteractable> OpenMenu, UnityAction<PlayerState> state)
    {
        OnPurchase += onPurchase;
        OpenShop += ()=>OpenMenu(CurrentInteractable.ShopKeeper);
        playerInput.actions["OpenInventory"].performed += delegate { 
            OpenMenu.Invoke(CurrentInteractable.Item); 
            ChangeState(PlayerState.BrowseInventory);
        };
        playerInput.actions["Interact"].performed += Interact;
        OnStateChanged += state;
    }

    public void ResetState()
    {
        ChangeState(PlayerState.Idle);
    }

    public PlayerState GetCurrentState()
    {
        return playerState;
    }

    private void Interact(InputAction.CallbackContext context)
    {
     
        if (currentInteractable is CurrentInteractable.Item && playerData.GetInventory().Count < playerData.GetMaxInventorySize())
        {
            Debug.Log("BuyItem");
            playerData.BuyItem(currentItem.itemData);
            OnPurchase.Invoke(currentItem.itemData, playerData.GetMoney());
            Destroy(currentItem.gameObject);
            return;
        }
        else if (playerData.GetInventory().Count >= playerData.GetMaxInventorySize())
        {
            Debug.Log("Inventory is full");
            return;
        }

        if (currentInteractable is CurrentInteractable.ShopKeeper)
        {
            Debug.Log("SellItem");
            ChangeState(PlayerState.Selling);
            OpenShop.Invoke();
        }   
    }

    private void ChangeState(PlayerState state)
    {
        OnStateChanged.Invoke(state);
        playerState = state;
    }

    public void EquipOutfit(string itemName)
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
        if (playerState == PlayerState.Idle)
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
            currentInteractable = CurrentInteractable.Item;
        }
        else if (other.gameObject.CompareTag("ShopKeeper"))
        {
            currentInteractable = CurrentInteractable.ShopKeeper;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            currentItem = null;
        }
        currentInteractable = CurrentInteractable.None;
    }
}

public enum PlayerState
{
    Idle,
    Walking,
    Selling,
    BrowseInventory
}

public enum CurrentInteractable
{
    None,
    Item,
    ShopKeeper
}
