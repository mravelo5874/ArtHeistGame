using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject secretUnlockText;

    void Awake()
    {
        GameHelper.SceneInit(true); // every scene must call this in Awake()
        AudioHelper.PlaySong(Song.hot_swing);

        secretUnlockText.SetActive(false);
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

    public void OnCreditsScenePressed()
    {
        GameHelper.LoadScene("CreditsScene", true);
    }

    public void OnQuitGamePressed()
    {
        Application.Quit();
    }

    public void SecretButton()
    {
        GameHelper.SecretUnlock();
        secretUnlockText.SetActive(true);
    }
}
