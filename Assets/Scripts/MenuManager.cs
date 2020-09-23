using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    void Awake()
    {
        //if (!SoundHelper.IsMusicPlaying())
            //SoundHelper.PlaySong();
    }

    public void OnPlayPressed()
    {
        //SoundHelper.PlaySound(Sound.Blip1);
        SceneManager.LoadScene("SpencerScene");
    }

    public void OnOptionsPressed()
    {
        //SoundHelper.PlaySound(Sound.Blip1);
        //SceneManager.LoadScene("OptionsScene");
    }

    public void OnExitPressed()
    {
        //SoundHelper.PlaySound(Sound.Blip3);
        Application.Quit();
    }

    public void GoToIntro()
    {
        //SoundHelper.PlaySound(Sound.Blip1);
        //SceneManager.LoadScene("Intro");
    }

    public void GoToMenu()
    {
        //SoundHelper.PlaySound(Sound.Blip1);
        //SceneManager.LoadScene("Menu");
    }
}
