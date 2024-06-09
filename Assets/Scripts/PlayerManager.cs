using System.Collections;
using Unity.VisualScripting;
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
    public bool IsStarpower { get; private set; }

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeFormRenderer = smallFormRenderer;
    }

    public void Hit()
    {
        if (!IsDead && !IsStarpower)
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

    public void Starpower(float duration = 10f)
    {
        StartCoroutine(StarpowerAnimation(duration));
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

    private IEnumerator StarpowerAnimation(float duration)
    {
        IsStarpower = true;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                activeFormRenderer.SpriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            }

            yield return null;
        }

        activeFormRenderer.SpriteRenderer.color = Color.white;
        IsStarpower = false;
    }
}
