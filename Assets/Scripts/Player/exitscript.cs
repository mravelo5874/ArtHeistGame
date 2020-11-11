using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitscript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //  && MuseumHelper.AllObjectivesComplete()
        {
            GameHelper.LoadScene("RecreatePaintingScene", true);
        }
        else
        {
            print ("***Complete all objectives before leaving the museum!***");
        }
    }
}
