using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTransfer : MonoBehaviour
{
    public static List<float> potentialStored, kineticStored;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
