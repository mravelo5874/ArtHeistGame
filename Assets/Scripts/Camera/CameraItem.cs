using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraItem : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject cameraOverlay;
    [SerializeField] private CanvasGroup flash;
    [SerializeField] private MouseLook lookScript;
    [SerializeField] private PlayerMovement playerScript;
    public static bool cameraOn;
    public static int photoCount = 0;

    private const float flashFadeDuration = 2f;
    private bool takeScreenShotNextFrame;

    void Start()
    {
        cameraOn = false;
        cameraOverlay.SetActive(false);
        flash.alpha = 0f;
    }

    void Update()
    {
        if (!cameraOn && !lookScript.thirdPerson && Input.GetKeyDown(KeyCode.LeftShift)) // turn cam on
        {
            cameraOn = true;
            CameraHelper.SetCameraFOV(true);
            playerScript.ToggleMovement(true);
            cameraOverlay.SetActive(true);
        }
        else if (cameraOn && Input.GetKeyDown(KeyCode.LeftShift)) // turn cam off
        {
            // remove any flashes
            StopAllCoroutines();
            flash.alpha = 0f;

            cameraOn = false;
            CameraHelper.SetCameraFOV(false);
            playerScript.ToggleMovement(false);
            cameraOverlay.SetActive(false);
        }

        if (cameraOn && Input.GetMouseButtonDown(0)) // take picture
        {
            // get screenshot
            ScreenShotHandler.TakeScreenShot_static(450, 450);

            // flash
            StartCoroutine(Flash());
        }
    }

    

   private IEnumerator Flash()
   {
       flash.alpha = 1f;
       yield return new WaitForSeconds(0.2f);
       
       float timer = 0f;
       while (true)
       {
           timer += Time.deltaTime;
           if (timer >= flashFadeDuration)
           {
               break;
           }
           flash.alpha = Mathf.Lerp(1f, 0f, (timer / flashFadeDuration));
           yield return null;
       }
       flash.alpha = 0f;
   }
}
