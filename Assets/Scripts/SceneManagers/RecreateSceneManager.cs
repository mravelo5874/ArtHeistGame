using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecreateSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject canvasCellObj;
    [SerializeField] private Transform canvasParent;
    private CanvasCell[,] canvas;

    void Start()
    {
        CreateNewCanvasFromPaintingSize(new Vector2Int(1, 1)); // TESTING -> create a 16x16 grid of canvas cells
    }


    public void CreateNewCanvasFromPaintingSize(Vector2Int paintingSize)
    {
        CreateNewCanvas(paintingSize * 16); // each 1x1 painting size is 16x16 pixels
    }

    private void CreateNewCanvas(Vector2Int size)
    {
        canvas = new CanvasCell[size.x, size.y]; // create a canvas of cells

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                canvas[x, y] = Instantiate(canvasCellObj, new Vector3(x, y, 0f), Quaternion.identity, canvasParent).GetComponent<CanvasCell>();
                canvas[x, y].SetPosition(new Vector2Int(x, y)); // set the position of each cell
            }
        }
    }
}
