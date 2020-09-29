using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPainting : MonoBehaviour
{
    public bool showRay;
    private CanvasObject mostRecentCanvas;

    [SerializeField] private Camera cam;

    void Update() 
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (showRay)
            Debug.DrawRay(ray.origin, ray.direction, Color.blue, 0.1f, false);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Painting")
            {
                // get canvas object
                mostRecentCanvas = hit.transform.GetComponentInParent<CanvasObject>();

                // set painting 'looked at' bool to true
                mostRecentCanvas.SetLookedAt(true);

                // send out painting data and activate UI
                Painting painting = mostRecentCanvas.painting;
                PaintingInfoDisplayHelper.SetInfoDisplayActive(true);
                PaintingInfoDisplayHelper.SetPaintingInfo(painting);
            }
            else // no longer looking at painting
            {
                // set painting 'looked at' bool to false
                if (mostRecentCanvas)
                    mostRecentCanvas.SetLookedAt(false);

                // deactivate ui
                PaintingInfoDisplayHelper.SetInfoDisplayActive(false);
            }
        }
        else // ray does not hit an object
        {
            // set painting 'looked at' bool to false
            if (mostRecentCanvas)
                mostRecentCanvas.SetLookedAt(false);

            // deactivate ui
            PaintingInfoDisplayHelper.SetInfoDisplayActive(false);
        }
    }
}
