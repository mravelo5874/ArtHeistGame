using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class CameraItem : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject cameraOverlay;
    [SerializeField] private CanvasGroup flash;
    [SerializeField] private MouseLook lookScript;
    [SerializeField] private PlayerMovement playerScript;
    [SerializeField] private TextMeshProUGUI photoCountText;

    public static bool cameraOn;
    public static int photoCount = 0;
    public static int photoMax = 3;

    private const float flashFadeDuration = 2f;
    private bool takeScreenShotNextFrame;
    public const string photoFolder = "/Resources/CameraPhotos/";
    public const int photoResolution = 400;

    void Start()
    {
        cameraOn = false;
        cameraOverlay.SetActive(false);
        flash.alpha = 0f;
        photoCountText.text = "photos left: " + photoMax + "/" + photoMax;
        photoCount = 0;
        
        // clear photo folder
        string path = Application.dataPath + photoFolder;
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        else
        {
            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                File.Delete(file);
            }

            Directory.Delete(path);
            Directory.CreateDirectory(path);
        }
    }

    void Update()
    {
        // if they didn't buy the camera, they can't use it (wouldn't be able to get it on levels 0,1,2)
        // if (!InventoryScript.hasDigitalCamera || LevelTrackerStaticClass.levelNum < 3)
        // {
        //     return;
        // }

        if (!cameraOn && !lookScript.thirdPerson && Input.GetKeyDown(KeyCode.F)) // turn cam on
        {
            cameraOn = true;
            CameraHelper.SetCameraFOV(true);
            playerScript.ToggleMovement(true);
            cameraOverlay.SetActive(true);
        }
        else if (cameraOn && Input.GetKeyDown(KeyCode.F)) // turn cam off
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
            // can only take certain amount of photos
            if (photoCount >= photoMax)
                return;

            // get screenshot
            ScreenShotHandler.TakeScreenShot_static(photoResolution, photoResolution);

            // flash
            StartCoroutine(Flash());

            // update UI text
            photoCountText.text = "photos left: " + (photoMax - photoCount - 1) + "/" + photoMax;
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
