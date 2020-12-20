using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chocolate : MonoBehaviour
{
    public GameObject[] items;

    void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (GameData.instance.power <= 1)
            {
                SoundManager.instance.PlaySE(9);
                if (transform.localScale == new Vector3(1, 1, 1))
                {
                    transform.localScale = new Vector3(0.8f, 0.8f, 1);
                }

                else if (transform.localScale == new Vector3(0.8f, 0.8f, 1))
                {
                    transform.localScale = new Vector3(0.5f, 0.5f, 1);
                }

                else if (transform.localScale == new Vector3(0.5f, 0.5f, 1))
                {
                    RandomNum();
                    Destroy(gameObject, 0.1f);
                }
            }

            else if (GameData.instance.power >= 2 && GameData.instance.power <= 3)
            {
                SoundManager.instance.PlaySE(9);
                if (transform.localScale == new Vector3(1, 1, 1))
                {
                    transform.localScale = new Vector3(0.5f, 0.5f, 1);
                }

                else if (transform.localScale == new Vector3(0.8f, 0.8f, 1))
                {
                    RandomNum();
                    Destroy(gameObject, 0.1f);
                }

                else if (transform.localScale == new Vector3(0.5f, 0.5f, 1))
                {
                    RandomNum();
                    Destroy(gameObject, 0.1f);
                }
            }

            else if (GameData.instance.power >= 4)
            {
                SoundManager.instance.PlaySE(10);
                RandomNum();
                Destroy(gameObject, 0.1f);
            }
        }
    }

    void RandomNum()
    {
        int num = Random.Range(0, 100);

        if (num < 10)
        {
            int r = Random.Range(0, items.Length);
            Instantiate(items[r], transform.position, Quaternion.identity);
        }
    }
}
