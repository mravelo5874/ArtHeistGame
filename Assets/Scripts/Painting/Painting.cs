using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Painting", order = 1)]
public class Painting : ScriptableObject
{
    public string title;
    public string artist;
    public string medium;
    public string dateMade;

    public Vector2Int size;
    public Material mat;
    public TextAsset paintingData_Json;
    public Texture2D texture;
}
