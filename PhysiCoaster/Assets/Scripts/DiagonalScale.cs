using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DiagonalScale : MonoBehaviour
{
    private Vector3 newScale;

    void Start()
    {

        if (this.gameObject.layer != LayerMask.NameToLayer("UI"))
        {
            newScale = transform.localScale;
            newScale.x = newScale.x * (float)Math.Sqrt(2);
            transform.localScale = newScale;
        }
    }

    
}
