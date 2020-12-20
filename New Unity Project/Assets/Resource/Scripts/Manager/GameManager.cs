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

    bool hoge = false;

    public GameObject door;

    public GameObject gameOver;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Stage 5" || SceneManager.GetActiveScene().name == "Stage 6")
        {
            GameData.instance.bonus = true;
        }
        else
        {
            GameData.instance.bonus = false;
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Stage 1" || SceneManager.GetActiveScene().name == "Stage 2" || SceneManager.GetActiveScene().name == "Stage 3" || SceneManager.GetActiveScene().name == "Stage 4")
        {
            SoundManager.instance.PlayBGM("GameBGM");
        }
        else if (SceneManager.GetActiveScene().name == "Stage 5")
        {
            SoundManager.instance.PlayBGM("Choco");
        }
        else if (SceneManager.GetActiveScene().name == "Stage 6")
        {
            SoundManager.instance.PlayBGM("Boss");
        }

        enemyCount = 20;
        spawnCount = enemyCount;

        timerText.text = timerCount.ToString("f0");

        if (GameData.instance.bonus)
        {
            enemyText.text = "-";
        }
        else
        {
            enemyText.text = enemyCount.ToString("f0");
        }

        gameOver.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FadeIOManager.instance.FadeOutToIn(() => Quit());
        }

        if (GameData.instance.bonus)
        {
            enemyText.text = "-";
        }
        else
        {
            enemyText.text = enemyCount.ToString("f0");
        }

        if (GameData.instance.dead)
        {
            gameOver.SetActive(true);
        }
        else
        {
            if (!GameData.instance.boss)
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
                        if (!hoge)
                        {
                            hoge = true;
                            FadeIOManager.instance.FadeOutToIn(() => Move());
                        }
                    }
                    else
                    {
                        gameOver.SetActive(true);
                        GameData.instance.dead = true;
                    }

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

    void Quit()
    {
        SceneManager.LoadScene("Title");
        GameData.instance.lifeCount = 3;
        GameData.instance.power = 0;
        GameData.instance.playerSpeed = 2;
        GameData.instance.dead = false;
        GameData.instance.boss = false;
    }
}
