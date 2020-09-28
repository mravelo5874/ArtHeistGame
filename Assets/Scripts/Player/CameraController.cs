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
    public const float defaultFOV = 60f;
    [SerializeField] private float resetFOVTime;

    void Awake() 
    {
        fps_cam = GetComponent<Camera>();
        fps_cam.fieldOfView = defaultFOV;
    }

    public void SetFOV(float num)
    {
        fps_cam.fieldOfView = num;
    }

    public void ResetFOV()
    {
        StartCoroutine(SmoothResetFOV());
    }

    private IEnumerator SmoothResetFOV()
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            fps_cam.fieldOfView = Mathf.Lerp(fps_cam.fieldOfView, defaultFOV, timer / resetFOVTime);

            if (timer > resetFOVTime)
            {
                fps_cam.fieldOfView = defaultFOV;
                break;
            }

            yield return null;
        }   
    }
}
