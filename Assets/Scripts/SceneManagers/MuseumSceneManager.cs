using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumSceneManager : MonoBehaviour
{   
    public bool loadLevelData;
    private Level levelData;

    void Awake()
    {
        GameHelper.SceneInit(true); // every scene must call this in Awake()
    }

    void Start()
    {
        if (loadLevelData)
        {
            levelData = GameHelper.GetCurrentLevel();
        }
    }
}
