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
        if (GameData.instance.power <= 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            bulletEffect.transform.localScale = new Vector3(1, 1, 1);
        }

        else if (GameData.instance.power >= 2 && GameData.instance.power <= 3)
        {
            transform.localScale = new Vector3(1.2f, 1.2f, 1);
            bulletEffect.transform.localScale = new Vector3(1.2f, 1.2f, 1);
        }

        else if (GameData.instance.power >= 4)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
            bulletEffect.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }

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
        if (collision.gameObject.tag == "Block" || collision.gameObject.tag == "Wall" ||collision.gameObject.tag == "Enemy")
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

        if (collision.gameObject.tag == "Arrow")
        {
            Instantiate(bulletEffect, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
