using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ojamaBlock : MonoBehaviour
{
    PlayerMove player;
    public Collider2D col;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<PlayerMove>();

            if (player.bear)
            {
                col.isTrigger = false;
            }
            else if (!player.bear)
            {
                col.isTrigger = true;
            }
        }

        if (collision.gameObject.tag == "Enemy")
        {
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<PlayerMove>();

            if (player.bear)
            {
                col.isTrigger = false;
            }
            else if (!player.bear)
            {
                col.isTrigger = true;
            }
        }

        if (collision.gameObject.tag == "EnemyBullet")
        {
            col.isTrigger = true;
        }
    }
}
