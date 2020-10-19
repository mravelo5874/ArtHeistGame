using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private float waitTime;
    public float startWaitTime;
    public static bool isChasing;

    public GameObject currentMoveSpot;
    public GameObject playerToChase;
    public static NavMeshAgent agent;

    void Start()
    {
        waitTime = startWaitTime;
        isChasing = false;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

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
                    waitTime = waitTime - Time.deltaTime;
                }
            }

            // If the player bumps into the guard, the guard will initiate chase.
            if (Vector3.Distance(transform.position, playerToChase.transform.position) < 1.1f)
            {
                Debug.Log("Whoops.");
                StartChasing();
            }

        // Chase Movement
        } else
        {
            agent.SetDestination(playerToChase.transform.position);
            if (Vector3.Distance(transform.position, playerToChase.transform.position) < 1f)
            {
                Debug.Log("Caught.");
                GameHelper.LoadScene("LevelSelectorScene", true);
            }
        }

    }

    // Makes the guard begin chasing and changes its speed so that it runs.
    public static void StartChasing()
    {
        isChasing = true;
        agent.speed = 14f;
    }

    // Makes the guard stop chasing and alters the speed back to normal.
    public static void StopChasing()
    {
        isChasing = false;
        agent.speed = 3.5f;
    }

}
