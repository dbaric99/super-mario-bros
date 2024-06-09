using UnityEngine;

public class Koopa : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    public Sprite shellSprite;

    private bool isShelled;
    private bool isShellMoving;

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

    private void EnterShell()
    {
        isShelled = true;

        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }
}
