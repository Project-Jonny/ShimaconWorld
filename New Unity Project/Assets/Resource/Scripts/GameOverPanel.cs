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
    }
}
