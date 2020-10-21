using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitscript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameHelper.GetPaintingCount() >= 1)
        {
            GameHelper.LoadScene("RecreatePaintingScene", true);
        }
        else
        {
            print ("Find a painting before exiting the level!");
        }
    }
}
