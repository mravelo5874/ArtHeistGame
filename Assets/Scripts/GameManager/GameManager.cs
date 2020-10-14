using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : DontDestroy<GameManager>
{
    public bool devModeActivated;
    public const float transitionTime = 1f; // time to fade into and out of a scene (total transition time is: transitionTime * 2)
    public const int canvasToPixelRatio = 16; // ratio between 1 canvas size and number of pixels

    private List<Painting> paintings; // list of paintings found in museum level

    [SerializeField] private List<Painting> testPaintings; // painting to be used for testing (recreate scene/dev mode)

    new void Awake()
    {
        paintings = new List<Painting>();
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

    public int money = 1000;
    public TextMeshProUGUI moneyText;
    
    public void buySomething(int itemCost)
    {
        money -= itemCost;
        moneyText.text = "Money: $" + money;
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

    public List<Painting> GetPaintingList()
    {
        return paintings;
    }

    public List<Painting> GetTestPaintingList()
    {
        return testPaintings;
    }
}