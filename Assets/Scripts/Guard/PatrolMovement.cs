using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovement : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    public GameObject currentMoveSpot;

    void Start()
    {
        waitTime = startWaitTime;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentMoveSpot.transform.position, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, currentMoveSpot.transform.position) < 0.02f)
        {
            if(waitTime < 0)
            {
                if(UnityEngine.Random.Range(1, 10) > 1)
                {
                    currentMoveSpot = currentMoveSpot.GetComponent<MoveSpotScript>().adjacentMoveSpots[1];
                } else
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
    }
}
