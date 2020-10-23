using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : DontDestroy<GameManager>
{
    public bool devModeActivated;
    public const float transitionTime = 1f; // time to fade into and out of a scene (total transition time is: transitionTime * 2)
    public const int canvasToPixelRatio = 16; // ratio between 1 canvas size and number of pixels

    private List<Painting> paintings; // list of paintings found in museum level
    private List<PaintingData> recreatedPaintings; // list of paintings player recreated    
    
    [SerializeField] List<Level> levels;
    private Level currLevel = null;

    void Start()
    {
        paintings = new List<Painting>();
        recreatedPaintings = new List<PaintingData>();
    }

    private void Update() 
    {
        if (devModeActivated)
        {
            // press 'ESC' to return to menu
            if (Input.GetKeyDown(KeyCode.Escape))
                RestartGame();
        }
    }

    /* 
    ################################################
    #   SCENE INITIALIZATION
    ################################################
    */

    public void SceneInit(bool fadeIn)
    {
        Cursor.lockState = CursorLockMode.None;

        if (fadeIn) StartCoroutine(SceneInitCoroutine());
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

    public void ClearRecreatedPaintingList()
    {
        recreatedPaintings.Clear();
    }

    public void AddPaintingToRecreatedList(PaintingData data)
    {
        if (!recreatedPaintings.Contains(data))
            recreatedPaintings.Add(data);
    }

    public List<PaintingData> GetRecreatedPaintingList()
    {
        if (recreatedPaintings == null) recreatedPaintings = new List<PaintingData>();
        return recreatedPaintings;
    }

    public void AddPaintingToList(Painting painting)
    {
        if (!paintings.Contains(painting))
            paintings.Add(painting);

        PaintingListDisplayHelper.UpdateList();
    }

    public void ClearPaintingList()
    {
        paintings.Clear();
        PaintingListDisplayHelper.UpdateList();
    }

    public int GetPaintingCount()
    {
        return paintings.Count;
    }

    public List<Painting> GetPaintingList()
    {
        if (paintings == null) paintings = new List<Painting>();
        return paintings;
    }

    /* 
    ################################################
    #   LEVEL DATA MANAGEMENT
    ################################################
    */

    public Level SetGetLevelData(int levelIndex)
    {
        if (levelIndex < levels.Count && levelIndex > 0)
            this.currLevel = levels[levelIndex];
        else
        {
            print ("Level Index does not exist, loading default level '0'");
            this.currLevel = levels[0];
        }
        
        return currLevel;
    }

    public Level GetCurrentLevel()
    {
        return currLevel;
    }
}