﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public void OnCompleteAni()
    {
        Destroy(gameObject);
    }
}
