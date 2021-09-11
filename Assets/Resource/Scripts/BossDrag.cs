using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDrag : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(0, -0.02f, 0);
        transform.Rotate(new Vector3(0, 0, 2));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
