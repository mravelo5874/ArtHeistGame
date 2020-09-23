using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CellColor
{
    public string colorName;
    public Color color;
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

    private static void FindCellColors()
    {
        if (cellColors == null) cellColors = GameObject.Find("Colors").GetComponent<CellColors>();

        // could not find cell colors
        if (cellColors == null) Debug.LogError("CellColorHelper could not find 'CellColors'");
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
}

