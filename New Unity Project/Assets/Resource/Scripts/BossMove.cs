using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossMove : MonoBehaviour
{
    public GameObject[] bossBullet;
    public GameObject pien;

    int hp;

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

        hp = 250;

        direction = DIRECTION_TYPE.LEFT;

        StartCoroutine(InstDrag());
        StartCoroutine(InstDrag2());
    }

    void Update()
    {
        if (!bossDead)
        {
            if (hp < 200 && hp >= 150)
            {
                sr.color = new Color32(255, 200, 200, 255);
            }
            else if (hp < 150 && hp >= 100)
            {
                sr.color = new Color32(255, 150, 150, 255);
            }
            else if (hp < 100 && hp >= 50)
            {
                sr.color = new Color32(220, 75, 75, 255);
            }
            else if (hp < 50 && hp > 0)
            {
                sr.color = new Color32(180, 50, 50, 255);
            }
            else if (hp <= 0)
            {
                GameData.instance.boss = true;
                bossDead = true;
                Destroy(gameObject.GetComponent<CircleCollider2D>());
            }
        }
        else if (bossDead)
        {
            direction = DIRECTION_TYPE.STOP;
            sr.color -= new Color32(0, 0, 0, 1);
        }

        if (sr.color == new Color32(180, 50, 50, 0))
        {
            sr.color = new Color32(255, 255, 255, 0);
            FadeIOManager.instance.FadeOutToIn(() => Move());
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
        while (!bossDead)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            ChangeDirection();
        }

        if (collision.gameObject.tag == "Bullet")
        {
            if (GameData.instance.power <= 1)
            {
                hp -= 5;
            }

            else if (GameData.instance.power >= 2 && GameData.instance.power <= 3)
            {
                hp -= 8;
            }

            else if (GameData.instance.power >= 4)
            {
                hp -= 10;
            }
        }
    }

    void Move()
    {
        SceneManager.LoadScene("Clear");
    }
}
