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

    public List<CanvasObject> objectiveCanvases;


    [SerializeField] private MuseumSection canvasesMuseumSection0;

    [SerializeField] private ObjectivesMenuScript oms;
    [SerializeField] private GameObject pausedScreen;
    [SerializeField] private Level defaultLevel;

    public static MuseumSceneManager instance;
    

    void Awake()
    {
        GameHelper.SceneInit(true); // every scene must call this in Awake()
        AudioHelper.PlaySong(Song.faster_does_it);

        instance = this;
    }

    void Start()
    {
        canvasObjects = new List<CanvasObject>();


        if (loadLevelData)
        {
            levelData = GameHelper.GetCurrentLevel();
            if (levelData == null)
            {
                levelData = defaultLevel;
                GameHelper.SetGetLevelData(0);
            }

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
        assignObjectiveCanvases(); // need to keep track of the actual objects that have the paintings we are looking for
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isPaused = !isPaused;
            pausedScreen.SetActive(isPaused);
            // cursor here....
            if (isPaused)
            {
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            GameHelper.RestartGame();
        }
    }

    public void assignObjectiveCanvases()
    {
        // need to look through the canvases and pick out which ones are the objectives
        // weird, but the top part was done weird so idk

        foreach (Painting painting in objectives)
        {
            foreach (CanvasObject canvas in canvasObjects)
            {
                if (canvas.painting == painting) // if the canvas object has the matching objective painting info?
                {
                    objectiveCanvases.Add(canvas);
                    break;
                }
            }
        }
    }

    public void removeObjectiveCanvas(Painting painting)
    {
        // remove from the list of objectiveCanvases the painting that was just aquired
        //Debug.Log("remove me from the list!");
        //don't know if its even in the list, but can try to remove anyways

        for (int i = 0; i < objectiveCanvases.Count; i++)
        {
            CanvasObject currentCanvas = objectiveCanvases[i];
            if (currentCanvas.painting == painting)
            {
                objectiveCanvases.RemoveAt(i);
                break;
            }
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
