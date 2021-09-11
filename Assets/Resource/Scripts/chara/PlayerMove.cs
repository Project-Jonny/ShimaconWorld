using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    int moveSpeed;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;
    public Vector2 lastMove;
    public GameObject bullet;
    public Vector2 oldPos;

    public GameObject ball;
    public GameObject[] enemys;

    SpriteRenderer spriteRenderer;

    public bool bear = false;

    Chase chase;

    Vector2 firstPos;

    void Start()
    {
        moveSpeed = GameData.instance.playerSpeed;

        if (!GameData.instance.bonus)
        {
            chase = GameObject.FindGameObjectWithTag("Bear").GetComponent<Chase>();
        }

        spriteRenderer = GetComponent<SpriteRenderer>();

        firstPos = transform.position;
        StartCoroutine(SavePos());
        StartCoroutine(Flash());
        Invoke("BallOff", 5f);
    }

    void Update()
    {
        if (!GameData.instance.dead)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            Animate();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shot();
            }
        }
        else
        {
            gameObject.SetActive(false);
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void Animate()
    {
        if (Mathf.Abs(movement.x) > 0.5f)
        {
            lastMove.x = movement.x;
            lastMove.y = 0;
        }
        if (Mathf.Abs(movement.y) > 0.5f)
        {
            lastMove.y = movement.y;
            lastMove.x = 0;
        }

        animator.SetFloat("Dir_X", movement.x);
        animator.SetFloat("Dir_Y", movement.y);
        animator.SetFloat("LastMove_X", lastMove.x);
        animator.SetFloat("LastMove_Y", lastMove.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void Shot()
    {
        SoundManager.instance.PlaySE(11);
        var shots = Instantiate(bullet, transform.position, transform.rotation);
        shots.GetComponent<PlayerShot>().SetDirecion(lastMove);
    }

    IEnumerator SavePos()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            oldPos = transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cure")
        {
            SoundManager.instance.PlaySE(8);

            ball.SetActive(true);
            Destroy(collision.gameObject);
            Invoke("BallOff", 5f);
        }

        if (collision.gameObject.tag == "Broke")
        {
            enemys = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < enemys.Length; i++)
            {
                enemys[i].GetComponent<EnemyMove>().DeathPanic();
            }

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "EnemyBullet" && ball.activeSelf == false)
        {
            SoundManager.instance.PlaySE(5);

            chase.Byebye();
            StartCoroutine(Flash());
            GameData.instance.lifeCount--;
            GameData.instance.power = 0;
            GameData.instance.playerSpeed = 2;
            moveSpeed = GameData.instance.playerSpeed;
            transform.position = firstPos;
            ball.SetActive(true);
            Invoke("BallOff", 3f);
        }

        if (collision.gameObject.tag == "BossBullet" && ball.activeSelf == false)
        {
            SoundManager.instance.PlaySE(5);

            StartCoroutine(Flash());
            GameData.instance.lifeCount--;
            GameData.instance.power = 0;
            GameData.instance.playerSpeed = 2;
            moveSpeed = GameData.instance.playerSpeed;
            ball.SetActive(true);
            Invoke("BallOff", 3f);
            transform.position = firstPos;
        }

        if (collision.gameObject.tag == "dragUP")
        {
            SoundManager.instance.PlaySE(1);

            GameData.instance.power++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "dragDOWN")
        {
            SoundManager.instance.PlaySE(2);

            GameData.instance.power--;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Speed")
        {
            SoundManager.instance.PlaySE(7);

            GameData.instance.playerSpeed++;
            moveSpeed = GameData.instance.playerSpeed;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Boss")
        {
            SoundManager.instance.PlaySE(5);

            StartCoroutine(Flash());
            GameData.instance.lifeCount--;
            GameData.instance.power = 0;
            GameData.instance.playerSpeed = 2;
            moveSpeed = GameData.instance.playerSpeed;
            ball.SetActive(true);
            Invoke("BallOff", 3f);
            transform.position = firstPos;
        }
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if (ball.activeSelf == false)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                SoundManager.instance.PlaySE(5);

                chase.Byebye();
                StartCoroutine(Flash());
                GameData.instance.lifeCount--;
                GameData.instance.power = 0;
                GameData.instance.playerSpeed = 2;
                moveSpeed = GameData.instance.playerSpeed;
                ball.SetActive(true);
                Invoke("BallOff", 3f);
                transform.position = firstPos;
            }
        }
    }

    void BallOff()
    {
        ball.SetActive(false);
    }

    IEnumerator Flash()
    {
        int count = 0;
        while (count < 5)
        {
            // 消える
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.2f);
            yield return new WaitForSeconds(0.1f); // 0.1秒まて
            // つく
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.1f); // 0.1秒まて
            count++;
        }
    }
}
