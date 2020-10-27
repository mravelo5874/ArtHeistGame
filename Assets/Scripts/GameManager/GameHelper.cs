using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameHelper
{
    private static GameManager gm;

    // class constructor
    static GameHelper()
    {
        FindGameManager();
    }

    // every scene must call this function in Awake()
    public static void SceneInit(bool fadeIn)
    {
        FindGameManager();
        gm.SceneInit(fadeIn);
    }

    /* 
    ################################################
    #   UTILITY
    ################################################
    */

    public static List<Painting> GetGlobalPaintings()
    {
        FindGameManager();
        return gm.GetGlobalPaintings();
    }

    /* 
    ################################################
    #   SCENE MANAGEMENT
    ################################################
    */

    public static void LoadScene(int sceneNum, bool fadeOut, float time = GameManager.transitionTime)
    {   
        FindGameManager();
        gm.LoadScene(sceneNum, fadeOut, time);
    }

    public static void LoadScene(string sceneName, bool fadeOut, float time = GameManager.transitionTime)
    {   
        FindGameManager();
        gm.LoadScene(sceneName, fadeOut, time);
    }

    public static void RestartGame()
    {
        FindGameManager();
        gm.RestartGame();
    }

    /* 
    ################################################
    #   PAINTING MANAGEMENT
    ################################################
    */

    public static void ClearRecreatedPaintingList()
    {
        FindGameManager();
        gm.ClearRecreatedPaintingList();
    }

    public static void AddPaintingToRecreatedList(PaintingData data)
    {
        FindGameManager();
        gm.AddPaintingToRecreatedList(data);
    }

    public static List<PaintingData> GetRecreatedPaintingList()
    {
        FindGameManager();
        return gm.GetRecreatedPaintingList();
    }

    public static void AddPaintingToList(Painting painting)
    {
        FindGameManager();
        gm.AddPaintingToList(painting);
    }

    public static void ClearPaintingList()
    {
        FindGameManager();
        gm.ClearPaintingList();
    }

    public static List<Painting> GetPaintingList()
    {
        FindGameManager();
        return gm.GetPaintingList();
    }

    public static int GetPaintingCount()
    {
        FindGameManager();
        return gm.GetPaintingCount();
    }

    /* 
    ################################################
    #   LEVEL DATA MANAGEMENT
    ################################################
    */

    public static Level SetGetLevelData(int levelIndex)
    {
        FindGameManager();
        return gm.SetGetLevelData(levelIndex);
    }

    public static Level GetCurrentLevel()
    {
        FindGameManager();
        return gm.GetCurrentLevel();
    }

    /* 
    ################################################
    #   FIND GAME MANAGER METHOD
    ################################################
    */

    private static void FindGameManager()
    {
        if (gm == null) gm = GameObject.Find("TheGameManager").GetComponent<GameManager>();

        // GameHelper could not find TheGameManager
        if (gm == null) Debug.LogError("GameHelper could not find 'TheGameManager'");
    }
}
