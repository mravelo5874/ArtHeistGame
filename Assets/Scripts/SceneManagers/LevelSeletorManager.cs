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

    public void OnDemoScenePressed()
    {
        Level level = GameHelper.SetGetLevelData(0);
        GameHelper.LoadScene(level.sceneName, true);
    }

    public void OnLevel1ScenePressed()
    {
        Level level = GameHelper.SetGetLevelData(1);
        GameHelper.LoadScene(level.sceneName, true);
    }

    public void OnLevel2ScenePressed()
    {
        Level level = GameHelper.SetGetLevelData(2);
        GameHelper.LoadScene(level.sceneName, true);
    }

    public void OnLevel3ScenePressed()
    {
        Level level = GameHelper.SetGetLevelData(3);
        GameHelper.LoadScene(level.sceneName, true);
    }

    public void OnMainMenuScenePressed()
    {
        GameHelper.LoadScene("MainMenuScene", true);
    }
}
