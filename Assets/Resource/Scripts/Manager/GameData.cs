using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int lifeCount;
    public int power;
    public int playerSpeed;

    public bool dead = false;
    public bool bonus = false;
    public bool boss = false;

    void Start()
    {
        lifeCount = 3;
        power = 0;
        playerSpeed = 2;
    }

    void Update()
    {
        if (power >= 4)
        {
            power = 4;
        }
        else if (power <= 0)
        {
            power = 0;
        }

        if (lifeCount <= 0)
        {
            lifeCount = 0;
        }

        if (playerSpeed >= 5)
        {
            playerSpeed = 5;
        }
    }
}
