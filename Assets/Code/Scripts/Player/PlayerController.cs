using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    private float movementX;
    private float movementY;
    public float speed = 1;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
}
