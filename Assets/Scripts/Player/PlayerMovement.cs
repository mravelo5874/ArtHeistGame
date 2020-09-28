using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public static class PlayerMovementHelper
{
    public static void ToggleMovement(bool opt)
    {
        var info = GameObject.Find("Player").GetComponent<PlayerMovement>();
        info.ToggleMovement(opt);
    }
}

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 8f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool restrictMovement;

    void Awake()
    {
        restrictMovement = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (restrictMovement) return; // no move if restrictMovement == true

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

        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = speed - 0.05f;
            if (speed < 2.5f)
            {
                speed = 2.5f;
            }
        } else if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = speed + .05f;
            if (speed > 12f)
            {
                speed = 12f;
            }
        } else
        {
            if (speed > 8f)
            {
                speed = speed - 0.05f;
            } else if (speed < 8f)
            {
                speed = speed + 0.05f;
            }
        }

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // pressed x and near a painting in first person mode
        if (Input.GetKeyDown(KeyCode.X) && !MouseLook.instance.thirdPerson)
        {
            // do something when your click it.
            MouseLook.instance.thirdPerson = true;
            
        } else if (Input.GetKeyDown(KeyCode.X) && MouseLook.instance.thirdPerson)
        {
            MouseLook.instance.thirdPerson = false;
        }

        
    }

    public void ToggleMovement(bool opt)
    {
        restrictMovement = opt;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Painting"))
        {
            //MouseLook.instance.painting = other.gameObject;
            //MouseLook.instance.thirdPerson = false;
            //transform.LookAt(other.transform);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Painting"))
        {
            //MouseLook.instance.thirdPerson = true;
        }
    }


}
