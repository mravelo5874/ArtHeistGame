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

    public static void SetBrushSize(int size)
    {
        FindPaintbrush();
        pb.ChangeBrushSize(size);
    }

    public static int GetBrushSize()
    {
        FindPaintbrush();
        return pb.GetBrushSize();
    }

    public static void ToggleSetFill()
    {
        FindPaintbrush();
        pb.ToggleSetFill();
    }

    public static bool GetFillTool()
    {
        FindPaintbrush();
        return pb.GetFillTool();
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
    private bool fillTool = false;

    private CanvasCell currCell;
    [SerializeField] private RecreateSceneManager rcm;
    private const int maxRecursiveCalls = 256;
    private CanvasCell[,] canvas;

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
                    canvas = rcm.GetCanvasCells();

                    if (fillTool)
                    {
                        // do fill tool stuff
                        StartCoroutine(RecurssiveFillDelay(currCell.pos, currCell.color, currBrushColor, maxRecursiveCalls));

                        // reset isFilled bools
                        foreach(CanvasCell cell in canvas)
                            cell.isFilled = false;
                    }
                    else
                    {
                        // use brush size to color cell(s)
                        currCell.SetColor(currBrushColor); // color cell
                    }
                }   
            }
        }
    }

    private IEnumerator RecurssiveFillDelay(Vector2Int pos, Color prevColor, Color newColor, int itteration)
    {
        if (itteration < 0)
            yield break;
        itteration--;

        if (pos.x >= canvas.GetLength(0) || pos.y >= canvas.GetLength(1) || pos.x < 0 || pos.y < 0)
            yield break;
        
        CanvasCell cell = canvas[pos.x, pos.y];

        yield return new WaitForSeconds(0f);

        if (cell.isFilled)
            yield break;
        cell.isFilled = true;

        if (cell.color != prevColor)
            yield break;
        
        cell.SetColor(newColor);
        
        StartCoroutine(RecurssiveFillDelay(new Vector2Int(pos.x + 1, pos.y), prevColor, newColor, itteration));
        StartCoroutine(RecurssiveFillDelay(new Vector2Int(pos.x - 1, pos.y), prevColor, newColor, itteration));
        StartCoroutine(RecurssiveFillDelay(new Vector2Int(pos.x, pos.y + 1), prevColor, newColor, itteration));
        StartCoroutine(RecurssiveFillDelay(new Vector2Int(pos.x, pos.y - 1), prevColor, newColor, itteration));
    }


    public void SetCurrentCell(CanvasCell cell)
    {
        currCell = cell;
    }

    public void ChangeBrushColor(string colorName)
    {
        currBrushColor = CellColorHelper.GetColor(colorName);
    }

    public int GetBrushSize()
    {
        return currbrushSize;
    }

    public void ChangeBrushSize(int size)
    {
        if (size > brushSizeRange.y)
            size = 1;
        currbrushSize = size;
    }

    public void ToggleSetFill()
    {
        fillTool = !fillTool;
    }

    public bool GetFillTool()
    {
        return fillTool;
    }
}
