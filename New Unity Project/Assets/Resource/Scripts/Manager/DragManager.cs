using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    public GameObject[] drags;

    void Start()
    {
        
    }

    IEnumerator InstDrag()
    {
        yield return null;

        while (true)
        {
            int r = Random.Range(0, drags.Length);
            drags[r].GetComponent<EnemySpawn>().Spawn();
        }
    }
}
