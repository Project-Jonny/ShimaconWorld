using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShot : MonoBehaviour
{
    Vector3 direction;
    float shotSpeed = 3;

    void Update()
    {
        transform.position += direction * shotSpeed * Time.deltaTime;
    }

    public void SetDirecion(Vector2 enemyPos)
    {
        direction = enemyPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Ball")
        {
            Destroy(gameObject);
        }
    }

}
