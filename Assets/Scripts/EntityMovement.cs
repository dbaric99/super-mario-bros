using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.left;

    private new Rigidbody2D rigidbody;
    private Vector2 velocity;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void OnBecomeVisible()
    {
        enabled = true;
    }

    private void OnBecomeInvisible()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        rigidbody.WakeUp();
    }

    private void OnDisable()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }
}
