using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideTrack : MonoBehaviour
{
    public float checkDistance;
    public float speed;
    public float lerpSpeed;
    public float positionLerpSpeed;
    public float offset;
    public float direction;
    public float rayTime;
    public bool madeDestination;
    public GameObject cart;
    bool move = false;
    bool brokenTrack;
    Transform lastTrackPosition;
    Vector3 lastTrackNormal;
    Vector3[] directions;
    Vector3[] downDirection;
    RaycastHit[] hit;
    RaycastHit[] downHit;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Press Q to move the cart");
        //populate the array with the directions we want to check with raycasts to see if there are blocks in front of us, or below 
        directions = new Vector3[]
        {
            Vector3.right, //for straight forward
            Vector3.right + Vector3.down, //for diagonally down
            Vector3.down // for straight down
        };
        downDirection = new Vector3[]
        {
            Vector3.down
        };
        rayTime = 0;
    }
    /*
    IEnumerator horizontalTrackSpeedChange(float seconds)
    {
        float counter = seconds; 
        while (counter > 0f)
        {
            yield return new WaitForSeconds(1f);
            counter--;
        }
        speed -= .5f;
        if (speed < 0)
        {
            speed = 0;
        }
    }
    IEnumerator inclineTrackSpeedChange(float seconds)
    {
        float counter = seconds;
        while (counter > 0f)
        {
            yield return new WaitForSeconds(1f);
            counter--;
        }
        speed -= 2;
    }

    IEnumerator declineTrackSpeedChange(float seconds)
    {
        float counter = seconds;
        while (counter > 0f)
        {
            yield return new WaitForSeconds(.001f);
            counter--;
        }
        speed += 1;
        if (speed > 5)
        {
            speed = 0;
        }
    }
    */
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            move = !move;
        }
        if (!brokenTrack && move)
        {
            moveCart();
            checkTracks();
            
            
        }
        
    }

    //this shoots out raycasts from our current position and checks to see if any of the raycasts collide with a  track piece. If it
    // does we'll store the location of that collider and go towards that tracks position using lerp.
    void checkTracks()
    {
        hit = new RaycastHit[directions.Length];
        downHit = new RaycastHit[downDirection.Length];
        for (int i = 0; i < downDirection.Length; i++)
        {
            Vector3 dir = transform.TransformDirection(downDirection[i]);
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), dir, out downHit[i], checkDistance);
            if (downHit[i].collider != null)
            {
                if (downHit[i].transform.CompareTag("HorizontalTrack"))
                {
                    speed -= .00075f;
                    if (speed < 0)
                    {
                        speed = 0;
                    }
                }
                else if (downHit[i].transform.CompareTag("InclineTrack"))
                {
                    speed -= .0025f;
                    if (speed < 0)
                    {
                        speed = 0;
                    }
                }
                else if (downHit[i].transform.CompareTag("DeclineTrack"))
                {
                    Debug.Log("Going downhill");
                    speed += .00075f;
                    if (speed > 5)
                    {
                        speed = 0;
                    }
                }
            }
        }
        for (int i = 0; i < directions.Length; i++)
        {
            //The direction of the current direction that we are currently on in our array
            Vector3 dir = transform.TransformDirection(directions[i]);

            int hits = 0;
            //Shoot out a raycast in that specific direction, and out however far away we set in the editor. Can also change the value in here if need be. 
            Physics.Raycast(transform.position, dir, out hit[i], checkDistance);
            if (hit[i].collider != null)
            {
                
                //debug the ray so we can see it in the scene, and turn it green if it collides with something
                Debug.DrawRay(transform.position, dir * hit[i].distance, Color.green);
                hits += 1;
                
            }
            else
            {
                //if we didnt hit anything, keep the raycasts red
                Debug.DrawRay(transform.position, dir * checkDistance, Color.red);
                hits -= 1;
            }

            //if (hits <= 0)
            //{
            //    //if the track is not connected and all of the colliders at any point dont get a hit back, broken track turns true. 
            //    brokenTrack = true;
            //}
            //Debug.Log("Amount of hits, max of 3 min of 0: " + hits);
        }
        
        if (!brokenTrack)
        {
            //if we're not on a broken track we'll order our hits by the closest one to us, 
            hit = hit.ToList().Where(h => h.collider != null).OrderBy(h => h.distance).ToArray();
            if(hit.Length > 0)
            {
                if(lastTrackPosition != hit[0].transform)
                {
                    GoToTrack(hit[0]);
                }
                if (madeDestination)
                {
                    lastTrackPosition = hit[0].transform;
                    madeDestination = false;
                }
                RotateTrack(hit[0]);
                
                
                //lastTrackNormal = hit[0].normal;
            }
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log("Speed is: " + speed);
    }


    //set a target position for our cart to go to, based on th closest collision to the cart
    void GoToTrack(RaycastHit hitPosition)
    {
        Vector3 target = new Vector3(transform.position.x, hitPosition.transform.position.y + offset, hitPosition.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, target, positionLerpSpeed * Time.deltaTime);
        if(transform.position == target)
        {
            Debug.Log("I have made my destination");
            madeDestination = true;
        }
        //Debug.Log("This is the position when going to a track: " + transform.position);
    }
    void RotateTrack(RaycastHit hitNormal)
    {
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hitNormal.normal);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, lerpSpeed * Time.deltaTime);
    }
    void moveCart()
    {
        if (speed > 0)
        {
            Vector3 move = new Vector3(speed * Time.deltaTime, 0, 0);
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else
        {
            Vector3 move = new Vector3(-speed * Time.deltaTime, 0, 0);
            transform.position -= transform.right * speed * Time.deltaTime;
        }
        
        //transform.Translate(speed * Time.deltaTime,0,0);
        //Debug.Log("This is the transform when trying to move: " + transform.position);
    }
}
