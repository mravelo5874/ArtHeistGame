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

    public void OnMuseumScenePressed()
    {
        GameHelper.LoadScene("MuseumTestScene", true);
    }

    public void OnLevel1ScenePressed()
    {
        //GameHelper.LoadScene("MuseumScene", true);
        GameHelper.LoadScene("MuseumScene", true);
        // do other level setup things, call a static function somewhere else? (below line doesn't work...can't find it)
        //PlayerMovement.instance.transform.position.Set(-1, 1, -44);
    }

    public void OnLevel2ScenePressed()
    {
        GameHelper.LoadScene("MuseumScene", true);
        // do other level setup things, call a static function somewhere else?
    }

    public void OnLevel3ScenePressed()
    {
        GameHelper.LoadScene("MuseumScene", true);
        // do other level setup things, call a static function somewhere else?
    }

    public void OnHeistScenePressed()
    {
        GameHelper.LoadScene("HeistTestScene", true);
    }

    public void OnRecreateScenePressed()
    {
        GameHelper.LoadScene("RecreatePaintingTestScene", true);
    }

    public void OnMainMenuScenePressed()
    {
        GameHelper.LoadScene("StartMenu", true);
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
