using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DetectCellHelper
{
    private static DetectCell dc;

    public static void BlockRaycasts(bool opt)
    {
        FindDetectCell();
        dc.blockRaycast = opt;
    }

    private static void FindDetectCell()
    {
        if (dc == null) dc = GameObject.Find("RecreateSceneManager").GetComponent<DetectCell>();

        // could not find detect cell
        if (dc == null) Debug.LogError("DetectCellHelper could not find 'DetectCell'");
    }
} 

public class DetectCell : MonoBehaviour
{
    public bool blockRaycast;
    public bool showRay;
    private CanvasCell mostRecentCell;

    [SerializeField] private Camera cam;

    void Update() 
    {
        if (blockRaycast)
            return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (showRay)
            Debug.DrawRay(ray.origin, ray.direction, Color.blue, 0.1f, false);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Cell")
            {
                // set previous cell SetLookedAt to false
                if (mostRecentCell)
                {
                    if (hit.transform.GetComponentInParent<CanvasCell>() != mostRecentCell)
                    {
                        mostRecentCell.SetLookedAt(false);
                    }
                }

                // get cell object
                mostRecentCell = hit.transform.GetComponentInParent<CanvasCell>();

                // set cell 'looked at' bool to true
                mostRecentCell.SetLookedAt(true);

                // send out cell data to paintbrush 
                PaintbrushHelper.SetCurrentCell(mostRecentCell);

                // highlight cell
                PaintbrushHelper.SetBrushHighlight(true);
            }
            else // no longer looking at cell
            {
                // set cell 'looked at' bool to false
                if (mostRecentCell)
                    mostRecentCell.SetLookedAt(false);
                
                // unhighlight cell
                PaintbrushHelper.SetBrushHighlight(false);
            }
        }
        else // no longer looking at cell
        {
            // set cell 'looked at' bool to false
            if (mostRecentCell)
                mostRecentCell.SetLookedAt(false);

            // unhighlight cell
            PaintbrushHelper.SetBrushHighlight(false);
        }
    }
}
