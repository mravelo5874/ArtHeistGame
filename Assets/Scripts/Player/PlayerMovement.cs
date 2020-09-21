using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (MouseLook.instance.thirdPerson)
        {
            // TODO: change movement when in the third person mode... (don't make vector movements based on last first person angled position?)
            // reset the transform.right into a hard 90 degree angle to line up with the walls and hallways, make it easy for the player to know which way to go
            move = Vector3.right * x + Vector3.forward * z;
        }

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // pressed x and near a painting in first person mode
        if (Input.GetKeyDown(KeyCode.X) && !MouseLook.instance.thirdPerson)
        {
            // do something when your click it...
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Painting"))
        {
            MouseLook.instance.painting = other.gameObject;
            MouseLook.instance.thirdPerson = false;
            //transform.LookAt(other.transform);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Painting"))
        {
            MouseLook.instance.thirdPerson = true;
        }
    }


}
