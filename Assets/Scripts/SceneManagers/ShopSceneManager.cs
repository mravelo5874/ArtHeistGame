using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSceneManager : MonoBehaviour
{
    void Awake()
    {
        GameHelper.SceneInit(true); // every scene must call this in Awake()
        AudioHelper.PlaySong(Song.hep_cats);
    }

    public void OnMainMenuScenePressed()
    {
        GameHelper.LoadScene("MainMenuScene", true);
    }
}
