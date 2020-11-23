using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasCell : MonoBehaviour
{
    public bool isLookedAt;
    public bool isFilled;
    public Color gridColor;
    private float prevWidth;
    
    public Vector2Int pos;
    public Color color;
    public GameObject highlightCellObject;

    private MeshRenderer mr;
    [SerializeField] private Image COLOR_IMG;

    void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        mr.material.SetColor("_OutlineColor", gridColor);
        SetColor(CellColorHelper.GetColor("WHITE")); // initalize color as WHITE
        SetHighlightCell(false); // start cell as not highlighted
    }

    public void SetPosition(Vector2Int pos)
    {
        this.pos = pos;
    }   

    public void SetColor(Color color)
    {
        this.color = color;
        COLOR_IMG.color = color;
        //print ("color set to: " + '#' + ColorUtility.ToHtmlStringRGBA(color));
    }

    public void SetLookedAt(bool opt)
    {
        if (opt == isLookedAt)
            return;

        //Debug.Log("Looking at cell? -> " + opt);

        isLookedAt = opt;
    }

    public void SetHighlightCell(bool opt)
    {
        highlightCellObject.SetActive(opt);
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
