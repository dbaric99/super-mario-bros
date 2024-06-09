using UnityEngine;

public class Goomba : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    private const string SHELL_LAYER = "Shell";

    public Sprite squashedSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_TAG))
        {
            PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();

            if (collision.transform.DotPositionTest(transform, Vector2.down))
            {
                Flatten();
            }
            else
            {
                playerManager.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(SHELL_LAYER))
        {
            Hit();
        }
    }

    private void Flatten()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = squashedSprite;
        Destroy(gameObject, 0.75f);
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 3f);
    }
}
