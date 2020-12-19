using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
       　if (SceneManager.GetActiveScene().name == "Stage 5" || SceneManager.GetActiveScene().name == "Stage 6")
        {
            GameData.instance.bonus = true;
        }
    }

    void Start()
    {
        enemyCount = 20;
        spawnCount = enemyCount;

        timerText.text = timerCount.ToString("f0");
        enemyText.text = enemyCount.ToString("f0");

        gameOver.SetActive(false);
    }

    void Update()
    {
        enemyText.text = enemyCount.ToString("f0");

        if (GameData.instance.dead)
        {
            gameOver.SetActive(true);
        }
        else
        {
            if (timerCount > 0)
            {
                timerCount -= Time.deltaTime;
                timerText.text = timerCount.ToString("f0");
            }

            else if (timerCount <= 0)
            {
                timerCount = 0;
                timerText.text = timerCount.ToString("f0");

                if (GameData.instance.bonus)
                {
                    FadeIOManager.instance.FadeOutToIn(() => Move());
                }
                else
                {
                    gameOver.SetActive(true);
                    GameData.instance.dead = true;
                }

            }
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

    void Move()
    {
        SceneManager.LoadScene("Stage 6");
    }
}
