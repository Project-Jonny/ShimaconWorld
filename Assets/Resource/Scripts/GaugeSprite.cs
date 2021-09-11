using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeSprite : MonoBehaviour
{
    public Sprite[] gaugeSprite;
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = gaugeSprite[GameData.instance.power];
    }

    void Update()
    {
        if (GameData.instance.power >= 4)
        {
            image.sprite = gaugeSprite[4];
        }
        else if (GameData.instance.power == 3)
        {
            image.sprite = gaugeSprite[3];
        }
        else if (GameData.instance.power == 2)
        {
            image.sprite = gaugeSprite[2];
        }
        else if (GameData.instance.power == 1)
        {
            image.sprite = gaugeSprite[1];
        }
        else if (GameData.instance.power <= 0)
        {
            image.sprite = gaugeSprite[0];
        }
    }
}
