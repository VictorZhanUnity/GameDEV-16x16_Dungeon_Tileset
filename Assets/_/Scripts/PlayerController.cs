using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Character attributes:")]
    public float movement_base_speed = 1.0f;

    [Space]
    [Header("Character statistics:")]
    public Vector2 movementDirection;
    public float movementSpeed;

    [Space]
    [Header("Reference:")]
    public Rigidbody2D rb;
    public Animator animator;
    public Transform spriteTrans;

    private void Update()
    {
        ProcessInput();
        Move();
        Animate();
    }

    private void ProcessInput()
    {
        movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
        movementDirection.Normalize();
    }

    private void Move()
    {
        rb.velocity = movementDirection * movementSpeed * movement_base_speed;
    }

    private void Animate()
    {
        animator.SetFloat("MovementSpeed", movementSpeed);
        float directionX = Input.GetAxis("Horizontal");
        if(directionX != 0)
        {
            spriteTrans.localScale = new Vector2(directionX > 0 ? 1 : -1, 1);
        }
    }
}
