using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class VisionScript : MonoBehaviour
{

    public GameObject player;
    float fovAngle;
    public static bool playerInSight;

    void Start()
    {
        fovAngle = 270f;
    }

    void Update()
    {
        // Finds if player is within field of view and then fires a ray to check if behind something.
        Vector3 direction = player.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        if(angle < fovAngle * 0.5f)
        {
            RaycastHit hit;

            if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, 20f))
            {

                if (hit.collider.gameObject == player)
                {
                    Debug.Log("RUN.");
                    if(!playerInSight)
                    {
                        Movement.StartChasing();
                        playerInSight = true;
                    }
                } else
                {
                    playerInSight = false;
                }

            }

        }
    }

}
