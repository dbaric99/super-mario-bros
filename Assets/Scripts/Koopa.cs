using UnityEngine;

public class Koopa : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    public Sprite shellSprite;
    public float shellSpeed = 12f;

    private bool isShelled;
    private bool isPushed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isShelled && collision.gameObject.CompareTag(PLAYER_TAG))
        {
            PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();

            if (collision.transform.DotPositionTest(transform, Vector2.down))
            {
                EnterShell();
            }
            else
            {
                playerManager.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isShelled && other.CompareTag(PLAYER_TAG))
        {
            if (!isPushed)
            {
                Vector2 direction = new Vector2(transform.position.x - other.transform.position.x, 0f);
                PushShell(direction);
            }
            else
            {
                PlayerManager playerManager = other.GetComponent<PlayerManager>();
                playerManager.Hit();
            }
        }
    }

    private void EnterShell()
    {
        isShelled = true;

        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }

    private void PushShell(Vector2 direction)
    {
        isPushed = true;

        GetComponent<Rigidbody2D>().isKinematic = false;

        EntityMovement entityMovement = GetComponent<EntityMovement>();
        entityMovement.direction = direction.normalized;
        entityMovement.speed = shellSpeed;
        entityMovement.enabled = true;
    }
}
