using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEditor;

[System.Serializable]
public class PaintingData
{
    public Vector2Int canvasSize;
    public List<string> colorHexValues;
    public List<CellData> cellData;
    public List<CellData> originalData;

    public PaintingData(Vector2Int _canvasSize, List<string> _colorHexValues, List<CellData> _cellData, List<CellData> _originalData = null)
    {
        this.canvasSize = _canvasSize;
        this.colorHexValues = _colorHexValues;
        this.cellData = _cellData;
        this.originalData = _originalData;
    }
}

[System.Serializable]
public class CellData
{
    public Vector2Int pos;
    public string colorHex;

    public CellData(Vector2Int _pos, string _colorHex)
    {
        this.pos = _pos;
        this.colorHex = _colorHex;
    }
}

public static class PaintingDataHelper
{
    public static PaintingData GetPaintingData(Painting painting)
    {
        string fileString = painting.paintingData_Json.text;
        PaintingData paintingData = JsonUtility.FromJson<PaintingData>(fileString);
        return paintingData;
    }
}

public class DevMakePaintingSceneManager : MonoBehaviour
{
    [SerializeField] private RecreateSceneManager rsm;
    public const string folderSavePath = "Assets/ScriptableObjects/PaintingObjects/";
    public const string pythonArgumentsPath = "Assets/Python/python_arguments.txt";

    private bool exportPopupActive;
    [SerializeField] private GameObject exportPopupWindow;
    [SerializeField] private TMP_InputField fileNameInput;
    [SerializeField] private TMP_InputField paintingTitleInput;
    [SerializeField] private TMP_InputField artistNameInput;
    [SerializeField] private TMP_InputField mediumInput;
    [SerializeField] private TMP_InputField yearMadeInput;

    void Start() 
    {
        exportPopupActive = false;
        exportPopupWindow.SetActive(false);
    }

    public void ToggleExportPopupWindow()
    {
        exportPopupActive = !exportPopupActive;
        DetectCellHelper.BlockRaycasts(exportPopupActive);
        exportPopupWindow.SetActive(exportPopupActive);
    }

    public void ExportPaintingData()
    {
        // check if directory exists
        if (Directory.Exists(folderSavePath + fileNameInput.text))
        {
            Debug.LogError("Directory " + folderSavePath + fileNameInput.text + " already exists!");
            return;
        }

        // create folder 
        var folder = Directory.CreateDirectory(folderSavePath + fileNameInput.text);
        print ("{Painting data file created @ " + folder.FullName + "}");

        // create json data
        Vector2Int canvasSize = rsm.GetWidthHeight();

        List<CellData> canvasCells = new List<CellData>();
        CanvasCell[,] canvas = rsm.GetCanvasCells();
        foreach(CanvasCell cell in canvas)
        {
            canvasCells.Add(new CellData(cell.pos, cell.GetHexColor()));
        }

        List<string> hexColors = CellColorHelper.GetHexColors();
        PaintingData data = new PaintingData(canvasSize, hexColors, canvasCells);

        string jsonData = JsonUtility.ToJson(data , true);
        File.WriteAllText(folder.FullName + "\\" + fileNameInput.text + "_data.txt", jsonData);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        print ("{json data file created @ " + folder.FullName + "\\" + fileNameInput.text + "_data.txt}");
        TextAsset jsonText = new TextAsset();
        TextAsset textAsset = Resources.Load(folder.FullName + "\\" + fileNameInput.text + "_data.txt") as TextAsset;

        // create png from json data
        Texture2D texture = CreatePNG(data, folder.FullName + "\\" + fileNameInput.text + "_img.png");
        print ("{PNG data file created @ " +  folder.FullName + "\\" + fileNameInput.text + "_img.png");

        // create material 
        var material = new Material(Shader.Find("Standard"));
        material.SetTexture("_MainTex", texture);
        AssetDatabase.CreateAsset(material, folder.FullName + "\\" + fileNameInput.text + "_mat.mat");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        print ("{Material data file created @ " +  folder.FullName + "\\" + fileNameInput.text + "_mat.mat}");

        // create painting object
        Painting painting = new Painting();
        painting.title = paintingTitleInput.text;
        painting.size = data.canvasSize;
        painting.medium = "Pixel on Canvas";
        painting.artist = artistNameInput.text;
        painting.dateMade = yearMadeInput.text;
        painting.mat = material;
        painting.paintingData_Json = textAsset;
        AssetDatabase.CreateAsset(painting, folder.FullName + "\\" + fileNameInput.text + "_obj");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        print ("{Painting data file created @ " +  folder.FullName + "\\" + fileNameInput.text + "_obj}");
    }

    TextAsset ConvertStringToTextAsset(string text) 
    {
        string temporaryTextFileName = "TemporaryTextFile";
        File.WriteAllText(Application.dataPath +  "/Resources/" + temporaryTextFileName + ".txt", text);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        TextAsset textAsset = Resources.Load(temporaryTextFileName) as TextAsset;
        return textAsset;
     }

    private Texture2D CreatePNG(PaintingData data, string pngSavePath)
    {
        int width = data.canvasSize.x * GameManager.canvasToPixelRatio;
        int height = data.canvasSize.y * GameManager.canvasToPixelRatio;
        Texture2D texture = new Texture2D(width, height);

        foreach (CellData cell in data.cellData)
        {
            string[] splitValue = cell.colorHex.Split('~');
            Color color = new Color();
            ColorUtility.TryParseHtmlString(splitValue[0], out color);
            texture.SetPixel(cell.pos.x, cell.pos.y, color);
        }        

        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(pngSavePath, bytes);

        return texture;
    }

    // public void ImportPaintingData()
    // {
    //     if (File.Exists(jsonSavePath + paintingNameInput.text + "_data.txt"))
    //     {
    //         print ("{Painting data file found @ " + jsonSavePath + paintingNameInput.text + "_data.txt}");
            
    //         string fileString = File.ReadAllText(jsonSavePath + paintingNameInput.text + "_data.txt");
    //         PaintingData paintingData = JsonUtility.FromJson<PaintingData>(fileString);

    //         rsm.LoadPainting(paintingData);
    //     }
    // }
}
