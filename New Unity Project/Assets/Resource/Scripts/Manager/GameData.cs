using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public GameObject[] life;
    public GameObject gauge;

    public Sprite[] gaugeSprite;
    Image sp;

    public int lifeCount;
    public int power;

    void Start()
    {
        lifeCount = 3;
        power = 0;

        sp = gauge.GetComponent<Image>();
        sp.sprite = gaugeSprite[0];
    }

    void Update()
    {
        if (lifeCount == 3)
        {
            for (int i = 0; i < life.Length; i++)
            {
                life[i].SetActive(true);
            }
        }
        else if (lifeCount == 2)
        {
            LifeOff();
            life[0].SetActive(true);
            life[1].SetActive(true);
        }
        else if (lifeCount == 1)
        {
            LifeOff();
            life[0].SetActive(true);
        }
        else if (lifeCount == 0)
        {
            LifeOff();
        }

        if (power >= 4)
        {
            power = 4;
        }
        else if (power <= 0)
        {
            power = 0;
        }

        sp.sprite = gaugeSprite[power];
    }

    void LifeOff()
    {
        life[0].SetActive(false);
        life[1].SetActive(false);
        life[2].SetActive(false);
    }
}
