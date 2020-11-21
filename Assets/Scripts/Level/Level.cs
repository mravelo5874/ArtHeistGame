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

    public bool guardEnabled;

    // used for the patrol path the guard takes in the level
    public Vector3 guardspot0;
    public Vector3 guardspot1;
    public Vector3 guardspot2;
    public Vector3 guardspot3;

    public bool lockDoors0; // locked on level 0, unlocked level 1
    public bool lockDoors1; // unlocked level 2
    public bool lockDoors2; // unlocked level 3
    public bool lockDoors3;

    // similar to lockDoors?
    public bool museumSection0;
    public bool museumSection1;
    public bool museumSection2;
    public bool museumSection3;
    public bool museumSection4;
}
