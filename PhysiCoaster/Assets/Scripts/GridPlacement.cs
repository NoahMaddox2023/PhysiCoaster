using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GridPlacement : MonoBehaviour
{
    public float gridSize = 1.0f;
    [SerializeField]
    public GameObject[] track;

    private int currentTrack;
    private bool placeMode;

    void Start()
    {
        placeMode = true;
        currentTrack = 0;
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
            GameObject rail = Instantiate(track[currentTrack], placementPos, track[currentTrack].transform.rotation);
        }
    }

    private void DestroyTrack (Vector3 nearestPoint)
    {
        var destroyPos = GetNearestPoint(nearestPoint) - new Vector3(0.0f, 0.25f, 0.0f);

        if(Physics.CheckBox(destroyPos, new Vector3(0.25f, 0.5f, 0.25f)))
        {
            Collider[] hits = Physics.OverlapBox(destroyPos, new Vector3(0.25f, 0.5f, 0.25f));
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
        //int zCoord = Mathf.RoundToInt(pos.z / gridSize);

        Vector3 result = new Vector3((float)xCoord * gridSize, (float)yCoord * gridSize, 1.5f);
        result += transform.position;

        return result;
    }

    public void EquipTrack(int trackType)
    {
        switch (trackType)
        {
            case 0:
                currentTrack = 0;
                break;
            case 1:
                currentTrack = 1;
                break;
            case 2:
                currentTrack = 2;
                break;
        }
    }

    /*
    //This method is for debugging only. Uncomment if you need to visualize the grid in the editor. Otherwise, Unity will crash upon running the game a second time.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                var point = GetNearestPoint(new Vector3(x, y, 0.0f));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }*/
}
