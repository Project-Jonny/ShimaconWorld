using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRotation : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 12f);
    }

    void Update()
    {
        transform.position += new Vector3(0, -0.02f, 0);
        transform.Rotate(new Vector3(0, 0, 2));
    }
}
