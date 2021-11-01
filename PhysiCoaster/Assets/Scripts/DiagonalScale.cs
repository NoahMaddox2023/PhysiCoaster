using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DiagonalScale : MonoBehaviour
{
    private Vector3 newScale;

    void Start()
    {
        newScale = transform.localScale;
        newScale.x = newScale.x * (float)Math.Sqrt(2);
        transform.localScale = newScale;
    }

    
}
