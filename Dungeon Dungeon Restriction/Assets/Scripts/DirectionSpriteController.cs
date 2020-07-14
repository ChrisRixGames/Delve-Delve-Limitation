using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionSpriteController : MonoBehaviour
{
    public Sprite leftSprite;
    public Sprite upSprite;
    public Sprite rightSprite;
    public Sprite downSprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void SetDirection(Vector2 direction)
    {
        if (direction.x == 1)
        {
            spriteRenderer.sprite = rightSprite;
        }
        else if (direction.x == -1)
        {
            spriteRenderer.sprite = leftSprite;
        }
        else if (direction.y == 1)
        {
            spriteRenderer.sprite = upSprite;
        }
        else if (direction.y == -1)
        {
            spriteRenderer.sprite = downSprite;
        }
    }
}
