using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Level", order = 2)]
public class Level : ScriptableObject
{
    public string levelName;
    public string sceneName;
}
