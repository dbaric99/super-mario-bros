using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string SCROLL_AXIS = "Horizontal";
    private const string POWERUP_LAYER = "PowerUp";
    private const string ENEMY_LAYER = "Enemy";

    public float speed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;

    public float JumpForce => 2f * maxJumpHeight / (maxJumpTime / 2f);
    public float Gravity => -2f * maxJumpHeight / Mathf.Pow(maxJumpTime / 2f, 2);
    public bool IsGrounded { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsRunning => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f;
    public bool IsSliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f);

    private new Rigidbody2D rigidbody;
    private new Camera camera;
    private new Collider2D collider;
    private float inputAxis;
    private Vector2 velocity;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        camera = Camera.main;
        collider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        rigidbody.isKinematic = false;
        collider.enabled = true;
        velocity = Vector2.zero;
        IsJumping = false;
    }

    private void OnDisable()
    {
        rigidbody.isKinematic = true;
        collider.enabled = false;
        velocity = Vector2.zero;
        IsJumping = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(ENEMY_LAYER) && transform.DotPositionTest(collision.transform, Vector2.down))
        {
            velocity.y = JumpForce / 2f;
            IsJumping = true;
        }
        if (collision.gameObject.layer != LayerMask.NameToLayer(POWERUP_LAYER) && transform.DotPositionTest(collision.transform, Vector2.up))
        {
            velocity.y = 0f;
        }
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis(SCROLL_AXIS);
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * speed, speed * Time.deltaTime);

        if (rigidbody.Raycast(Vector2.right * velocity.x))
        {
            velocity.x = 0f;
        }

        if (velocity.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (velocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void GroundedMovement()
    {
        velocity.y = Mathf.Max(velocity.y, 0f);
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
