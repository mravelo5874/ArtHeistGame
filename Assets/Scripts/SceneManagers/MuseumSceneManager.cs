using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumSceneManager : MonoBehaviour
{   
    public bool loadLevelData;
    private Level levelData;

    [SerializeField] private Transform player;
    [SerializeField] private Transform exitPos;
    [SerializeField] private List<GameObject> doors;
    [SerializeField] private List<Painting> objectives;
    [SerializeField] private List<CanvasObject> canvasObjects;


    [SerializeField] private MuseumSection canvasesMuseumSection0;

    [SerializeField] private ObjectivesMenuScript oms;
    

    void Awake()
    {
        GameHelper.SceneInit(true); // every scene must call this in Awake()
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
                        usedPaintings.Clear();
                        unusedPaintings.Clear();
                        unusedPaintings.AddRange(global);
                        print ("reset paintings");
                        print ("unusedPainting.count: " + unusedPaintings.Count);
                    }
                    int index = Random.Range(0, unusedPaintings.Count - 1);

                    canvas.SetPainting(unusedPaintings[index]);
                    usedPaintings.Add(unusedPaintings[index]);
                    unusedPaintings.Remove(unusedPaintings[index]);
                }
                
                // set objectives
                unusedPaintings = global;
                usedPaintings.Clear();
                for (int i = 0; i < levelData.objectiveCount; i++)
                {
                    int index = Random.Range(0, unusedPaintings.Count - 1);
                    objectives.Add(unusedPaintings[index]);
                    usedPaintings.Add(unusedPaintings[index]);
                    unusedPaintings.Remove(unusedPaintings[index]);
                }
            }
            else
            {

            }
        }

        oms.SetObjectives(objectives);
    }
}

public static class MuseumSceneStaticClass
{
    public static bool gameIsPaused = false;
}
