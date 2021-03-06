﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEditor;


public class DevMakePaintingSceneManager : MonoBehaviour
{
    [SerializeField] private RecreateSceneManager rsm;
    public const string folderSavePath = "Assets\\Resources\\PaintingObjects\\";

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
        TextAsset textAsset = Resources.Load(folder.FullName + "\\" + fileNameInput.text + "_data") as TextAsset;

        // create png from json data
        Texture2D texture = CreatePNG(data, folder.FullName + "\\" + fileNameInput.text + "_img.png");
        print ("{PNG data file created @ " +  folder.FullName + "\\" + fileNameInput.text + "_img.png");
        
        // create material 
        Material material = new Material(Shader.Find("Standard"));
        string mat_path = "Assets\\Resources\\" + fileNameInput.text + "_mat.mat";
        AssetDatabase.CreateAsset(material, mat_path);
        print ("{Material data file created @ " + "Assets\\Resources\\" + fileNameInput.text + "_mat.mat}");
        File.Move(mat_path, folder.FullName + "\\" + fileNameInput.text + "_mat.mat");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        print ("{Material data file moved to @ " + folder.FullName + "\\" + fileNameInput.text + "_mat.mat}");
        Material loaded_material = Resources.Load("PaintingObjects\\" + fileNameInput.text + "\\" + fileNameInput.text + "_mat") as Material;
        if (loaded_material != null)
        {
            loaded_material.SetTexture("_MainTex", texture);
            print ("material updated!");
        }
        else
            Debug.LogError("Loaded material is NULL!");
        

        // create painting object
        Painting painting = ScriptableObject.CreateInstance("Painting") as Painting;
        painting.title = paintingTitleInput.text;
        painting.size = data.canvasSize;
        painting.medium = "Pixel on Canvas";
        painting.artist = artistNameInput.text;
        painting.dateMade = yearMadeInput.text;
        painting.mat = material;
        painting.paintingData_Json = textAsset;
        string obj_path = "Assets\\Resources\\" + fileNameInput.text + "_obj.asset";
        AssetDatabase.CreateAsset(painting, obj_path);
        print ("{Painting data file created @ " + "Assets\\Resources\\" + fileNameInput.text + "_obj.asset}");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        File.Move(obj_path, folder.FullName + "\\" + fileNameInput.text + "_obj.asset");
        print ("{Painting data file move to @ " + folder.FullName + "\\" + fileNameInput.text + "_obj.asset}");
        
    }

    private Texture2D CreatePNG(PaintingData data, string pngSavePath)
    {
        int width = data.canvasSize.x * GameManager.canvasToPixelRatio;
        int height = data.canvasSize.y * GameManager.canvasToPixelRatio;
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;

        foreach (CellData cell in data.cellData)
        {
            string[] splitValue = cell.colorHex.Split('~');
            Color color = new Color();
            ColorUtility.TryParseHtmlString(splitValue[0], out color);
            texture.SetPixel(width - cell.pos.x, cell.pos.y, color);
        }        

        byte[] bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(pngSavePath, bytes);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

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
