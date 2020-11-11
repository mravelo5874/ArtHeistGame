using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotHandler : MonoBehaviour
{
    private static ScreenShotHandler instance;
    private Camera cam;
    private bool takeScreenShotNextFrame;

    private void Awake() 
    {
        instance = this;
        cam = GetComponent<Camera>();
    }

    private void OnPostRender()
    {
        if (takeScreenShotNextFrame)
        {
            takeScreenShotNextFrame = false;
            RenderTexture renderTexture = cam.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/CameraPhotos/CameraPhoto_" + CameraItem.photoCount + ".png", byteArray);
            Debug.Log("Saved Photo to " + Application.dataPath + "/CameraPhotos/CameraPhoto_" + CameraItem.photoCount + ".png");

            RenderTexture.ReleaseTemporary(renderTexture);
            cam.targetTexture = null;

            CameraItem.photoCount++;
        }
    }
   
    private void TakeScreenShot(int width, int height)
    {
        cam.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenShotNextFrame = true;
    }

    public static void TakeScreenShot_static(int width, int height)
    {
        instance.TakeScreenShot(width, height);
    }
}
