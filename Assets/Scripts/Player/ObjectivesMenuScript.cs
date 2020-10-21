using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesMenuScript : MonoBehaviour
{

    public GameObject objectiveMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            objectiveMenu.transform.position = new Vector3(790, 600, 0);
        } else
        {
            objectiveMenu.transform.position = new Vector3(1100, 600, 0);
        }
        
    }
}
