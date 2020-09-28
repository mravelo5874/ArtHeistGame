using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovement : MonoBehaviour
{
    public float speed = 4f;

    public Transform[] moveSpots;
    private int randomSpot;

    void Start()
    {
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
