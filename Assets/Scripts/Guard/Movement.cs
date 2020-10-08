using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private float waitTime;
    public float startWaitTime;
    public bool isChasing;

    public GameObject currentMoveSpot;
    public GameObject playerToChase;
    public NavMeshAgent agent;

    void Start()
    {
        waitTime = startWaitTime;
        isChasing = false;
    }

    void Update()
    {

        // Normal patrol movement
        if (!isChasing)
        {
            //transform.position = Vector3.MoveTowards(transform.position, currentMoveSpot.transform.position, speed * Time.deltaTime);
            //agent.transform.position = Vector3.MoveTowards(agent.transform.position, currentMoveSpot.transform.position, speed * Time.deltaTime);
            agent.SetDestination(currentMoveSpot.transform.position);

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
        } else
        {
            agent.SetDestination(playerToChase.transform.position);
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            if(isChasing)
            {
                isChasing = false;
                agent.speed = 5f;
            } else
            {
                isChasing = true;
                agent.speed = 14f;
            }
        }

    }

}
