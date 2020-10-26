using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumSection : MonoBehaviour
{
    [HideInInspector] public List<CanvasObject> canavses;

    void Awake()
    {
        canavses = new List<CanvasObject>();

        foreach (Transform child in transform)
        {
            CanvasObject canvas = child.GetComponent<CanvasObject>();
            canavses.Add(canvas);
        }
    }
}
