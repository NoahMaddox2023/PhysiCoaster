﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class RideTrack : MonoBehaviour
{
    [Header("Button GameObjects")]
    public GameObject resultsScreenButtonGameObject;
    public GameObject retryButtonGameObject;
    public GameObject pauseMenuTitleScreenButtonGameObject;
    public GameObject pauseMenuRestartButtonGameObject;

    [Header("Cart/Grid GameObjects")]
    public GameObject cart;
    public GameObject grid;

    [Header("Text Objects")]
    public Text levelClearText;
    public Text levelFailText;
    public Text pauseText;

    [Header("Button Objects")]
    public Button retryButton;
    public Button resultsScreenButton;
    public Button pauseMenuTitleScreenButton;
    public Button pauseMenuRestartButton;

    [Header("Booleans")]
    public bool madeDestination;
    private bool MoveCartButtonPressed;
    private bool offTrack = false;
    private bool paused = false; 
    bool move = false;
    bool brokenTrack;
    private bool levelNotCleared = true;

    [Header("Floats")]
    private float velocity;
    private float kinetic;
    private float potential;
    private float time;
    public float checkDistance;
    public float speed;
    public float lerpSpeed;
    public float positionLerpSpeed;
    public float offset;
    public float direction;
    public float rayTime;
    public float gravity;
    public float coefFriction;
    public float mass;
    public float realScienceValue;
    private float pointTimer;
    private float failTimer;
    private int counter;
    private int numOfLevels = 10;

    private string path1, path2;

    private List<Vector2> potentialGraphed, kineticGraphed;

    Rigidbody rigidbody;
    Collider collider;

    [Header("Images")]
    public Image kineticBar;
    public Image potentialBar;

    [HideInInspector]
    public DialogueHandler duder;


    public AudioSource audioSource;

    Transform lastTrackPosition;
    Vector3 lastTrackNormal;
    Vector3[] directions;
    Vector3[] downDirection;
    Vector3 tallestTrackPosition;
    RaycastHit[] hit;
    RaycastHit[] downHit;

    private bool hasDialogue = false;
    // Start is called before the first frame update
    void Start()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("NextLevel", currentScene + 1);
        audioSource = GetComponent<AudioSource>();
        try
        {
            duder = GameObject.FindGameObjectWithTag("DialogueHandler").GetComponent<DialogueHandler>();
            hasDialogue = true;
        }
        catch
        {
            hasDialogue = false;
        }
        Time.timeScale = 1;
        grid.GetComponent<GridPlacement>().enabled = true;
        time = 0;
        pointTimer = 0.1f;
        tallestTrackPosition = new Vector3(0, 0, 0);

       
        retryButton.enabled = false;
        pauseMenuRestartButton.enabled = false;       
        pauseMenuTitleScreenButton.enabled = false;
        resultsScreenButtonGameObject.SetActive(false);

        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        levelFailText.enabled = false;
        pauseText.enabled = false;
        levelClearText.enabled = false;
        resultsScreenButton.enabled = false;

        retryButtonGameObject.SetActive(false);
        pauseMenuRestartButtonGameObject.SetActive(false);
        pauseMenuTitleScreenButtonGameObject.SetActive(false);
        resultsScreenButtonGameObject.SetActive(false);
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

        path1 = Application.streamingAssetsPath + "/PotentialPoints.txt";
        path2 = Application.streamingAssetsPath + "/KineticPoints.txt";

        if (File.Exists(path1))
        {
            File.Delete(path1);
        }

        if (File.Exists(path2))
        {
            File.Delete(path2);
        }
        failTimer = 10.0f;
        counter = 1;
        potentialGraphed = new List<Vector2>();
        kineticGraphed = new List<Vector2>();
    }

    public void MoveCart()
    {
        MoveCartButtonPressed = true;
        audioSource.Play();
        AddToLists();
    }

    public void PauseMenu()
    {
        if (hasDialogue && levelNotCleared && duder.isDoneDone)
        {
            paused = !paused;
            audioSource.Play();
            if (paused == true)
            {
                Time.timeScale = 0;
                pauseText.enabled = true;
                pauseMenuRestartButton.enabled = true;
                pauseMenuRestartButtonGameObject.SetActive(true);
                pauseMenuTitleScreenButton.enabled = true;
                pauseMenuTitleScreenButtonGameObject.SetActive(true);
                grid.GetComponent<GridPlacement>().enabled = false;
            }
            else
            {
                Time.timeScale = 1;
                pauseText.enabled = false;
                pauseMenuRestartButton.enabled = false;
                pauseMenuRestartButtonGameObject.SetActive(false);
                pauseMenuTitleScreenButton.enabled = false;
                pauseMenuTitleScreenButtonGameObject.SetActive(false);
                grid.GetComponent<GridPlacement>().enabled = true;
            }
        }
        else
        {
            if (levelNotCleared)
            {
                paused = !paused;
                audioSource.Play();
                if (paused == true)
                {
                    Time.timeScale = 0;
                    pauseText.enabled = true;
                    pauseMenuRestartButton.enabled = true;
                    pauseMenuRestartButtonGameObject.SetActive(true);
                    pauseMenuTitleScreenButton.enabled = true;
                    pauseMenuTitleScreenButtonGameObject.SetActive(true);
                    grid.GetComponent<GridPlacement>().enabled = false;
                }
                else
                {
                    Time.timeScale = 1;
                    pauseText.enabled = false;
                    pauseMenuRestartButton.enabled = false;
                    pauseMenuRestartButtonGameObject.SetActive(false);
                    pauseMenuTitleScreenButton.enabled = false;
                    pauseMenuTitleScreenButtonGameObject.SetActive(false);
                    grid.GetComponent<GridPlacement>().enabled = true;
                }
            }
        }
    }
    void FixedUpdate()
    {
        velocity -= velocity * Time.deltaTime * coefFriction;
        transform.position += (transform.right * velocity * Time.deltaTime);
        if (MoveCartButtonPressed == true)
        {
            TimeForLevel();
            AddToLists();
            time += Time.fixedDeltaTime;
            
            move = !move;
            
            potential = mass * gravity * transform.position.y;
            kinetic = 0.5f * mass * velocity * velocity;

            //Change 7.5 if highest point on grid is changed
            potentialBar.fillAmount = potential / (mass * gravity * 7.5f);
            kineticBar.fillAmount = kinetic / (mass * gravity * 7.5f);
        }

        if (!brokenTrack && move)
        {
            //moveCart();
            checkTracks();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FinishLine")
        {
            levelNotCleared = false;
            Time.timeScale = 0;
            levelClearText.enabled = true;
            resultsScreenButton.enabled = true;
            resultsScreenButtonGameObject.SetActive(true);
            grid.GetComponent<GridPlacement>().enabled = false;
            try
            {
                bool[] completedLevels;
                using (StreamReader sr = new StreamReader(Application.streamingAssetsPath + "/SavedLevels.txt"))
                {
                    string s = "";
                    string temp = sr.ReadToEnd();
                    Debug.Log(temp);
                    Debug.Log("Temp.length: " + temp.Length);
                    string[] allLevels = temp.Split('\n');
                    completedLevels = new bool[allLevels.Length];
                    for (int i = 0; i < completedLevels.Length; i++)
                    {
                        Debug.Log(allLevels[i] + " vs " + true + " vs " + false);
                        completedLevels[i] = Convert.ToBoolean(allLevels[i]);
                        s += completedLevels[i] + ",";
                    }
                    Debug.Log(s);
                    sr.Close();
                }
                using (StreamWriter sw = new StreamWriter(Application.streamingAssetsPath + "/SavedLevels.txt"))
                {
                    string s = "";
                    for (int i = 5; i < 5 + numOfLevels; i++)
                    {
                        if (SceneManager.GetActiveScene().buildIndex == i)
                        {
                            completedLevels[i - 5] = true;
                        }
                        //s += completedLevels[i] + ",";
                        if (i - 5 != numOfLevels - 1)
                            sw.Write(completedLevels[i - 5] + "\n");
                        else
                            sw.Write(completedLevels[i - 5]);
                    }
                    sw.Close();
                }
            }
            catch (FileNotFoundException fnfe)
            {
                using (StreamWriter sw = new StreamWriter(Application.streamingAssetsPath + "/SavedLevels.txt"))
                {
                    for (int i = 0; i < numOfLevels; i++)
                    {
                        if (i == 0)
                            sw.Write(true + "\n");
                        else if (i != numOfLevels - 1)
                            sw.Write(false + "\n");
                        else
                            sw.Write(false);
                    }
                    sw.Close();
                }
            }
            SetData();
        }
        else if (other.gameObject.tag == "FailTrigger")
        {
            Time.timeScale = 0;
            levelFailText.enabled = true;
            retryButton.enabled = true;
            retryButtonGameObject.SetActive(true);
            pauseMenuTitleScreenButton.enabled = true;
            pauseMenuTitleScreenButtonGameObject.SetActive(true);
            grid.GetComponent<GridPlacement>().enabled = false;
        }
    }

    //this shoots out raycasts from our current position and checks to see if any of the raycasts collide with a  track piece. If it
    // does we'll store the location of that collider and go towards that tracks position using lerp.
    void checkTracks()
    {
        //velocity += gravity * Mathf.Sin(0) / Time.deltaTime;
        hit = new RaycastHit[directions.Length];
        downHit = new RaycastHit[downDirection.Length]; 
        for (int i = 0; i < downDirection.Length; i++)
        {
            Vector3 dir = transform.TransformDirection(downDirection[i]);
            Physics.Raycast(transform.position, dir, out downHit[i], checkDistance);
            if (downHit[i].collider != null && !offTrack)
            {
                GoToTrack(downHit[i]);

                transform.up = downHit[i].normal;
                velocity += mass * gravity * -1 * Mathf.Sin(transform.rotation.z) * Time.deltaTime;
                break;
            }
            else
            {
                offTrack = true;
                rigidbody.useGravity = true;
                GetComponent<UnityEngine.EventSystems.PhysicsRaycaster>().enabled = false;
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
            if (hit.Length > 0)
            {
                if (lastTrackPosition != hit[0].transform)
                {
                    GoToTrack(hit[0]);
                }
                if (madeDestination)
                {
                    lastTrackPosition = hit[0].transform;
                    madeDestination = false;
                }
                //RotateTrack(hit[0]);


                //lastTrackNormal = hit[0].normal;
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
        Vector3 target = new Vector3(transform.position.x, hitPosition.point.y + offset, hitPosition.point.z);
        transform.position = Vector3.Lerp(transform.position, target, positionLerpSpeed /*Time.deltaTime*/);
        if(transform.position == target)
        {
            //Debug.Log("I have made my destination");
            madeDestination = true;
        }
        //Debug.Log("This is the position when going to a track: " + transform.position);
    }
    /*
    void RotateTrack(RaycastHit hitNormal)
    {
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hitNormal.normal);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, lerpSpeed * Time.deltaTime);
    }
    void moveCart()
    {
        /*
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
        */
    //transform.Translate(speed * Time.deltaTime,0,0);
    //Debug.Log("This is the transform when trying to move: " + transform.position);
    // }

    private void TimeForLevel()
    {
        if (levelNotCleared)
        {
            failTimer -= Time.fixedDeltaTime;

            if (failTimer <= 0.0f)
            {
                Time.timeScale = 0;
                levelFailText.enabled = true;
                retryButton.enabled = true;
                retryButtonGameObject.SetActive(true);
                grid.GetComponent<GridPlacement>().enabled = false;
            }
        }
    }

    private void AddToLists()
    {
        if (levelNotCleared)
        {
            pointTimer -= Time.fixedDeltaTime;

            if (pointTimer <= 0.0f)
            {
                potentialGraphed.Add(new Vector2(0.1f * counter, potential));
                kineticGraphed.Add(new Vector2(0.1f * counter, kinetic));
                counter++;
                pointTimer = 0.1f;
            }
        } else if (!levelNotCleared)
        {
            potentialGraphed.Add(new Vector2(counter + pointTimer, potential));
            kineticGraphed.Add(new Vector2(counter + pointTimer, kinetic));
        }
    }

    private void SetData()
    {
        using (StreamWriter sw = File.CreateText(path1))
        {
            for (int i = 0; i < potentialGraphed.Count; i++)
            {
                sw.WriteLine(potentialGraphed[i].x.ToString() + "," + potentialGraphed[i].y.ToString());
            }
        }

        using (StreamWriter sw = File.CreateText(path2))
        {
            for (int i = 0; i < kineticGraphed.Count; i++)
            {
                sw.WriteLine(kineticGraphed[i].x.ToString() + "," + kineticGraphed[i].y.ToString());
            }
        }
    }
}
