using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
//using UnityEditor.Scripting.Python;

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
        exportPopupWindow.SetActive(exportPopupActive);
    }

    public void ExportPaintingData()
    {
        // create folder 
        var folder = Directory.CreateDirectory(folderSavePath + fileNameInput);
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
        File.WriteAllText(folder.FullName + fileNameInput.text + "_data.txt", jsonData);
        print ("{json data file created @ " + folder.FullName + fileNameInput.text + "_data.txt}");

        // create png image
        //PythonRunnner.
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
