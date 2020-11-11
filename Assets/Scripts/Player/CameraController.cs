using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraHelper
{
    private static CameraController cc;

    public static void SetFOV(float num)
    {
        FindCameraController();
        cc.SetFOV(num);
    }

    public static void SetCameraFOV(bool opt)
    {
        FindCameraController();
        cc.SetCameraFOV(opt);
    }

    public static void ResetFOV()
    {
        FindCameraController();
        cc.ResetFOV();
    }

    private static void FindCameraController()
    {
        if (cc == null) cc = GameObject.Find("FPS_Camera").GetComponent<CameraController>();
        if (cc == null) Debug.LogError("CameraHelper could not find 'FPS_Camera'");
    }
}

public class CameraController : MonoBehaviour
{
    private Camera fps_cam;
    private bool camOn;
    public const float defaultFOV = 60f;
    public const float cameraFOV = 45f;
    [SerializeField] private float resetFOVTime;

    void Awake() 
    {
        fps_cam = GetComponent<Camera>();
        fps_cam.fieldOfView = defaultFOV;
    }

    public void SetFOV(float num)
    {
        if (!camOn)
        {
            fps_cam.fieldOfView = num;
            //print ("setting FOV to: " + num);
        }
    }

    public void SetCameraFOV(bool opt)
    {
        if (opt)
        {
            StopAllCoroutines();
            SetFOV(cameraFOV);
            this.camOn = opt;
        }
        else
        {
            StopAllCoroutines();
            this.camOn = opt;
            SetFOV(defaultFOV);
        }
    }

    public void ResetFOV()
    {
        if (!camOn)
            StartCoroutine(SmoothResetFOV());
    }

    private IEnumerator SmoothResetFOV()
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            SetFOV(Mathf.Lerp(fps_cam.fieldOfView, defaultFOV, timer / resetFOVTime));

            if (timer > resetFOVTime)
            {
                fps_cam.fieldOfView = defaultFOV;
                break;
            }

            yield return null;
        }   
    }
}
