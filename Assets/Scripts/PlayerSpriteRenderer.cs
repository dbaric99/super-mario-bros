using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public AnimatedSprite run;

    public SpriteRenderer SpriteRenderer { get; private set; }
    private PlayerMovement playerMovement;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void LateUpdate()
    {
        run.enabled = playerMovement.IsRunning;

        if (playerMovement.IsJumping)
        {
            SpriteRenderer.sprite = jump;
        }
        else if (playerMovement.IsSliding)
        {
            SpriteRenderer.sprite = slide;
        }
        else if (!playerMovement.IsRunning)
        {
            SpriteRenderer.sprite = idle;
        }
    }

    private void OnEnable()
    {
        SpriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        SpriteRenderer.enabled = false;
        run.enabled = false;
    }
}
