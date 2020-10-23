using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    void Awake()
    {
        GameHelper.SceneInit(true); // every scene must call this in Awake()
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

    public void OnLevelSelectorScenePressed()
    {
        GameHelper.LoadScene("LevelSelectorScene", true);
    }

    public void OnShopScenePressed()
    {
        GameHelper.LoadScene("ShopScene", true); ;
    }

    public void OnOptionsScenePressed()
    {
        GameHelper.LoadScene("OptionsScene", true);
    }

    public void OnQuitGamePressed()
    {
        Application.Quit();
    }
}
