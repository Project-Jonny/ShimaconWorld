using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        SceneManager.LoadScene("Stage 1");
        GameData.instance.lifeCount = 3;
        GameData.instance.power = 0;
        GameData.instance.playerSpeed = 2;
        GameData.instance.dead = false;
        GameData.instance.bonus = false;
    }
}
