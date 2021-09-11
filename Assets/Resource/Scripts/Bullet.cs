using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    byte red;
    byte green;
    byte blue;

    byte alpha;

    void Start()
    {
        red = 240;
        green = 200;
        blue = 240;

        alpha = 255;

        StartCoroutine(Colorful());
    }

    IEnumerator Colorful()
    {
        while (true)
        {
            while (red > 200)
            {
                red -= 15;
                spriteRenderer.color = new Color32(red, green, blue, alpha);
                yield return new WaitForSeconds(0.001f);
            }
            while (green < 240)
            {
                green += 15;
                spriteRenderer.color = new Color32(red, green, blue, alpha);
                yield return new WaitForSeconds(0.001f);
            }
            while (blue > 200)
            {
                blue -= 15;
                spriteRenderer.color = new Color32(red, green, blue, alpha);
                yield return new WaitForSeconds(0.001f);
            }
            while (red < 240)
            {
                red += 15;
                spriteRenderer.color = new Color32(red, green, blue, alpha);
                yield return new WaitForSeconds(0.001f);
            }
            while (green > 200)
            {
                green -= 15;
                spriteRenderer.color = new Color32(red, green, blue, alpha);
                yield return new WaitForSeconds(0.001f);
            }
            while (blue < 240)
            {
                blue += 15;
                spriteRenderer.color = new Color32(red, green, blue, alpha);
                yield return new WaitForSeconds(0.001f);
            }
            yield return new WaitForSeconds(0.001f);
        }
    }
}
