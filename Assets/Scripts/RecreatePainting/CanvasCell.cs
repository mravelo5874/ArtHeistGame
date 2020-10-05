using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCell : MonoBehaviour
{
    public bool isLookedAt;
    public Color gridColor;
    
    public Vector2Int pos;
    public Color color;

    private MeshRenderer mr;

    void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        mr.material.SetColor("_OutlineColor", gridColor);
        SetColor(CellColorHelper.GetColor("WHITE")); // initalize color as WHITE
    }

    public void SetPosition(Vector2Int pos)
    {
        this.pos = pos;
    }   

    public void SetColor(Color color)
    {
        this.color = color;
        mr.material.color = color;
        print ("color set to: " + '#' + ColorUtility.ToHtmlStringRGBA(color));
    }

    public void SetLookedAt(bool opt)
    {
        if (opt == isLookedAt)
            return;

        Debug.Log("Looking at cell? -> " + opt);

        isLookedAt = opt;
    }

    public void SetOutlineWidth(float width)
    {
        mr.material.SetFloat("_OutlineWidth", width);
    }

    public string GetHexColor()
    {
        return '#' + ColorUtility.ToHtmlStringRGBA(color);
    }
}
