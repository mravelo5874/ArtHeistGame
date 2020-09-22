using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPainting : MonoBehaviour
{
    public bool showRay;

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
                Painting painting = hit.transform.GetComponentInParent<CanvasObject>().painting;
                PaintingInfoDisplayHelper.SetInfoDisplayActive(true);
                PaintingInfoDisplayHelper.SetPaintingInfo(painting);
            }
        }
        else
        {
            PaintingInfoDisplayHelper.SetInfoDisplayActive(false);
        }
    }
}
