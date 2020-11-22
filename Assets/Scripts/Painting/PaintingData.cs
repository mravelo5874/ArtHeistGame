using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PaintingDataHelper
{
    public static PaintingData GetPaintingData(Painting painting)
    {
        string fileString = painting.paintingData_Json.text;
        PaintingData paintingData = JsonUtility.FromJson<PaintingData>(fileString);
        return paintingData;
    }
}

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
