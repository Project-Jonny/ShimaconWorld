using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager : MonoBehaviour
{
    public GameObject[] drags;

    void Start()
    {
        StartCoroutine(InstDrag());
        StartCoroutine(InstDrag2());
    }

    IEnumerator InstDrag()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.8f);
            int x = Random.Range(-9, 9);
            int r = Random.Range(0, drags.Length);
            Instantiate(drags[r],new Vector3(x,5.5f,0),Quaternion.identity);
        }
    }

    IEnumerator InstDrag2()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            int x = Random.Range(-8, 8);
            int r = Random.Range(0, drags.Length);
            Instantiate(drags[r], new Vector3(x, 5.5f, 0), Quaternion.identity);
        }
    }
}
