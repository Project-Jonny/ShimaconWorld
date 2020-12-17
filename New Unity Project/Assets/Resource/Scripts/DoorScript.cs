using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] Sprite[] doorSprites;
    public SpriteRenderer spriteRenderer;

    public GameObject[] enemys;
    public GameObject bear;

    void Start()
    {
        CloseDoor();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerMove>().bear == true)
            {
                bear = GameObject.FindGameObjectWithTag("Bear");
                enemys = GameObject.FindGameObjectsWithTag("Enemy");

                for (int i = 0; i < enemys.Length; i++)
                {
                    enemys[i].GetComponent<EnemyMove>().DoorClose();
                }

                spriteRenderer.sprite = doorSprites[1];

                collision.gameObject.SetActive(false);
                bear.SetActive(false);

                Invoke("CloseDoor", 0.8f);
            }
        }
    }

    void CloseDoor()
    {
        spriteRenderer.sprite = doorSprites[0];
    }
}
