using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MuseumHelper
{
    public static void SetRoofActive(bool opt)
    {
        var info = GameObject.Find("MuseumSceneManager").GetComponent<MuseumSceneManager>();
        info.SetRoofActive(opt);
    }

    public static bool AllObjectivesComplete()
    {
        var info = GameObject.Find("MuseumSceneManager").GetComponent<MuseumSceneManager>();
        return info.AllObjectivesComplete();
    }

    public static bool GetPaused()
    {
        var info = GameObject.Find("MuseumSceneManager").GetComponent<MuseumSceneManager>();
        return info.GetPaused();
    }
}

public class MuseumSceneManager : MonoBehaviour
{   
    public bool loadLevelData;
    private Level levelData;
    private bool isPaused = false;

    [SerializeField] private Transform player;
    [SerializeField] private Transform exitPos;
    [SerializeField] private List<GameObject> doors;
    [SerializeField] private List<Painting> objectives;
    [SerializeField] private List<CanvasObject> canvasObjects;
    [SerializeField] private GameObject roof;


    [SerializeField] private MuseumSection canvasesMuseumSection0;

    [SerializeField] private ObjectivesMenuScript oms;
    [SerializeField] private GameObject pausedScreen;
    

    void Awake()
    {
        GameHelper.SceneInit(true); // every scene must call this in Awake()
        AudioHelper.PlaySong(Song.faster_does_it);
    }

    void Start()
    {
        canvasObjects = new List<CanvasObject>();


        if (loadLevelData)
        {
            levelData = GameHelper.GetCurrentLevel();

            player.position = levelData.startPos;
            exitPos.position = levelData.endPos;
            doors[0].SetActive(levelData.lockDoor0);

            if (levelData.museumSection0)
            {
                canvasObjects.AddRange(canvasesMuseumSection0.canavses);
            }

            if (levelData.randomizeObjectivePaintings)
            {
                List<Painting> global = GameHelper.GetGlobalPaintings();
                List<Painting> unusedPaintings = new List<Painting>();
                List<Painting> usedPaintings = new List<Painting>();
                unusedPaintings.AddRange(global);

                // set canvas paintings
                foreach (CanvasObject canvas in canvasObjects)
                {
                    if (unusedPaintings.Count <= 0)
                    {
                        unusedPaintings.Clear();
                        unusedPaintings.AddRange(global);
                    }
                    int index = Random.Range(0, unusedPaintings.Count - 1);

                    canvas.SetPainting(unusedPaintings[index]);
                    if (!usedPaintings.Contains(unusedPaintings[index]))
                    {
                        usedPaintings.Add(unusedPaintings[index]);
                    }
                    unusedPaintings.Remove(unusedPaintings[index]);
                }
                
                for (int i = 0; i < levelData.objectiveCount; i++)
                {
                    int index = Random.Range(0, usedPaintings.Count - 1);
                    objectives.Add(usedPaintings[index]);
                    usedPaintings.Remove(usedPaintings[index]);
                }
            }
            else
            {

            }
        }

        oms.SetObjectives(objectives);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isPaused = !isPaused;
            pausedScreen.SetActive(isPaused);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            GameHelper.RestartGame();
        }
    }
       

    public void SetRoofActive(bool opt)
    {
        roof.SetActive(opt);
    }

    public bool AllObjectivesComplete()
    {
        return oms.AllObjectivesComplete();
    }

    public bool GetPaused()
    {
        return isPaused;
    }
}
