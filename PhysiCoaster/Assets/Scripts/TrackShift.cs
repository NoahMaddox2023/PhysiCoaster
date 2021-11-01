using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackShift : MonoBehaviour
{
    void Start()
    {
        if (this.gameObject.layer != LayerMask.NameToLayer("UI"))
        {
            transform.position -= new Vector3(0.0f, 0.75f, 0.0f);
        }
    }
}
