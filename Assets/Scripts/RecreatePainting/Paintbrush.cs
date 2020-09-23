using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PaintbrushHelper
{   
    public static Paintbrush pb;

    public static void SetCurrentCell(CanvasCell cell)
    {
        FindPaintbrush();
        pb.SetCurrentCell(cell);
    }

    private static void FindPaintbrush()
    {
        if (pb == null) pb = GameObject.Find("Paintbrush").GetComponent<Paintbrush>();

        // could not find paintbrush
        if (pb == null) Debug.LogError("PaintbrushHelper could not find 'Paintbrush'");
    }
}

public class Paintbrush : MonoBehaviour
{
    private Color currBrushColor;
    private int currbrushSize;
    [SerializeField] private Vector2Int brushSizeRange;

    private CanvasCell currCell;

    void Start()
    {
        currBrushColor = CellColorHelper.GetColor("WHITE");
        currbrushSize = 1;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // if mouse is being held down
        {
            if (currCell) // make sure curr cell is not null
            {
                if (currCell.isLookedAt) // curr cell is being looked at (mouse is over it)
                {
                    currCell.SetColor(currBrushColor); // color cell
                }   
            }
        }
    }

    public void SetCurrentCell(CanvasCell cell)
    {
        currCell = cell;
    }

    public void ChangeBrushColor(string colorName)
    {
        currBrushColor = CellColorHelper.GetColor(colorName);
    }

    public void ChangeBrushSize(int size)
    {
        if (size >= brushSizeRange.x && size <= brushSizeRange.y) // check to see if size is within range
        {
            currbrushSize = size;
        }
    }
}
