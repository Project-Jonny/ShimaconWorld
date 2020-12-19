using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public GameObject[] bossBullet;
    public GameObject pien;

    public enum DIRECTION_TYPE
    {
        STOP,
        RIGHT,
        LEFT,
    }

    DIRECTION_TYPE direction = DIRECTION_TYPE.STOP;

    Rigidbody2D rb;
    SpriteRenderer sr;
    float speed;

    bool bossDead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        direction = DIRECTION_TYPE.LEFT;

        StartCoroutine(InstDrag());
        StartCoroutine(InstDrag2());
    }

    IEnumerator InstDrag()
    {
        yield return new WaitForSeconds(1);

        while (!bossDead)
        {
            yield return new WaitForSeconds(3);

            direction = DIRECTION_TYPE.STOP;

            for (int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(0.2f);
                PienBullet();
            }

            yield return new WaitForSeconds(0.2f);

            if (transform.localScale == new Vector3(1, 1, 1))
            {
                direction = DIRECTION_TYPE.RIGHT;
            }
            else
            {
                direction = DIRECTION_TYPE.LEFT;
            }
        }
    }

    IEnumerator InstDrag2()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            int x = Random.Range(-8, 8);
            int r = Random.Range(0, bossBullet.Length);
            Instantiate(bossBullet[r], new Vector3(x, transform.position.y, 0), Quaternion.identity);
        }
    }

    void ChangeDirection()
    {
        if (direction == DIRECTION_TYPE.RIGHT)
        {
            direction = DIRECTION_TYPE.LEFT;
        }

        else if (direction == DIRECTION_TYPE.LEFT)
        {
            direction = DIRECTION_TYPE.RIGHT;
        }
    }

    private void FixedUpdate()
    {
        switch (direction)
        {
            case DIRECTION_TYPE.STOP:
                speed = 0;
                break;

            case DIRECTION_TYPE.RIGHT:
                transform.localScale = new Vector3(1, 1, 1);
                speed = 5;
                break;

            case DIRECTION_TYPE.LEFT:
                transform.localScale = new Vector3(-1, 1, 1);
                speed = -5;
                break;
        }

        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            ChangeDirection();
        }
    }

    void PienBullet()
    {
        if (transform.localScale == new Vector3(1, 1, 1))
        {
            Instantiate(pien, new Vector3(transform.position.x - 0.2f, transform.position.y - 0.2f, 0), Quaternion.identity);
            Instantiate(pien, new Vector3(transform.position.x + 0.3f, transform.position.y - 0.2f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(pien, new Vector3(transform.position.x + 0.2f, transform.position.y - 0.2f, 0), Quaternion.identity);
            Instantiate(pien, new Vector3(transform.position.x - 0.3f, transform.position.y - 0.2f, 0), Quaternion.identity);
        }
    }
}
