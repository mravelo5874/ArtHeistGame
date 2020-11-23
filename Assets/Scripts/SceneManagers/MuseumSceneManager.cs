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

    public static bool IsObjective(Painting painting)
    {
        var info = GameObject.Find("MuseumSceneManager").GetComponent<MuseumSceneManager>();
        return info.IsObjective(painting);
    }
}

public class MuseumSceneManager : MonoBehaviour
{   
    public bool loadLevelData;
    private Level levelData;
    private bool isPaused = false;

    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject exitPos;

    [SerializeField] private List<GameObject> doors;
    [SerializeField] private List<Painting> objectives;
    [SerializeField] private List<CanvasObject> canvasObjects;
    [SerializeField] private GameObject roof;

    [SerializeField] private GameObject guard;
    [SerializeField] private GameObject guardspot0;
    [SerializeField] private GameObject guardspot1;
    [SerializeField] private GameObject guardspot2;
    [SerializeField] private GameObject guardspot3;

    public List<CanvasObject> objectiveCanvases;

    [SerializeField] private MuseumSection canvasesMuseumSection0;
    [SerializeField] private MuseumSection canvasesMuseumSection1;
    [SerializeField] private MuseumSection canvasesMuseumSection2;
    [SerializeField] private MuseumSection canvasesMuseumSection3;
    [SerializeField] private MuseumSection canvasesMuseumSection4;

    [SerializeField] private ObjectivesMenuScript oms;
    [SerializeField] private GameObject pausedScreen;
    [SerializeField] private Level defaultLevel;

    public static MuseumSceneManager instance;

    public GameObject playerObj;
    //public GameObject guardObj;
    

    void Awake()
    {
        GameHelper.SceneInit(true); // every scene must call this in Awake()
        AudioHelper.PlaySong(Song.faster_does_it);

        instance = this;

        playerObj = GameObject.Find("Player");

        //guardObj = GameObject.Find("Guard");
    }

    void Start()
    {
        canvasObjects = new List<CanvasObject>();

        if (loadLevelData)
        {
            print ("loading level data...");

            levelData = GameHelper.GetCurrentLevel();
            if (levelData == null)
            {
                levelData = defaultLevel;
                GameHelper.SetGetLevelData(0);
            }

            print ("level: " + levelData.name);

            LevelTrackerStaticClass.updateHackTracker = 0;

            // should have 3 doors, but could error here if not properly set
            doors[0].SetActive(levelData.lockDoors0);
            doors[1].SetActive(levelData.lockDoors1);
            doors[2].SetActive(levelData.lockDoors2);
            doors[3].SetActive(levelData.lockDoors3);

            guard.SetActive(levelData.guardEnabled);
            // guard starts at the first spot? (should probably be tested) (makes more sense to not start exactly on the first spot but a little away from it? (could have data for this))
            guard.transform.position = levelData.guardspot0;

            guardspot0.transform.position = levelData.guardspot0;
            guardspot1.transform.position = levelData.guardspot1;
            guardspot2.transform.position = levelData.guardspot2;
            guardspot3.transform.position = levelData.guardspot3;    

            // add correct canvases
            if (levelData.museumSection0)
            {
                canvasObjects.AddRange(canvasesMuseumSection0.canavses);
            }
            if (levelData.museumSection1)
            {
                canvasObjects.AddRange(canvasesMuseumSection1.canavses);
            }
            if (levelData.museumSection2)
            {
                canvasObjects.AddRange(canvasesMuseumSection2.canavses);
            }
            if (levelData.museumSection3)
            {
                canvasObjects.AddRange(canvasesMuseumSection3.canavses);
            }
            if (levelData.museumSection4)
            {
                canvasObjects.AddRange(canvasesMuseumSection4.canavses);
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
                
                // set objective paintings
                GameHelper.ClearPaintingList();
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


            player.SetPosition(levelData.startPos);
            playerObj.transform.position = levelData.startPos;
            exitPos.transform.position = levelData.endPos;        
        }

        oms.SetObjectives(objectives);
        assignObjectiveCanvases(); // need to keep track of the actual objects that have the paintings we are looking for
    }

    void Update()
    {   

        // hacky hack stuff
        //if (LevelTrackerStaticClass.updateHackTracker < 1000)
        //{
        //    LevelTrackerStaticClass.updateHackTracker += 1;
        //    player.SetPosition(levelData.startPos);
        //    playerObj.transform.position = levelData.startPos;
        //    playerObj.transform.position = playerObj.transform.position + new Vector3(0, LevelTrackerStaticClass.updateHackTracker, 0);
        //    print("reset position" + LevelTrackerStaticClass.updateHackTracker);
        //}


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
                    canvas.isObjective = true;
                    print("set objective canvas bool!");
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

    public bool IsObjective(Painting painting)
    {
        if (objectives.Contains(painting))
            return true;
        else
            return false;
    }
}
