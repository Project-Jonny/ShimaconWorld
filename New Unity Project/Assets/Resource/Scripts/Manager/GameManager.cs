using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timerText;
    public Text enemyText;

    public float timerCount;
    public int enemyCount;

    public int spawnCount;

    public GameObject[] enemySpawn;

    public bool enemyDown;

    public GameObject door;

    public GameObject gameOver;

    void Start()
    {
        timerCount = 60;
        enemyCount = 20;
        spawnCount = enemyCount;

        timerText.text = timerCount.ToString("f0");
        enemyText.text = enemyCount.ToString("f0");

        gameOver.SetActive(false);
    }

    void Update()
    {
        enemyText.text = enemyCount.ToString("f0");

        if (timerCount > 0)
        {
            timerCount -= Time.deltaTime;
            timerText.text = timerCount.ToString("f0");
        }

        else if (timerCount <= 0)
        {
            timerCount = 0;
            timerText.text = timerCount.ToString("f0");

            gameOver.SetActive(true);
            // timeUp
        }

        if (enemyCount <= 5)
        {
            door.SetActive(true);
        }

        if (GameData.instance.lifeCount == 0)
        {
            gameOver.SetActive(true);
        }
    }

    public void Search()
    {
        enemySpawn = GameObject.FindGameObjectsWithTag("EnemySpawn");
        int r = Random.Range(0, enemySpawn.Length);
        enemySpawn[r].GetComponent<EnemySpawn>().Spawn();
    }
}
