using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public static class GuardMovementHelper
{
    public static void ToggleMovement(bool opt)
    {
        var info = GameObject.Find("Guard").GetComponent<Movement>();
        info.ToggleMovement(opt);
    }
}

public class Movement : MonoBehaviour
{
    private float waitTime;
    public float startWaitTime;
    private float lostTime; // Tracks how long since the player has last been seen.
    public float startLostTime;
    public static bool isChasing;

    public Animator guardAnimator;

    public GameObject currentMoveSpot;
    public GameObject playerToChase;
    public static NavMeshAgent agent;

    bool restrictMovement;
    public static Movement instance;
    public GameObject guardBodyModel;

    void Awake()
    {
        restrictMovement = false;
        instance = this;
    }

    void Start()
    {
        waitTime = -1.0f;
        isChasing = false;
        agent = GetComponent<NavMeshAgent>();
        gameObject.transform.position = GameHelper.GetCurrentLevel().guardspot0;
    }

    bool stopUpdating = false;

    void Update()
    {

        Debug.Log(transform.position.x + " " + transform.position.y + " " + transform.position.z);

        if (MuseumHelper.GetPaused())
        {
            agent.enabled = false;
            return;
        }
        else
        {
            agent.enabled = true;
        }

        // Normal patrol movement
        if (!isChasing)
        {

            agent.SetDestination(currentMoveSpot.transform.position);

            // Moves between movespots and waits for a specified amount of time upon reaching one.
            if (Vector3.Distance(transform.position, currentMoveSpot.transform.position) < 0.2f)
            {
                if (waitTime < 0)
                {
                    if (UnityEngine.Random.Range(1, 10) > 1)
                    {
                        currentMoveSpot = currentMoveSpot.GetComponent<MoveSpotScript>().adjacentMoveSpots[1];
                    }
                    else
                    {
                        currentMoveSpot = currentMoveSpot.GetComponent<MoveSpotScript>().adjacentMoveSpots[0];
                    }

                    waitTime = startWaitTime;
                }
                else
                {
                    guardAnimator.SetTrigger("Stand");
                    waitTime = waitTime - Time.deltaTime;
                }
            } else
            {
                guardAnimator.SetTrigger("Patrol");
            }

            // If the player bumps into the guard, the guard will initiate chase.
            if (Vector3.Distance(transform.position, playerToChase.transform.position) < 1.1f)
            {
                Debug.Log("Whoops.");
                StartChasing();
            }

            // Chase Movement
        }
        else
        {
            guardAnimator.SetTrigger("Chase");

            agent.SetDestination(playerToChase.transform.position);
            if (Vector3.Distance(transform.position, playerToChase.transform.position) < 1f)
            {
                // Debug.Log("Caught.");
                PlayerMovement.speed = 0f;
                if (!stopUpdating)
                {
                    stopUpdating = true;
                    CaughtText.updateDisplay();
                    GameHelper.LoadScene("LevelSelectorScene", true, 3) ; // need to load a separate scene or some text that says you got caught
                }


            }

            // Checks if too far from intended position and if player has been lost for half of lostTime.
            if (Vector3.Distance(transform.position, currentMoveSpot.transform.position) > 100f && lostTime <= startLostTime / 2)
            {
                Debug.Log("Too far from movespot and lost player for a bit! Heading back.");
                stopUpdating = false;
                StopChasing();
            }

            // Checks if the player has been out of the guard's cone of vision.
            if (!VisionScript.playerInSight)
            {
                if (lostTime < 0f)
                {
                    Debug.Log("Lost for longer than LostTime.");
                    stopUpdating = false;
                    StopChasing();
                }
                else
                {
                    lostTime = lostTime - Time.deltaTime;
                }
            }
            else
            {
                lostTime = startLostTime;
            }

        }

        // test hack stuff
        if (LevelTrackerStaticClass.updateHackTracker < 10)
        {
            LevelTrackerStaticClass.updateHackTracker += 1;
            agent.transform.position = GameHelper.GetCurrentLevel().guardspot0;
            agent.speed = 1000.0f;
        } else if(LevelTrackerStaticClass.updateHackTracker == 10)
        {
            LevelTrackerStaticClass.updateHackTracker += 1;
            agent.speed = 3.5f;
        }

    }

    // Makes the guard begin chasing and changes its speed so that it runs.
    public static void StartChasing()
    {
        isChasing = true;
        agent.speed = 8.0f;
    }

    // Makes the guard stop chasing and alters the speed back to normal.
    public static void StopChasing()
    {
        isChasing = false;
        agent.speed = 3.5f;
    }

    public void ToggleMovement(bool opt)
    {
        restrictMovement = opt;
    }

    public void SetPosition(Vector3 pos)
    {
        this.transform.position = pos;
    }

}
