using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public string scene = "";

    public void OnButton()
    {
        FadeIOManager.instance.FadeOutToIn(() => Move());
    }

    public void ToStart()
    {
        FadeIOManager.instance.FadeOutToIn(() => Back());
    }

    void Move()
    {
        SceneManager.LoadScene(scene);
    }

    void Back()
    {
        SceneManager.LoadScene("Title");
        GameData.instance.lifeCount = 3;
        GameData.instance.power = 0;
        GameData.instance.playerSpeed = 2;
        GameData.instance.dead = false;
        GameData.instance.boss = false;
    }
}