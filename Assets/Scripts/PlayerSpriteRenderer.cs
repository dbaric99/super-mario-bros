using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    public Sprite idle;
    public Sprite jump;
    public Sprite slide;
    public Sprite run;

    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void LateUpdate()
    {
        if (playerMovement.IsJumping)
        {
            spriteRenderer.sprite = jump;
        }
        else if (playerMovement.IsSliding)
        {
            spriteRenderer.sprite = slide;
        }
        else if (playerMovement.IsRunning)
        {
            spriteRenderer.sprite = run;
        }
        else
        {
            spriteRenderer.sprite = idle;
        }
    }
}
