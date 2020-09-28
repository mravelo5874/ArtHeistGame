using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class vec2_vec3
{
    public Vector2 size;
    public Vector3 position;
}

public class RecreateSceneManager : MonoBehaviour
{
    [SerializeField] private List<vec2_vec3> canvasSize2CamPosDictionary;

    [SerializeField] private Camera cam;
    public float timeChangeCameraPos;
    private IEnumerator moveCamCoroutine;

    [SerializeField] private GameObject canvasCellObj;
    [SerializeField] private Transform canvasParent;
    private CanvasCell[,] canvas;

    private int canvasWidth;
    private int canvasHeight;
    public int canvasRatio;
    [SerializeField] private TextMeshProUGUI widthLabel;
    [SerializeField] private TextMeshProUGUI heightLabel;
    [SerializeField] private int maxCanvasSize;

    private bool showGrid;
    [SerializeField] private TextMeshProUGUI showGridLabel;

    void Awake()
    {
        GameHelper.SceneInit();
    }

    void Start()
    {
        showGrid = false;
        canvasWidth = 1;
        canvasHeight = 1;
        widthLabel.text = canvasWidth.ToString();
        heightLabel.text = canvasHeight.ToString();
        CreateNewCanvasFromPaintingSize(new Vector2Int(canvasWidth, canvasHeight));
    }

    public void CreateNewCanvasFromPaintingSize(Vector2Int paintingSize)
    {
        CreateNewCanvas(paintingSize * canvasRatio); // each 1x1 painting size is 16x16 pixels
    }

    private void CreateNewCanvas(Vector2Int size)
    {
        DestroyCanvas();
        canvas = new CanvasCell[size.x, size.y]; // create a canvas of cells

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                canvas[x, y] = Instantiate(canvasCellObj, new Vector3(-x, y, 0f), Quaternion.identity, canvasParent).GetComponent<CanvasCell>();
                canvas[x, y].SetPosition(new Vector2Int(size.x - x, y)); // set the position of each cell
            }
        }

        SetCameraPosition();
        SetGrid(showGrid);
    }

    private void SetCameraPosition()
    {
        Vector3 pos = GetCamPosFromCanvasSize(new Vector2(canvasWidth, canvasHeight));

        if (moveCamCoroutine != null)
            StopCoroutine(moveCamCoroutine);
        
        moveCamCoroutine = SmoothChangeCameraPosition(pos, timeChangeCameraPos);
        StartCoroutine(moveCamCoroutine);
    }

    private Vector3 GetCamPosFromCanvasSize(Vector2 canvasSize)
    {
        foreach(vec2_vec3 pair in canvasSize2CamPosDictionary)
        {
            if (pair.size == canvasSize)
                return pair.position;
        }
        return Vector3.zero;
    } 

    private IEnumerator SmoothChangeCameraPosition(Vector3 newPos, float duration)
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > duration)
            {
                break;
            }

            cam.transform.position = Vector3.Lerp(cam.transform.position, newPos, timer / duration);
            yield return null;
        }

        cam.transform.position = newPos;
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

    public void ToggleCanvasWidth()
    {
        canvasWidth++;
        if (canvasWidth > maxCanvasSize)
            canvasWidth = 1;

        widthLabel.text = canvasWidth.ToString();
        CreateNewCanvasFromPaintingSize(new Vector2Int(canvasWidth, canvasHeight));
    }

    public void ToggleCanvasHeight()
    {
        canvasHeight++;
        if (canvasHeight > maxCanvasSize)
            canvasHeight = 1;

        heightLabel.text = canvasHeight.ToString();
        CreateNewCanvasFromPaintingSize(new Vector2Int(canvasWidth, canvasHeight));
    }

    public void ToggleShowGrid()
    {
        showGrid = !showGrid;
        SetGrid(showGrid);
    }

    private void SetGrid(bool opt)
    {
        if (showGrid)
        {
            showGridLabel.text = "on";
            foreach(CanvasCell cell in canvas)
                cell.SetOutlineWidth(1.1f);
        }
        else
        {
            showGridLabel.text = "off";
            foreach(CanvasCell cell in canvas)
                cell.SetOutlineWidth(1f);
        }
    }
}
