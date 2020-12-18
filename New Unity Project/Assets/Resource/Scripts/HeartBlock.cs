using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBlock : MonoBehaviour
{
    [SerializeField] Sprite[] blockSprites;
    public SpriteRenderer spriteRenderer;

    public GameObject DestroyEffect;

    private void Start()
    {
        spriteRenderer.sprite = blockSprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (GameData.instance.power <= 1)
            {
                DestroyEffect.transform.localScale = new Vector3(1, 1, 1);

                if (spriteRenderer.sprite == blockSprites[0])
                {
                    spriteRenderer.sprite = blockSprites[1];
                }
                else if (spriteRenderer.sprite == blockSprites[1])
                {
                    spriteRenderer.sprite = blockSprites[2];
                }
                else if (spriteRenderer.sprite == blockSprites[2])
                {
                    Instantiate(DestroyEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject, 0.1f);
                }
            }

            else if (GameData.instance.power >= 2 && GameData.instance.power <= 3)
            {
                DestroyEffect.transform.localScale = new Vector3(1.2f, 1.2f, 1);

                if (spriteRenderer.sprite == blockSprites[0])
                {
                    spriteRenderer.sprite = blockSprites[2];
                }
                else if (spriteRenderer.sprite == blockSprites[1])
                {
                    spriteRenderer.sprite = blockSprites[2];
                }
                else if (spriteRenderer.sprite == blockSprites[2])
                {
                    Instantiate(DestroyEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject, 0.1f);
                }
            }

            else if (GameData.instance.power >= 4)
            {
                DestroyEffect.transform.localScale = new Vector3(1.5f, 1.5f, 1);

                if (spriteRenderer.sprite == blockSprites[0])
                {
                    Instantiate(DestroyEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject, 0.1f);
                }
                else if (spriteRenderer.sprite == blockSprites[1])
                {
                    Instantiate(DestroyEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject, 0.1f);
                }
                else if (spriteRenderer.sprite == blockSprites[2])
                {
                    Instantiate(DestroyEffect, transform.position, Quaternion.identity);
                    Destroy(gameObject, 0.1f);
                }
            }
        }

        if (collision.gameObject.tag == "EnemyBullet")
        {
            if (spriteRenderer.sprite == blockSprites[0])
            {
                spriteRenderer.sprite = blockSprites[1];
            }
            else if (spriteRenderer.sprite == blockSprites[1])
            {
                spriteRenderer.sprite = blockSprites[2];
            }
            else if (spriteRenderer.sprite == blockSprites[2])
            {
                Instantiate(DestroyEffect, transform.position, Quaternion.identity);
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
