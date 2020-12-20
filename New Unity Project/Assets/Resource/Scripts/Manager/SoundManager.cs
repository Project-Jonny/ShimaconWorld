using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSourceBGM;
    public AudioClip[] audioClipsBGM;
    public AudioSource audioSourceSE;
    public AudioClip[] audioClipsSE;

    public void StopBGM()
    {
        audioSourceBGM.Stop();
    }

    public static SoundManager instance;

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

    string currentBGM = "";

    private void Start()
    {
        PlayBGM("Title");
    }

    public void PlayBGM(string sceneName)
    {
        if (currentBGM == sceneName)
        {
            return;
        }
        else
        {
            currentBGM = sceneName;
        }

        audioSourceBGM.Stop();
        switch (sceneName)
        {
            default:
            case "Title":
                audioSourceBGM.clip = audioClipsBGM[0];
                break;
            case "GameBGM":
                audioSourceBGM.clip = audioClipsBGM[1];
                break;
            case "Choco":
                audioSourceBGM.clip = audioClipsBGM[2];
                break;
            case "Boss":
                audioSourceBGM.clip = audioClipsBGM[3];
                break;
            case "Clear":
                audioSourceBGM.clip = audioClipsBGM[4];
                break;
        }
        audioSourceBGM.Play();
    }

    public void PlaySE(int index)
    {
        audioSourceSE.PlayOneShot(audioClipsSE[index]);
    }
}