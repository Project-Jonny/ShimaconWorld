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
            StartCoroutine(RandomMove());
            StartCoroutine(Shooting());
        }
    }

    IEnumerator RandomMove()
    {
        while (true)
        {
            yield return null;

            while (target.emotional == false)
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
        }
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
        if (target == null)
        {
            return;
        }

        if (target.emotional == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 1 * Time.deltaTime);
            animator.SetFloat("Direction_X", target.transform.position.x - transform.position.x);
            animator.SetFloat("Direction_Y", target.transform.position.y - transform.position.y);
        }
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
