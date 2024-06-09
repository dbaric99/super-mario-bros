using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    public Sprite emptyBlock;
    public int maxHits = -1;

    private bool isAnimated;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAnimated && maxHits != 0 && collision.gameObject.CompareTag(PLAYER_TAG))
        {
            if (collision.transform.DotPositionTest(transform, Vector2.up))
            {
                Hit();
            }
        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;

        maxHits--;

        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        isAnimated = true;

        Vector3 defaultPosition = transform.localPosition;
        Vector3 animatedPosition = defaultPosition + Vector3.up * 0.5f;

        yield return Move(defaultPosition, animatedPosition);
        yield return Move(animatedPosition, defaultPosition);

        isAnimated = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }
}
