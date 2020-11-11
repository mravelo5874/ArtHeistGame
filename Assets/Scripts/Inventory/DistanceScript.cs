using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceScript : MonoBehaviour
{
    public GameObject playerObject;
    public TextMeshProUGUI distanceText;

    // Start is called before the first frame update
    void Start()
    {
        // do something if they have the inventoryitem or not?
    }

    // Update is called once per frame
    void Update()
    {
        // update the distance calculation if they do have it...
        if (InventoryScript.hasHotCold && MuseumSceneManager.instance.objectiveCanvases.Count != 0)
        {
            // figure out the distance to the nearest canvas

            Vector3 playerPosition = playerObject.transform.position;


            // CODE FOR NEAREST ONE, LOOKING AT ALL OF THEM
            //float smallestDistance = 999999999;
            //foreach (CanvasObject Canvas in MuseumSceneManager.instance.objectiveCanvases)
            //{
            //    Vector3 canvasPosition = Canvas.transform.position;
            //
            //    float distance = Vector3.Distance(playerPosition, canvasPosition);
            //
            //    if (distance < smallestDistance)
            //    {
            //        smallestDistance = distance;
            //    }
            //}

            // CODE FOR FIRST ONE IN THE LIST (PROBABLY BETTER TO STICK WITH ONE AT A TIME)
            Vector3 canvasPosition = MuseumSceneManager.instance.objectiveCanvases[0].transform.position;
            float distance = Vector3.Distance(playerPosition, canvasPosition);
            
            distanceText.text = "Distance = " + Mathf.Max(distance - 1.9f, 0).ToString("F2"); // use smallestDistance if looping through all objectives at once to find nearest...
        } 
        else
        {
            distanceText.text = "";
        }
    }
}
