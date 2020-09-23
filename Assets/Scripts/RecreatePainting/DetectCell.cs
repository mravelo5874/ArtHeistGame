using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCell : MonoBehaviour
{
    public bool showRay;
    private CanvasCell mostRecentCell;

    [SerializeField] private Camera cam;

    void Update() 
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (showRay)
            Debug.DrawRay(ray.origin, ray.direction, Color.blue, 0.1f, false);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Cell")
            {
                // get cell object
                mostRecentCell = hit.transform.GetComponentInParent<CanvasCell>();

                // set cell 'looked at' bool to true
                mostRecentCell.SetLookedAt(true);

                // send out cell data
                PaintbrushHelper.SetCurrentCell(mostRecentCell);
            }
        }
        else // no longer looking at cell
        {
            // set painting 'looked at' bool to false
            if (mostRecentCell)
                mostRecentCell.SetLookedAt(false);
        }
    }
}
