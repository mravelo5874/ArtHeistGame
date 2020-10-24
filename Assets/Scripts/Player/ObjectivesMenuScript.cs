using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ObjectivesMenuScript : MonoBehaviour
{

    public GameObject objectiveMenu;
    Vector3 pos;

    void Start()
    {
        objectiveMenu.transform.position = new Vector3(1300, 500, 0);
        pos = objectiveMenu.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            if(pos.x > 790)
            {
                pos.x -= 20;
                if (pos.x <= 790)
                {
                    pos.x = 790;
                }
            }
            objectiveMenu.transform.position = pos;
        } else
        {
            if (pos.x < 1300)
            {
                pos.x += 20;
                if (pos.x >= 1300)
                {
                    pos.x = 1300;
                }
            }
            objectiveMenu.transform.position = pos;
        }
        
    }
}
