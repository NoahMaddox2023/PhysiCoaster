using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RideTrack : MonoBehaviour
{
    public float checkDistance;
    public float speed;
    public float offset;
    bool brokenTrack;
    Transform lastTrackPosition;
    Vector3 lastTrackNormal;
    Vector3[] directions;
    RaycastHit[] hit;
    // Start is called before the first frame update
    void Start()
    {
        //populate the array with the directions we want to check with raycasts to see if there are blocks in front of us, or below 
        directions = new Vector3[]
        {
            Vector3.right, //for straight forward
            Vector3.right + Vector3.down, //for diagonally down
            Vector3.down // for straight down
        };
    }

    // Update is called once per frame
    void Update()
    {
        checkTracks();
    }

    //this shoots out raycasts from our current position and checks to see if any of the raycasts collide with a  track piece. If it
    // does we'll store the location of that collider and go towards that tracks position using lerp.
    void checkTracks()
    {
        hit = new RaycastHit[directions.Length];
        for (int i = 0; i < directions.Length; i++)
        {
            //The direction of the current direction that we are currently on in our array
            Vector3 dir = transform.TransformDirection(directions[i]);


            //Shoot out a raycast in that specific direction, and out however far away we set in the editor. Can also change the value in here if need be. 
            Physics.Raycast(transform.position, dir, out hit[i], checkDistance);
            if (hit[i].collider != null)
            {
                //debug the ray so we can see it in the scene, and turn it green if it collides with something
                Debug.DrawRay(transform.position, dir * hit[i].distance, Color.green);
            }
            else
            {
                //if we didnt hit anything, keep the raycasts red
                Debug.DrawRay(transform.position, dir * checkDistance, Color.red);
            }
            if(hit == null)
            {
                //if the track is not connected and all of the colliders at any point dont get a hit back, broken track turns true. 
                brokenTrack = true;
            }
        }
        if (!brokenTrack)
        {
            //if we're not on a broken track we'll order our hits by the closest one to us, 
            hit = hit.ToList().Where(h => h.collider != null).OrderBy(h => h.distance).ToArray();
            if(hit.Length > 0)
            {
                GoToTrack(hit[0]);
                lastTrackPosition = hit[0].transform;
                lastTrackNormal = hit[0].normal;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //set a target position for our cart to go to, based on th closest collision to the cart
    void GoToTrack(RaycastHit hitPosition)
    {
        Vector3 target = new Vector3(hitPosition.transform.position.x, hitPosition.transform.position.y + offset, hitPosition.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
    }
}
