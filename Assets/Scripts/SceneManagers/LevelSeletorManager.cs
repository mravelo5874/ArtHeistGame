using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSeletorManager : MonoBehaviour
{
    void Awake()
    {
        GameHelper.SceneInit(true); // every scene must call this in Awake()
        AudioHelper.PlaySong(Song.hot_swing);
    }

    public void OnLevel1ScenePressed()
    {
        Level level = GameHelper.SetGetLevelData(0);
        LevelTrackerStaticClass.currentLevel = 0;
        GameHelper.LoadScene(level.sceneName, true);
    }

    public void OnLevel2ScenePressed()
    {
        // TODO: could figure out if able to press this yet?
        Level level = GameHelper.SetGetLevelData(1);
        LevelTrackerStaticClass.currentLevel = 1;
        GameHelper.LoadScene(level.sceneName, true);
    }

    public void OnLevel3ScenePressed()
    {
        Level level = GameHelper.SetGetLevelData(2);
        LevelTrackerStaticClass.currentLevel = 2;
        GameHelper.LoadScene(level.sceneName, true);
    }

    public void OnLevel4ScenePressed()
    {
        Level level = GameHelper.SetGetLevelData(3);
        LevelTrackerStaticClass.currentLevel = 3;
        GameHelper.LoadScene(level.sceneName, true);
    }

    public void OnFreePlayScenePressed()
    {
        Level level = GameHelper.SetGetLevelData(4);
        LevelTrackerStaticClass.currentLevel = 4; // TODO: not sure how to handle this yet, be careful
        GameHelper.LoadScene(level.sceneName, true);
    }

    public void OnMainMenuScenePressed()
    {
        GameHelper.LoadScene("MainMenuScene", true);
    }
}
