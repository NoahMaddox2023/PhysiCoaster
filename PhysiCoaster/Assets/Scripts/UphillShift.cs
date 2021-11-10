using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UphillShift : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.layer != LayerMask.NameToLayer("UI"))
        {
            transform.position -= new Vector3(-0.75f, 0.70f, 0.0f);
        }
    }
}
