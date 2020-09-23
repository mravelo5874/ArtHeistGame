using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCell : MonoBehaviour
{
    public bool isLookedAt;
    private Vector2Int pos;

    private MeshRenderer mr;

    void Start() 
    {
        mr = GetComponent<MeshRenderer>();
        SetColor(CellColorHelper.GetColor("WHITE")); // initalize color as WHITE
    }

    public void SetPosition(Vector2Int pos)
    {
        this.pos = pos;
    }   

    public void SetColor(Color color)
    {
        mr.material.color = color;
    }

    public void SetLookedAt(bool opt)
    {
        if (opt == isLookedAt)
            return;

        Debug.Log("Looking at cell? -> " + opt);

        isLookedAt = opt;
        //StartCoroutine(SmoothSetOutline(opt, outlineSpeed));
    }
}
