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

    public static void SetBrushHighlight(bool opt)
    {
        FindPaintbrush();
        pb.SetBrushHighlight(opt);
    }

    public static void ChangeBrushColor(string color)
    {
        FindPaintbrush();
        pb.ChangeBrushColor(color);
    }

    public static void AddColorButton(CellColor cellColor)
    {
        FindPaintbrush();
        pb.AddColorButton(cellColor);
    }

    public static void RemoveAllColors()
    {
        FindPaintbrush();
        pb.RemoveAllColors();
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

    [SerializeField] private GameObject colorButtonObject;
    [SerializeField] private Transform colorButtonParent;

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
                        SetColorUsingBrushSize();
                    }
                }   
            }
        }
    }

    private void SetColorUsingBrushSize()
    {
        List<Vector2Int> brushDeltaPos = GetBrushDeltaPositions(currbrushSize);
        Vector2Int origin = currCell.pos;
        foreach (Vector2Int pos in brushDeltaPos)
        {
            Vector2Int newPos = origin + pos;

            if (newPos.x >= canvas.GetLength(0) || newPos.y >= canvas.GetLength(1) || newPos.x < 0 || newPos.y < 0)
                continue;

            CanvasCell cell = canvas[newPos.x, newPos.y];
            cell.SetColor(currBrushColor);
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

    public void SetBrushHighlight(bool opt)
    {
        if (rcm.currCanvas == null)
            return;
        
        rcm.currCanvas.UnhighlightAllCells();

        if (opt)
        {
            List<Vector2Int> brushDeltaPos;
            if (fillTool)
                brushDeltaPos = GetBrushDeltaPositions(1);
            else
                brushDeltaPos = GetBrushDeltaPositions(currbrushSize);
            
            canvas = rcm.GetCanvasCells();
            Vector2Int origin = currCell.pos;
            foreach (Vector2Int pos in brushDeltaPos)
            {
                Vector2Int newPos = origin + pos;

                if (newPos.x >= canvas.GetLength(0) || newPos.y >= canvas.GetLength(1) || newPos.x < 0 || newPos.y < 0)
                    continue;

                CanvasCell cell = canvas[newPos.x, newPos.y];
                cell.SetHighlightCell(true);
            }
        }
    }

    private List<Vector2Int> GetBrushDeltaPositions(int brushSize)
    {
        if (brushSize < brushSizeRange.x || brushSize > brushSizeRange.y)
            brushSize = 1; // set brush size to 1 if invalid brush size

        List<Vector2Int> list = new List<Vector2Int>();

        switch (brushSize)
        {
            // #
            default:
            case 1:
                list.Add(new Vector2Int(0, 0));
                break;
            // # #
            // # #
            case 2:
                list.Add(new Vector2Int(0, 0));

                list.Add(new Vector2Int(-1, 0));
                list.Add(new Vector2Int(0, 1));
                list.Add(new Vector2Int(-1, 1));
                break;
            // # # #
            // # # #
            // # # #
            case 3:
                list.Add(new Vector2Int(0, 0));

                list.Add(new Vector2Int(-1, 0));
                list.Add(new Vector2Int(0, 1));
                list.Add(new Vector2Int(-1, 1));

                list.Add(new Vector2Int(0, 2));
                list.Add(new Vector2Int(-1, 2));
                list.Add(new Vector2Int(-2, 2));
                list.Add(new Vector2Int(-2, 0));
                list.Add(new Vector2Int(-2, 1));
                break;
            // # # # #
            // # # # #
            // # # # #
            // # # # #
            case 4:
                list.Add(new Vector2Int(0, 0));

                list.Add(new Vector2Int(-1, 0));
                list.Add(new Vector2Int(0, 1));
                list.Add(new Vector2Int(-1, 1));

                list.Add(new Vector2Int(0, 2));
                list.Add(new Vector2Int(-1, 2));
                list.Add(new Vector2Int(-2, 2));
                list.Add(new Vector2Int(-2, 0));
                list.Add(new Vector2Int(-2, 1));

                list.Add(new Vector2Int(0, 3));
                list.Add(new Vector2Int(-1, 3));
                list.Add(new Vector2Int(-2, 3));
                list.Add(new Vector2Int(-3, 3));
                list.Add(new Vector2Int(-3, 0));
                list.Add(new Vector2Int(-3, 1));
                list.Add(new Vector2Int(-3, 2));
                break;
        }

        return list;
    }

    public void AddColorButton(CellColor cellColor)
    {
        PaintColorButton cb = Instantiate(colorButtonObject, colorButtonParent).GetComponent<PaintColorButton>();
        cb.Constructor(cellColor.colorName, cellColor.color);
    }

    public void RemoveAllColors()
    {
        foreach (Transform child in colorButtonParent.transform) 
        {
            GameObject.Destroy(child.gameObject);
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
