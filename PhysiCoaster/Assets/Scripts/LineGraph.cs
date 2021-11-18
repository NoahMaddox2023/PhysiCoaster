using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineGraph : MonoBehaviour
{
    public GameObject potentialPoint, kineticPoint;
    public GameObject graphBackdrop;

    private List<float> potential, kinetic;
    private float yAxisMax, xAxisMax;
    private Vector3 origin;
    private int xIncrement;

    void Start()
    {
        potential = DataTransfer.potentialStored;
        kinetic = DataTransfer.kineticStored;
        origin = new Vector3(-255.0f, -175.0f, 0.0f);
        xIncrement = 0;

        foreach (int element in potential)
        {
            GameObject pPoint = Instantiate(potentialPoint);
            pPoint.transform.SetParent(graphBackdrop.transform);
            pPoint.transform.position = origin + new Vector3(51 * xIncrement, (element / 367.5f) * 170,0.0f);
            xIncrement++;
        }

        xIncrement = 0;

        foreach(int element in kinetic)
        {
            GameObject kPoint = Instantiate(kineticPoint);
            kPoint.transform.SetParent(graphBackdrop.transform);
            kPoint.transform.position = origin + new Vector3(51 * xIncrement, (element / 367.5f) * 170, 0.0f);
        }
    }
}
