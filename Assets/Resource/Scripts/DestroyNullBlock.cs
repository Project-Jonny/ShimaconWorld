﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNullBlock : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.1f);
    }
}
