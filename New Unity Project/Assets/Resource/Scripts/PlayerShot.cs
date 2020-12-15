using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    Vector3 direction;
    float shotSpeed = 7;

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
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Block" || collision.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
