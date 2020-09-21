using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public static MouseLook instance;
    private void Awake()
    {
        instance = this;
    }

    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    float yOffset = 14f;
    float xRotationOffset = 59f;

    public bool thirdPerson = true;
    bool tempLookat = false;
    public GameObject painting = null;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if (!thirdPerson)
        {
            // transform.position = playerBody.position;
            if (!tempLookat)
            {
                Debug.Log("templookat");
                transform.position = playerBody.position;
                transform.rotation.Set(0,0,0,0);
                transform.LookAt(painting.transform);
                transform.rotation.SetFromToRotation(transform.position, painting.transform.position);
                tempLookat = true;
            } else
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

                playerBody.Rotate(Vector3.up * mouseX);
            }

            
        } else
        {

            transform.position = playerBody.position + new Vector3(0, 20, 0);
            transform.LookAt(playerBody.position);
            tempLookat = false;
        }
        
    }
}
