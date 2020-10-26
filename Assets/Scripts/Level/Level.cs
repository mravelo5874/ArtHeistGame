using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Level", order = 2)]
public class Level : ScriptableObject
{
    public string levelName;
    public string sceneName;
    public Vector3 startPos;
    public Vector3 endPos;

    public bool randomizeObjectivePaintings;
    public int objectiveCount;
    public List<Painting> objectivePaintings;

    public bool lockDoor0;

    public bool museumSection0;
}
