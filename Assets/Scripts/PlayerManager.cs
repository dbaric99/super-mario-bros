using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerSpriteRenderer smallFormRenderer;
    public PlayerSpriteRenderer bigFormRenderer;
    private PlayerSpriteRenderer activeFormRenderer;

    private DeathAnimation deathAnimation;
    private CapsuleCollider2D capsuleCollider;

    public bool IsBig => bigFormRenderer.enabled;
    public bool IsSmall => smallFormRenderer.enabled;
    public bool IsDead => deathAnimation.enabled;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    public void Hit()
    {
        if (IsBig)
        {
            Shrink();
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        smallFormRenderer.enabled = false;
        bigFormRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.PlayerDeath(3f);
    }

    public void Grow()
    {
        smallFormRenderer.enabled = false;
        bigFormRenderer.enabled = true;
        activeFormRenderer = bigFormRenderer;

        capsuleCollider.size = new Vector2(1f, 2f);
        capsuleCollider.offset = new Vector2(0f, 0.5f);

        StartCoroutine(ScaleAnimation());
    }

    private void Shrink()
    {
        smallFormRenderer.enabled = true;
        bigFormRenderer.enabled = false;
        activeFormRenderer = smallFormRenderer;

        capsuleCollider.size = new Vector2(1f, 1f);
        capsuleCollider.offset = new Vector2(0f, 0f);

        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                smallFormRenderer.enabled = !smallFormRenderer.enabled;
                bigFormRenderer.enabled = !smallFormRenderer.enabled;
            }

            yield return null;
        }

        smallFormRenderer.enabled = false;
        bigFormRenderer.enabled = false;
        activeFormRenderer.enabled = true;
    }
}
