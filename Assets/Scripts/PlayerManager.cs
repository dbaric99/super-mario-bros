using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerSpriteRenderer smallFormRenderer;
    public PlayerSpriteRenderer bigFormRenderer;

    private DeathAnimation deathAnimation;

    public bool IsBig => bigFormRenderer.enabled;
    public bool IsSmall => smallFormRenderer.enabled;
    public bool IsDead => deathAnimation.enabled;

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
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

    private void Shrink()
    {

    }

    private void Die()
    {
        smallFormRenderer.enabled = false;
        bigFormRenderer.enabled = false;
        deathAnimation.enabled = true;

        GameManager.Instance.PlayerDeath(3f);
    }
}
