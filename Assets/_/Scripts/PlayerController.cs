using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private float movement_speed_orignal;
  [Header("Character attributes:")]
  public float movement_base_speed = 3.0f;

  [Space]
  [Header("Character statistics:")]
  public Vector2 movementDirection;
  public float movementSpeed;

  [Space]
  [Header("Reference:")]
  public Rigidbody2D rb;
  public Animator animator;
  public Transform spriteTrans;

  private void Awake()
  {
    movement_speed_orignal = movement_base_speed;
    Debug.Log($"Speed:{animator.speed}");
  }

  private void Update()
  {
    ProcessInput();
    Move();
    Animate();

    SprintInput();
  }

  private void ProcessInput()
  {
    movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
    movementDirection.Normalize();
  }

  private void SprintInput()
  {
    float currentSpeed = movement_base_speed;

    if (Input.GetKey(KeyCode.LeftShift) && movementSpeed > 0)
    {
      movement_base_speed = movement_speed_orignal * 1.5f;
      animator.speed = 1.5f;
    }
    else
    {
      movement_base_speed = movement_speed_orignal;
      animator.speed = 1;
    }
  }

  private void Move()
  {
    rb.velocity = movementDirection * movementSpeed * movement_base_speed;
  }

  private void Animate()
  {
    animator.SetFloat("MovementSpeed", movementSpeed);
    float directionX = Input.GetAxis("Horizontal");
    if (directionX != 0)
    {
      spriteTrans.localScale = new Vector2(directionX > 0 ? 1 : -1, 1);
    }
  }
}
