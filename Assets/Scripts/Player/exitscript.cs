using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO: change to next recreation scene...bring over any details about paintings you gathered...
            GameHelper.LoadScene("MuseumSceneStartCopy", true);
        }
    }
}
