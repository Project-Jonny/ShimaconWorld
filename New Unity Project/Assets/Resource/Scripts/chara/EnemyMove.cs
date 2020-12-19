using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 direction = Vector2.down;

    Animator animator;
    public GameObject DestroyAni;

    public GameObject enemyBullet;
    GameManager gameManager;

    Chase target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction;

        animator = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        GameObject bear = GameObject.FindGameObjectWithTag("Bear");
        if (bear != null)
        {
            target = GameObject.FindGameObjectWithTag("Bear").GetComponent<Chase>();
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            StartCoroutine(Move());
            StartCoroutine(Shooting());
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            yield return null;

            //ぷえんモード
            while (target.emotional == false)
            {
                yield return RandomMove();
            }
            //ぷえんモードを抜けると速度を0にする
            rb.velocity = Vector3.zero;
        }
    }

    IEnumerator RandomMove()
    {
        yield return new WaitForSeconds(1);
        int r = Random.Range(0, 4);
        switch (r)
        {
            case 0:
                direction = Vector2.up;
                break;
            case 1:
                direction = Vector2.down;
                break;
            case 2:
                direction = Vector2.left;
                break;
            case 3:
                direction = Vector2.right;
                break;
        }
        rb.velocity = direction;
        animator.SetFloat("Direction_X", direction.x);
        animator.SetFloat("Direction_Y", direction.y);
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            Shot();
        }
    }

    void Shot()
    {
        var shots = Instantiate(enemyBullet, transform.position, transform.rotation);
        shots.GetComponent<enemyShot>().SetDirecion(direction);
    }

    void Update()
    {
        if (GameData.instance.dead)
        {
            target.emotional = false;
        }
        else
        {
            if (target == null)
            {
                return;
            }

            if (target.emotional == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 1 * Time.deltaTime);
                animator.SetFloat("Direction_X", target.transform.position.x - transform.position.x);
                animator.SetFloat("Direction_Y", target.transform.position.y - transform.position.y);
                direction = Normalization(target.transform.position, transform.position); // 一旦向きを入れる:ここから発射方向を決める
            }
        }
    }

    // 斜め方向を上下左右に変換する
    Vector2 Normalization(Vector2 target, Vector2 self)
    {
        Vector2 dir = target - self; // 進行方向を取得する
        dir.Normalize();             // 長さを1にする
        // 4方向でもっとも近いものを取得する
        Vector2[] around = new Vector2[4] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        Vector2 minVec = around[0];
        float minDis = Vector2.Distance(dir, minVec);
        for (int i=1; i<around.Length; i++)
        {
            if (Vector2.Distance(dir, around[i]) < minDis)
            {
                // minDisより小さいなら最小を入れ替える
                minVec = around[i];
                minDis = Vector2.Distance(dir, minVec);
            }
        }
        return minVec;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (GameData.instance.power <= 1)
            {
                DestroyAni.transform.localScale = new Vector3(1, 1, 1);
            }

            else if (GameData.instance.power >= 2 && GameData.instance.power <= 3)
            {
                DestroyAni.transform.localScale = new Vector3(1.2f, 1.2f, 1);
            }

            else if (GameData.instance.power >= 4)
            {
                DestroyAni.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            }

            Instantiate(DestroyAni, transform.position, Quaternion.identity);
            gameManager.enemyCount--;
            gameManager.Search();

            Destroy(gameObject);
        }
    }

    public void DeathPanic()
    {
        Instantiate(DestroyAni, transform.position, Quaternion.identity);
        gameManager.enemyCount--;
        gameManager.Search();

        Destroy(gameObject);
    }

    public void DoorClose()
    {
        Instantiate(DestroyAni, transform.position, Quaternion.identity);
        gameManager.enemyCount = 0;
        Destroy(gameObject);
    }
}
