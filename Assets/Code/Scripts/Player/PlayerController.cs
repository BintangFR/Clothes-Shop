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
    public Animator animator;
    private PlayerData playerData;
    private PlayerInput playerInput;
    private ItemController currentItem;
    public UIManager uiManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerData = new PlayerData(money);
        playerInput.actions["Interact"].performed += BuyItem;
        uiManager.Init(playerData.GetMoney());
        
    }

    private void BuyItem(InputAction.CallbackContext context)
    {
      
        if (playerData != null && currentItem != null)
        {
            Debug.Log("Interact");
            playerData.BuyItem(currentItem.itemData);
            Destroy(currentItem.gameObject);
            uiManager.OnPurchase.Invoke(playerData.GetMoney());
        }
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 movementVector = inputValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
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
        if (other.gameObject.CompareTag("Item"))
        {
            currentItem = other.gameObject.GetComponent<ItemController>();
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
