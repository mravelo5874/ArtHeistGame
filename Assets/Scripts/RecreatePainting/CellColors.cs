using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CellColor
{
    public string colorName;
    public Color color;

    public CellColor(string _colorName, Color _color)
    {
        this.colorName = _colorName;
        this.color = _color;
    }
}

public static class CellColorHelper
{
    public static CellColors cellColors;

    public static Color GetColor(string colorName)
    {
        return GetCellColor(colorName).color;
    }

    public static CellColor GetCellColor(string colorName)
    {
        FindCellColors();
        return cellColors[colorName];
    }

    public static void AddColor(string name, Color color)
    {
        FindCellColors();
        cellColors.AddColor(name, color);
    }

    public static void ImportPaintingColors(PaintingData data)
    {
        FindCellColors();
        cellColors.ImportPaintingColors(data);
    }

    private static void FindCellColors()
    {
        if (cellColors == null) cellColors = GameObject.Find("Colors").GetComponent<CellColors>();

        // could not find cell colors
        if (cellColors == null) Debug.LogError("CellColorHelper could not find 'CellColors'");
    }

    /*
    #####################################
    #   DevMakePaintingSceneManager
    #####################################
    */

    public static List<string> GetHexColors()
    {
        FindCellColors();
        return cellColors.GetHexColors();
    }
}

public class CellColors : MonoBehaviour
{
    [SerializeField] List<CellColor> colors = new List<CellColor>();

    Dictionary<string, CellColor> colorLookup;

    void OnEnable()
    {  
        //Create and populate the lookup dictionary with the list of colors.
        colorLookup = new Dictionary<string, CellColor>();
        for (int i = 0; i < colors.Count; i++)
        {
            CellColor color = colors[i];
            if (!colorLookup.ContainsKey(color.colorName))
            {
                colorLookup.Add(color.colorName, color);
            }
        }
    }

    public void ImportPaintingColors(PaintingData data)
    {
        colors.Clear();
        colorLookup.Clear();

        foreach(string hexValue in data.colorHexValues)
        {
            string[] splitValue = hexValue.Split('~');
            Color color = new Color();
            ColorUtility.TryParseHtmlString(splitValue[0], out color);
            AddColor(splitValue[1], color);
        }
    }

    public void AddColor(string name, Color color)
    {
        if (!colorLookup.ContainsKey(name))
        {
            CellColor newCellColor = new CellColor(name, color);
            colors.Add(newCellColor);
            colorLookup.Add(name, newCellColor);

            // create color button
            PaintbrushHelper.AddColorButton(newCellColor);
        }
    }

    //Method to get CellColor by name.
    //Returns null if there is no spell with matching name.    
    public CellColor GetColor(string colorName)
    {
        CellColor color = null;
        if (!colorLookup.TryGetValue(colorName, out color))
        {
            Debug.LogError("Colors - No CellColor with name: " + colorName);
        }
        return color;
    }

    //Indexer, provides a nicer to write way of accessing CellColor 
    public CellColor this[string colorName]
    {
        get { return GetColor(colorName); }        
    }

    public List<string> GetHexColors()
    {
        List<string> colorHexs = new List<string>();
        foreach(CellColor color in colors)
        {
            string hex = '#' + ColorUtility.ToHtmlStringRGBA(color.color) + '~' + color.colorName;
            colorHexs.Add(hex);
        }

        return colorHexs;
    }
}

