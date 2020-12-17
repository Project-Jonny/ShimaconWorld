using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    bool setChase = false;
    bool inputR = false;

    Animator animator;

    public PlayerMove target;
    Vector2 direction;

    Vector2 lastMove;

    public GameObject icon;
    public GameObject heartIcon;

    float timer;

    public bool emotional = false;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Anim();

        if (setChase)
        {
            timer = 0;
            emotional = false;
            icon.SetActive(false);
            heartIcon.SetActive(true);
            animator.SetBool("Angry", false);

            if (Vector2.Distance(transform.position, target.oldPos) > 1)
            {
                direction = target.oldPos - (Vector2)(transform.position);
                transform.position = Vector2.MoveTowards(transform.position, target.oldPos, 5 * Time.deltaTime);
            }
            else
            {
                direction = Vector2.zero;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                heartIcon.SetActive(false);
                direction = Vector2.zero;
                setChase = false;
                target.bear = false;
            }
        }
        if (inputR && !setChase)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                setChase = true;
                target.bear = true;
            }
        }

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        }

        if (!setChase)
        {
            timer += Time.deltaTime;

            if (timer >= 10)
            {
                emotional = true;
                icon.SetActive(true);
                animator.SetBool("Angry", true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inputR = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inputR = false;
        }
    }

    void Anim()
    {
        if (Mathf.Abs(direction.x) > 0.5f)
        {
            lastMove.x = direction.x;
            lastMove.y = 0;
        }
        if (Mathf.Abs(direction.y) > 0.5f)
        {
            lastMove.y = direction.y;
            lastMove.x = 0;
        }

        animator.SetFloat("Dir_X", direction.x);
        animator.SetFloat("Dir_Y", direction.y);
        animator.SetFloat("LastMove_X", lastMove.x);
        animator.SetFloat("LastMove_Y", lastMove.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }
}