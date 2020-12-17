using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject spawnEffect;
    public GameObject[] enemy;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        StartCoroutine(SetEnemy());
    }

    IEnumerator SetEnemy()
    {
        gameManager.spawnCount--;
        yield return new WaitForSeconds(1f);
        int number = Random.Range(0, enemy.Length);
        Instantiate(spawnEffect, transform.position + new Vector3(-0.3f,0.3f), Quaternion.identity);
        // SoundManager.instance.PlayOneShot(0);
        yield return new WaitForSeconds(0.2f);
        Instantiate(spawnEffect, transform.position + new Vector3(0.3f, -0.3f), Quaternion.identity);
        // SoundManager.instance.PlayOneShot(0);
        yield return new WaitForSeconds(0.2f);
        Instantiate(spawnEffect, transform.position + new Vector3(-0.3f, -0.3f), Quaternion.identity);
        // SoundManager.instance.PlayOneShot(0);
        yield return new WaitForSeconds(0.2f);
        Instantiate(spawnEffect, transform.position + new Vector3(0.3f, 0.3f), Quaternion.identity);
        // SoundManager.instance.PlayOneShot(0);
        yield return new WaitForSeconds(0.2f);
        Instantiate(spawnEffect, transform.position, Quaternion.identity);
        // SoundManager.instance.PlayOneShot(0);
        yield return new WaitForSeconds(0.2f);
        Instantiate(enemy[number], transform.position, transform.rotation);
    }

    public void Spawn()
    {
        if (gameManager.spawnCount > 0)
        {
            StartCoroutine(SetEnemy());
        }
    }
}
