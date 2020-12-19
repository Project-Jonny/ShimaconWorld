using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonBlock : MonoBehaviour
{
    public GameObject DestroyEffect;
    public GameObject[] items;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (GameData.instance.power >= 4)
            {
                Instantiate(DestroyEffect, transform.position, Quaternion.identity);

                int num = Random.Range(0, 100);

                if(num < 20)
                {
                    int r = Random.Range(0, items.Length);
                    Instantiate(items[r], transform.position, Quaternion.identity);
                }

                Destroy(gameObject, 0.1f);
            }
        }
    }
}

