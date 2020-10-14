using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class VisionScript : MonoBehaviour
{

    public GameObject player;
    float fovAngle;

    void Start()
    {
        fovAngle = 110f;
    }

    void Update()
    {
        // Finds if player is within field of view and then fires a ray to check if behind something.
        Vector3 direction = player.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        if(angle < fovAngle * 0.5f)
        {
            RaycastHit hit;

            if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, 10f))
            {

                if (hit.collider.gameObject == player)
                {
                    Debug.Log("RUN.");
                    Movement.StartChasing();
                }

            }

        }
    }

}
