using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacement : MonoBehaviour
{
    public float gridSize = 1.0f;
    public GameObject track;

    private bool placeMode;

    void Start()
    {
        placeMode = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (placeMode)
                {
                    PlaceTrack(hit.point);
                } else if (!placeMode)
                {
                    DestroyTrack(hit.point);
                }
            }
        }

        //Press 1 to switch toggle placing tracks. Press 2 to toggle deleting tracks
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            placeMode = true;
            Debug.Log("Place Mode enabled.");
        } else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            placeMode = false;
            Debug.Log("Destroy Mode enabled.");
        }
    }

    private void PlaceTrack(Vector3 nearestPoint)
    {
        var placementPos = GetNearestPoint(nearestPoint);

        if (Physics.CheckBox(placementPos, new Vector3(0.25f, 0.25f, 0.25f)))
        {
            Debug.Log("A track already exists on that tile.");
        } else
        {
            Instantiate(track, placementPos, Quaternion.identity);
        }
    }

    private void DestroyTrack (Vector3 nearestPoint)
    {
        var destroyPos = GetNearestPoint(nearestPoint);

        if(Physics.CheckBox(destroyPos, new Vector3(0.25f, 0.25f, 0.25f)))
        {
            Collider[] hits = Physics.OverlapBox(destroyPos, new Vector3(0.25f, 0.25f, 0.25f));
            Destroy(hits[0].gameObject);
        } else
        {
            Debug.Log("There is no track to destroy on that tile.");
        }
    }

    private Vector3 GetNearestPoint(Vector3 pos)
    {
        int xCoord = Mathf.RoundToInt(pos.x / gridSize);
        int yCoord = Mathf.RoundToInt(pos.y / gridSize);
        int zCoord = Mathf.RoundToInt(pos.z / gridSize);

        Vector3 result = new Vector3((float)xCoord * gridSize, (float)yCoord * gridSize, (float)zCoord * gridSize);
        result += transform.position;

        return result;
    }
}
