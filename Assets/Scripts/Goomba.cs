using UnityEngine;

public class Goomba : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    public Sprite squashedSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_TAG) && collision.transform.DotPositionTest(transform, Vector2.down))
        {
            Flatten();
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
}