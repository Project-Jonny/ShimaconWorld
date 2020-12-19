using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public GameObject[] life;

    void Update()
    {
        if (GameData.instance.lifeCount == 3)
        {
            for (int i = 0; i < life.Length; i++)
            {
                life[i].SetActive(true);
            }
        }
        else if (GameData.instance.lifeCount == 2)
        {
            LifeOff();
            life[0].SetActive(true);
            life[1].SetActive(true);
        }
        else if (GameData.instance.lifeCount == 1)
        {
            LifeOff();
            life[0].SetActive(true);
        }
        else if (GameData.instance.lifeCount == 0)
        {
            LifeOff();
            GameData.instance.dead = true;
        }
    }

    void LifeOff()
    {
        life[0].SetActive(false);
        life[1].SetActive(false);
        life[2].SetActive(false);
    }
}
