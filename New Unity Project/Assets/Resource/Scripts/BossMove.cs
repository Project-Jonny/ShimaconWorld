using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public GameObject[] bossBullet;

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
    }

    IEnumerator InstDrag()
    {
        while (!bossDead)
        {
            yield return new WaitForSeconds(0.8f);
            int r = Random.Range(0, bossBullet.Length);
            Instantiate(bossBullet[r], transform.position, Quaternion.identity);
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
}
