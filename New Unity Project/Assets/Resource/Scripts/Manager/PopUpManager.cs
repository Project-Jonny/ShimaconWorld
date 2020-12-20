using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopUpManager : MonoBehaviour
{
    [SerializeField]
    RectTransform rectTran;

    public void ScaleOne()
    {
        SoundManager.instance.PlaySE(12);

        //スケール（1,1,1）にスケーリング
        rectTran.DOScale(
            Vector3.one,  //終了時点のScale
            0.2f          //時間
            );
    }

    public void ScaleZero()
    {
        SoundManager.instance.PlaySE(13);

        //スケール（0,0,0）にスケーリング
        rectTran.DOScale(
            Vector3.zero,  //終了時点のScale
            0.2f           //時間
            );
    }
}
