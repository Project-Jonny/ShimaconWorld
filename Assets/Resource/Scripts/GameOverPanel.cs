using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    string scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            FadeIOManager.instance.FadeOutToIn(() => Move());
        }
    }

    void Move()
    {
        SceneManager.LoadScene(scene);
        GameData.instance.lifeCount = 3;
        GameData.instance.power = 0;
        GameData.instance.playerSpeed = 2;
        GameData.instance.dead = false;
        GameData.instance.boss = false;
    }
}
