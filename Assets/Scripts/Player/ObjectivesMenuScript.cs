using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ObjectivesMenuScript : MonoBehaviour
{

    public GameObject objectiveMenu;
    Vector3 pos;

    public int topValue = 300;
    public int leftSideValue = 641;
    public int rightSideValue = 1300;
    public int incrementValue = 20;
    public bool testActive = false;

    void Start()
    {
        objectiveMenu.transform.position = new Vector3(rightSideValue, topValue, 0);
        pos = objectiveMenu.transform.position;
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab) || testActive)
        {
            if(pos.x > leftSideValue)
            {
                pos.x -= incrementValue;
                if (pos.x <= leftSideValue)
                {
                    pos.x = leftSideValue;
                }
            }
            objectiveMenu.transform.position = pos;
        } else
        {
            if (pos.x < rightSideValue)
            {
                pos.x += incrementValue;
                if (pos.x >= rightSideValue)
                {
                    pos.x = rightSideValue;
                }
            }
            objectiveMenu.transform.position = pos;
        }
        
    }
}
