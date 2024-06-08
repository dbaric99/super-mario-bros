using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string SCROLL_AXIS = "Horizontal";

    public float speed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;

    public float JumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float Gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2);
    public bool IsGrounded { get; private set; }
    public bool IsJumping { get; private set; }

    private new Rigidbody2D rigidbody;
    private new Camera camera;
    private float inputAxis;
    private Vector2 velocity;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    private void Update()
    {
        HorizontalMovement();

        IsGrounded = rigidbody.Raycast(Vector2.down);
        if (IsGrounded)
        {
            GroundedMovement();
        }

        ApplyGravity();
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rigidbody.MovePosition(position);
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis(SCROLL_AXIS);
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * speed, speed * Time.deltaTime);
    }

    private void GroundedMovement()
    {
        IsJumping = velocity.y > 0f;

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = JumpForce;
            IsJumping = true;
        }
    }

    private void ApplyGravity()
    {
        bool isFalling = velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = isFalling ? 2f : 1f;

        velocity.y += Gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, Gravity / 2f);
    }
}
