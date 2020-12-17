using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    Vector3 direction;
    float shotSpeed = 7;

    public GameObject bulletEffect;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.position += direction * shotSpeed * Time.deltaTime;
    }

    public void SetDirecion(Vector2 playerPos)
    {
        direction = playerPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "EnemyBullet")
        {
            Instantiate(bulletEffect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
