using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class vec2_vec3
{
    public Vector2 size;
    public Vector3 position;
}

public class RecreateSceneManager : MonoBehaviour
{
    public bool isDevRecreateMode;
    public bool useTestPaintings;

    [SerializeField] private List<vec2_vec3> canvasSize2CamPosDictionary;

    [SerializeField] private Camera cam;
    public float timeChangeCameraPos;
    private IEnumerator moveCamCoroutine;

    [SerializeField] private TextMeshProUGUI widthLabel;
    [SerializeField] private TextMeshProUGUI heightLabel;
    [SerializeField] private int maxCanvasSize;

    [SerializeField] private Color darkColor;
    [SerializeField] private Color lightColor;

    private bool showGrid = false;
    [SerializeField] private TextMeshProUGUI showGridLabel;
    [SerializeField] private Image gridButton;

    [SerializeField] private TextMeshProUGUI fillToolLabel;
    [SerializeField] private Image fillButton;

    [SerializeField] private TextMeshProUGUI brushSizeLabel;
    [SerializeField] private Button brushButton;
    
    private List<Painting> paintings;
    private List<RecreateCanvasObject> canvases;
    [HideInInspector] public RecreateCanvasObject currCanvas;
    private int currCanvasIndex;
    [SerializeField] private CanvasParentManager cpm;
    [SerializeField] private GameObject recreateCanvasObject;

    [SerializeField] private GameObject leftNavButton;
    [SerializeField] private GameObject rightNavButton;

    // dev mode variables
    private int canvasWidth = 1;
    private int canvasHeight = 1;

    void Awake()
    {
        GameHelper.SceneInit(true); // every scene must call this in Awake()
    }

    void Start()
    {
        if (useTestPaintings) paintings = GameHelper.GetTestPaintingList();
        else paintings = GameHelper.GetPaintingList();

        // create canvas objects list
        canvases = new List<RecreateCanvasObject>();

        if (isDevRecreateMode) // used in DevRecreateScene to make json data for panitnings
        {
            GameObject newCanvas = Instantiate(recreateCanvasObject);
            cpm.AddCanvas(newCanvas);

            RecreateCanvasObject script = newCanvas.GetComponent<RecreateCanvasObject>();
            script.DevModeConstructor();
            canvases.Add(script);
            currCanvas = canvases[0];
            currCanvasIndex = 0;
            SetCameraPosition();
        }
        else
        {
            // create canvas object from painting list
            foreach(Painting painting in paintings)
            {
                GameObject newCanvas = Instantiate(recreateCanvasObject);
                cpm.AddCanvas(newCanvas);

                RecreateCanvasObject script = newCanvas.GetComponent<RecreateCanvasObject>();
                script.Constructor(painting);
                canvases.Add(script);
            }

            // set the first canvas
            currCanvas = canvases[0];
            currCanvasIndex = 0;

            SetCameraPosition();

            // set colors
            print ("paintings: " + paintings[0]);
            PaintingData data =  PaintingDataHelper.GetPaintingData(paintings[0]);
            print ("data: " + data);
            print (data.canvasSize);
            print (data.colorHexValues);
            PaintbrushHelper.RemoveAllColors();
            CellColorHelper.ImportPaintingColors(data);
        }

        // disable nav buttons if only one painting
        if (canvases.Count == 1)
        {
            leftNavButton.SetActive(false);
            rightNavButton.SetActive(false);
        }

        // set brush size
        PaintbrushHelper.SetBrushSize(1);
        brushSizeLabel.text = PaintbrushHelper.GetBrushSize().ToString();

        // set grid tool
        SetGrid(showGrid);

        // set fill tool
        SetFillLabels();
    }

    public void OnFinshedButtonPressed()
    {
        print ("nice job! your painting was " + Random.Range(30f, 100f) + "% accurate to the original!");
        GameHelper.LoadScene("MuseumSceneStartCopy", true);
    }

    public void GoToLeftCanvas()
    {
        if (isDevRecreateMode) return; // disable when in dev recreate mode

        currCanvasIndex--;
        if (currCanvasIndex < 0)
        {
            currCanvasIndex = 0;
        }
        else
        {
            cpm.MoveToLeftCanvas();
            currCanvas = canvases[currCanvasIndex];
            currCanvas.SetGrid(showGrid);

            PaintingData data =  PaintingDataHelper.GetPaintingData(paintings[0]);
            PaintbrushHelper.RemoveAllColors();
            CellColorHelper.ImportPaintingColors(data);

            SetCameraPosition();
        }
    }

    public void GoToRightCanvas()
    {
        if (isDevRecreateMode) return; // disable when in dev recreate mode

        currCanvasIndex++;
        if (currCanvasIndex > canvases.Count - 1)
        {
            currCanvasIndex = canvases.Count - 1;
        }
        else
        {
            cpm.MoveToRightCanvas();
            currCanvas = canvases[currCanvasIndex];
            currCanvas.SetGrid(showGrid);

            PaintingData data =  PaintingDataHelper.GetPaintingData(paintings[0]);
            PaintbrushHelper.RemoveAllColors();
            CellColorHelper.ImportPaintingColors(data);

            SetCameraPosition();
        }
    }

    private void SetCameraPosition()
    {
        Vector3 pos = GetCamPosFromCanvasSize(currCanvas.GetPaintingSize());

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

    /*
    #####################################
    #   TOOL METHODS
    #####################################
    */
    
    
    public void ToggleCanvasWidth()
    {
        canvasWidth++;
        if (canvasWidth > maxCanvasSize)
            canvasWidth = 1;

        widthLabel.text = canvasWidth.ToString();
        currCanvas.SetCanvasSize(new Vector2Int(canvasWidth, canvasHeight));
        SetCameraPosition();
    }

    public void ToggleCanvasHeight()
    {
        canvasHeight++;
        if (canvasHeight > maxCanvasSize)
            canvasHeight = 1;

        heightLabel.text = canvasHeight.ToString();
        currCanvas.SetCanvasSize(new Vector2Int(canvasWidth, canvasHeight));
        SetCameraPosition();
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
            showGridLabel.color = darkColor;
            gridButton.color = lightColor;
        }
        else 
        {
            showGridLabel.text = "off";
            showGridLabel.color = lightColor;
            gridButton.color = darkColor;
        }
        currCanvas.SetGrid(opt);
    }

    public void ToggleSetFill()
    {
        PaintbrushHelper.ToggleSetFill();
        SetFillLabels();
    }

    private void SetFillLabels()
    {
        bool fillTool = PaintbrushHelper.GetFillTool();
        if (fillTool)
        {
            fillToolLabel.text = "on";
            fillToolLabel.color = darkColor;
            fillButton.color = lightColor;

            brushButton.interactable = false;
        }
        else 
        {
            fillToolLabel.text = "off";
            fillToolLabel.color = lightColor;
            fillButton.color = darkColor;

            brushButton.interactable = true;
        }
    }
    
    public void ToggleBrushSize()
    {
        int brushSize = PaintbrushHelper.GetBrushSize();
        brushSize++;
        PaintbrushHelper.SetBrushSize(brushSize);
        brushSizeLabel.text = PaintbrushHelper.GetBrushSize().ToString();
    }

    /*
    #####################################
    #   DevMakePaintingSceneManager
    #####################################
    */

    public void LoadPainting(PaintingData data)
    {
        currCanvas.DestroyObject();
        cpm.DeleteAllCanvases();
        canvases.Clear();

        GameObject newCanvas = Instantiate(recreateCanvasObject);
        cpm.AddCanvas(newCanvas);

        RecreateCanvasObject script = newCanvas.GetComponent<RecreateCanvasObject>();
        script.LoadData(data);
        canvases.Add(script);

        // set the first canvas
        currCanvas = canvases[0];
        currCanvasIndex = 0;

        // set colors
        CellColorHelper.ImportPaintingColors(data);

        SetCameraPosition();
    }

    public Vector2Int GetWidthHeight()
    {
        return currCanvas.GetPaintingSize();
    }

    public CanvasCell[,] GetCanvasCells()
    {
        return currCanvas.canvas;
    }
}
