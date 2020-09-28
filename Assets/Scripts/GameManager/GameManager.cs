﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : DontDestroy<GameManager>
{
    public bool devModeActivated;
    public const float transitionTime = 1f; // time to fade into and out of a scene (total transition time is: transitionTime * 2)

    private List<Painting> paintings; // list of paintings found in museum level

    new void Awake()
    {
        paintings = new List<Painting>();    
    }

    private void Update() 
    {
        if (devModeActivated)
        {
            /* We dont have a dev menu yet :)
            // press 'D' to go to the dev menu
            if (Input.GetKeyDown(KeyCode.D))
                LoadScene("DevMenu", true);
            */
        }
    }

    /* 
    ################################################
    #   SCENE INITIALIZATION
    ################################################
    */

    public void SceneInit()
    {
        StartCoroutine(SceneInitCoroutine());
    }

    private IEnumerator SceneInitCoroutine()
    {
        FadeHelper.FadeIn();
        yield return new WaitForSeconds(transitionTime);
    }

    /* 
    ################################################
    #   UTILITY
    ################################################
    */

    public void RestartGame()
    {
        LoadScene(0, true);
    }

    /* 
    ################################################
    #   SCENE MANAGEMENT
    ################################################
    */

    public void LoadScene(string sceneName, bool fadeOut, float time = transitionTime)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName, fadeOut, time));
    }

    public void LoadScene(int sceneNum, bool fadeOut, float time = transitionTime)
    {
        StartCoroutine(LoadSceneCoroutine(sceneNum, fadeOut, time));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName, bool fadeOut, float time)
    {
        if (fadeOut)
        {
            FadeHelper.FadeOut(time);
        }
            
        yield return new WaitForSeconds(time);
        SceneManager.LoadSceneAsync(sceneName);
    }

    private IEnumerator LoadSceneCoroutine(int sceneNum, bool fadeOut, float time)
    {
        if (fadeOut)
        {
            FadeHelper.FadeOut(time);
        }
            
        yield return new WaitForSeconds(time);
        SceneManager.LoadSceneAsync(sceneNum);
    }

    /* 
    ################################################
    #   PAINTING MANAGEMENT
    ################################################
    */

    public void AddPaintingToList(Painting painting)
    {
        if (!paintings.Contains(painting))
            paintings.Add(painting);
    }

    public void ClearPaintingList()
    {
        paintings.Clear();
    }
}