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
    public Animator thiefAnimator;

    public float speed = 8f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool restrictMovement;

    public static PlayerMovement instance;

    public GameObject playerBodyModel;

    void Awake()
    {
        restrictMovement = false;
        instance = this;
        playerBodyModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            MuseumSceneStaticClass.gameIsPaused = !MuseumSceneStaticClass.gameIsPaused;
        }

        if (MuseumSceneStaticClass.gameIsPaused)
        {
            return;
        }



        if (restrictMovement) return; // no move if restrictMovement == true

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (x == 0 && z == 0)
            {
                thiefAnimator.SetTrigger("Crouch");
            }
            else
            {
                thiefAnimator.SetTrigger("CrouchMovement");
            }

            speed = speed - 0.05f;
            if (speed < 2.5f)
            {
                speed = 2.5f;
            }
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            if ((x < 0.2 && x > -0.2) && (z < 0.2 && z > -0.2))
            {
                thiefAnimator.SetTrigger("Stand");
            }
            else
            {
                thiefAnimator.SetTrigger("Run");
            }
            speed = speed + .05f;
            if (speed > 12f)
            {
                speed = 12f;
            }
        }
        else
        {
            if ((x < 0.2 && x > -0.2) && (z < 0.2 && z > -0.2))
            {
                thiefAnimator.SetTrigger("Stand");
            }
            else
            {
                thiefAnimator.SetTrigger("Walk");
            }
            if (speed > 8f)
            {
                speed = speed - 0.05f;
            }
            else if (speed < 8f)
            {
                speed = speed + 0.05f;
            }
        }

        // if they have items that boost their speed
        float boost = InventoryScript.hasSpeedOnePills ? 100 : 0;

        controller.Move(move * (speed + boost) * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // switch between 3rd person and 1st person
        if (Input.GetKeyDown(KeyCode.X) && !MouseLook.instance.thirdPerson)
        {
            MouseLook.instance.thirdPerson = true;
            playerBodyModel.SetActive(true);
            MuseumHelper.SetRoofActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.X) && MouseLook.instance.thirdPerson)
        {
            playerBodyModel.SetActive(false);
            MouseLook.instance.thirdPerson = false;
            MuseumHelper.SetRoofActive(true);
        }
    }

    public void ToggleMovement(bool opt)
    {
        restrictMovement = opt;
    }
}