﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillBar : MonoBehaviour
{
    public GameObject Bar;

    public void SetFill(float amount)
    {
        Vector3 scale = Bar.transform.localScale;
        scale.x = amount;
        Bar.transform.localScale = scale;
    }
}