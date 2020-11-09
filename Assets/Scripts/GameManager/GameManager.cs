using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : DontDestroy<GameManager>
{
    public bool devModeActivated;
    [SerializeField] private GameObject raycastBlocker;


    public const float transitionTime = 0.5f; // time to fade into and out of a scene (total transition time is: transitionTime * 2)
    public const int canvasToPixelRatio = 16; // ratio between 1 canvas size and number of pixels

    private List<Painting> playerPaintings; // list of paintings found in museum level
    private List<PaintingData> recreatedPaintings; // list of paintings player recreated 
    [SerializeField] private PaintingPool globalPaintingPool; // all paintings in the game
    
    [SerializeField] private List<Level> levels;
    private Level currLevel = null;

    private float mouseSensitivity;
    public const float defaultMouseSensitivty = 500f;

    void Start()
    {
        playerPaintings = new List<Painting>();
        recreatedPaintings = new List<PaintingData>();
        raycastBlocker.SetActive(false);
        mouseSensitivity = defaultMouseSensitivty;
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
        raycastBlocker.SetActive(false);
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

    public List<Painting> GetGlobalPaintings()
    {
        return globalPaintingPool.paintings;
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
        raycastBlocker.SetActive(true);

        if (fadeOut)
        {
            FadeHelper.FadeOut(time);
        }
            
        yield return new WaitForSeconds(time);
        SceneManager.LoadSceneAsync(sceneName);
    }

    private IEnumerator LoadSceneCoroutine(int sceneNum, bool fadeOut, float time)
    {
        raycastBlocker.SetActive(true);

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
        if (!playerPaintings.Contains(painting))
            playerPaintings.Add(painting);

        ObjectiveHelper.CheckCompleteObjective(painting);
        PaintingListDisplayHelper.UpdateList();
        MuseumSceneManager.instance.removeObjectiveCanvas(painting);
    }

    public void ClearPaintingList()
    {
        playerPaintings.Clear();
        PaintingListDisplayHelper.UpdateList();
    }

    public int GetPaintingCount()
    {
        return playerPaintings.Count;
    }

    public List<Painting> GetPaintingList()
    {
        if (playerPaintings == null) playerPaintings = new List<Painting>();
        return playerPaintings;
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

    /* 
    ################################################
    #   GAME OPTIONS
    ################################################
    */

    public void SetSensitivity(float num)
    {
        if (num > 0f && num < 1000f)
            this.mouseSensitivity = num;
    }

    public float GetSensitivity()
    {
        return mouseSensitivity;
    }
}