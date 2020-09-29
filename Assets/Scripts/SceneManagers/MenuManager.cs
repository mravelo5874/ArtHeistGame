using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    void Awake()
    {
        GameHelper.SceneInit(); // every scene must call this in Awake()
    }

    public void OnMuseumScenePressed()
    {
        GameHelper.LoadScene("SpencerScene", true);
    }

    public void OnHeistScenePressed()
    {
        GameHelper.LoadScene("HeistTestScene", true);
    }

    public void OnRecreateScenePressed()
    {
        GameHelper.LoadScene("RecreatePaintingTestScene", true);
    }

    public void OnQuitGamePressed()
    {
        Application.Quit();
    }
}
