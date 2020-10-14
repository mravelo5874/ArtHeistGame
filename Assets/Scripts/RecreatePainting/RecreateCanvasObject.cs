﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecreateCanvasObject : MonoBehaviour
{
    public CanvasCell[,] canvas;
    private Painting painting;
    private List<Color> colors;
    [SerializeField] private GameObject canvasCellObj;
    [SerializeField] private Transform canvasParent;

    public void Constructor(Painting _painting)
    {
        this.painting = _painting;
        CreateNewCanvasFromPaintingSize(painting.size);
    }

    public void CreateNewCanvasFromPaintingSize(Vector2Int paintingSize)
    {
        CreateNewCanvas(paintingSize * GameManager.canvasToPixelRatio); // each 1x1 painting size is 16x16 pixels
    }

    private void CreateNewCanvas(Vector2Int size)
    {
        DestroyCanvas();
        canvas = new CanvasCell[size.x, size.y]; // create a canvas of cells

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                float xPos = this.transform.position.x - x;
                canvas[x, y] = Instantiate(canvasCellObj, new Vector3(xPos, y, 0f), Quaternion.identity, canvasParent).GetComponent<CanvasCell>();
                canvas[x, y].SetPosition(new Vector2Int(x, y)); // set the position of each cell
            }
        }

        //SetCameraPosition();
        //SetGrid(showGrid);
    }

    private void DestroyCanvas()
    {
        if (canvas != null)
        {
            foreach(CanvasCell cell in canvas)
            {
                Destroy(cell.gameObject);
            }
        }
    }

    public Vector2Int GetPaintingSize()
    {
        return new Vector2Int(painting.size.x, painting.size.y);
    }

    public void SetGrid(bool opt)
    {
        if (opt)
        {
            foreach(CanvasCell cell in canvas)
                cell.SetOutlineWidth(1.1f);
        }
        else
        {
            foreach(CanvasCell cell in canvas)
                cell.SetOutlineWidth(1f);
        }
    }

    /*
    #####################################
    #   DevMakePaintingSceneManager
    #####################################
    */

    public void LoadData(PaintingData data)
    {
        CreateNewCanvasFromPaintingSize(data.canvasSize);

        foreach (CellData cell in data.cellData)
        {
            Color color = new Color();
            ColorUtility.TryParseHtmlString(cell.colorHex, out color);

            //print ("cell pos: " + cell.pos.x + ", " + cell.pos.y + " --- color: " + '#' + ColorUtility.ToHtmlStringRGBA(color));
            canvas[cell.pos.x, cell.pos.y].SetColor(color);
        }
    }
}
