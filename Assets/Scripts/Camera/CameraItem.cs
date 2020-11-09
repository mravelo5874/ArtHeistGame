using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraItem : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform cameraObject;
    [SerializeField] private MouseLook lookScript;

    private bool cameraActive;
    public float distance;

    void Update()
    {
        if (!lookScript.thirdPerson && Input.GetKeyDown(KeyCode.LeftShift))
        {   
            MoveCameraToFace();
        }
        else
        {
            PutCameraBack();
        }
    }

    public void MoveCameraToFace()
    {
        if (!cameraActive)
        {
            cameraActive = true;
            StartCoroutine(MoveCameraToFaceCoroutine());
        }
    }

    public void PutCameraBack()
    {
        if (cameraActive)
        {
            cameraActive = false;
        }
    }

    private IEnumerator MoveCameraToFaceCoroutine()
    {
        cameraObject.gameObject.SetActive(true);
        cameraObject.position = playerCamera.transform.position + playerCamera.transform.forward * distance;
        yield return null;
    }

    private IEnumerator PutCameraBackCoroutine()
    {
        cameraObject.gameObject.SetActive(false);
        yield return null;
    }
}
