using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaintingJudgingSceneManager : MonoBehaviour
{
    List<PaintingData> paintings;
    private bool pauseLoop;

    // UI
    [SerializeField] private TextMeshProUGUI accuracyText;
    [SerializeField] private CanvasGroup accuracyCanvasGroup;
    [SerializeField] private Button nextButton;

    // canvas stuff:
    [HideInInspector] public RecreateCanvasObject currCanvas;
    private List<RecreateCanvasObject> canvases;
    [SerializeField] private CanvasParentManager cpm;
    [SerializeField] private GameObject recreateCanvasObject;

    // camera stuff:
    [SerializeField] private List<vec2_vec3> canvasSize2CamPosDictionary;
    [SerializeField] private Camera cam;
    public float timeChangeCameraPos;
    private IEnumerator moveCamCoroutine;

    public double accuracyCounter;

    void Awake() 
    {
        GameHelper.SceneInit(true);
    }

    void Start()
    {
        accuracyCounter = 0;
        nextButton.gameObject.SetActive(false);
        canvases = new List<RecreateCanvasObject>();
        paintings = GameHelper.GetRecreatedPaintingList();
        accuracyCanvasGroup.alpha = 0f;

        foreach (PaintingData data in paintings)
        {
            GameObject newCanvas = Instantiate(recreateCanvasObject);
            cpm.AddCanvas(newCanvas);

            RecreateCanvasObject script = newCanvas.GetComponent<RecreateCanvasObject>();
            script.LoadData(data);
            canvases.Add(script);
        }
        
        StartCoroutine(LoopPaintings());
    }

    private void SetCameraPosition()
    {
        Vector3 pos = GetCamPosFromCanvasSize(currCanvas.GetPaintingSize());

        if (moveCamCoroutine != null)
            StopCoroutine(moveCamCoroutine);
        
        moveCamCoroutine = SmoothChangeCameraPosition(pos, timeChangeCameraPos);
        StartCoroutine(moveCamCoroutine);
    }

    private Vector3 GetCamPosFromCanvasSize(Vector2 canvasSize)
    {
        foreach(vec2_vec3 pair in canvasSize2CamPosDictionary)
        {
            if (pair.size == canvasSize)
                return pair.position;
        }
        return Vector3.zero;
    }

    private IEnumerator SmoothChangeCameraPosition(Vector3 newPos, float duration)
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > duration)
            {
                break;
            }

            cam.transform.position = Vector3.Lerp(cam.transform.position, newPos, timer / duration);
            yield return null;
        }

        cam.transform.position = newPos;
    }

    private IEnumerator SmoothChangeAccuracyAlpha(bool fadeIn, float duration)
    {
        float from, to;
        if (fadeIn)
        {
            from = 0f;
            to = 1f;
        }
        else
        {
            from = 1f;
            to = 0f;
        }
        accuracyCanvasGroup.alpha = from;
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > duration)
            {
                break;
            }

            accuracyCanvasGroup.alpha = Mathf.Lerp(from, to, timer / duration);
            yield return null;
        }

        accuracyCanvasGroup.alpha = to;
    }

    private IEnumerator RandomAccuracyGenerator(float duration)
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > duration)
            {
                break;
            }

            accuracyText.text = "Accuracy: " + System.Math.Round(Random.Range(0f, 100f), 2) + "%";
            yield return null;
        }
    }

    private IEnumerator LoopPaintings()
    {
        for(int i = 0; i < paintings.Count; i++)
        {
            pauseLoop = true;
            StartCoroutine(JudgePainting(i));

            while(pauseLoop) yield return null;
        }
        
        yield return null;
    }

    private IEnumerator JudgePainting(int index)
    {
        // set current canvas
        currCanvas = canvases[index];

        // move camera to correct place
        SetCameraPosition();

        // zoom into painting slowly - build up anticipation
        StartCoroutine(SmoothChangeAccuracyAlpha(true, 0.5f));

        // show percentage of accuracy to original painting
        StartCoroutine(RandomAccuracyGenerator(3f));
        yield return new WaitForSeconds(3f);
        double accuracyNumber = System.Math.Round(GetTruePaintingAccuracy(index), 2);
        accuracyCounter += accuracyNumber;
        accuracyText.text = "Accuracy: " + accuracyNumber + "%";

        // show how much money is awarded
        
        // press button to go to next painting
        // else (if last painting) return to menu
        if (index == canvases.Count - 1)
        {
            nextButton.onClick.AddListener(EndScene);
        }
        else
        {
            nextButton.onClick.AddListener(NextPainting);
        }

        nextButton.gameObject.SetActive(true);
        yield return null;
    }

    private float GetTruePaintingAccuracy(int index)
    {
        PaintingData data = paintings[index];
        List<CellData> recreatedCanvas = data.cellData;
        List<CellData> originalCanvas = data.originalData;

        int cellTotal = recreatedCanvas.Count;
        int matchingCells = 0;
        for (int i = 0; i < cellTotal; i++)
        {
            if (recreatedCanvas[i].pos == originalCanvas[i].pos && recreatedCanvas[i].colorHex == originalCanvas[i].colorHex)
            {
                matchingCells++;
            }
        }

        return ((float)matchingCells / (float)cellTotal) * 100f;
    }

    private void EndScene()
    {
        // StartCoroutine(SmoothChangeAccuracyAlpha(false, 0.5f));

        // accuracy number = total accuracy added up (50% + 60% + 40% = 150)
        // divide by number of paintings that were analyzed
        // see if that number is above a certain threshhold...

        int accuracy = (int)Mathf.Floor((float)(accuracyCounter / canvases.Count));
        //Debug.Log(accuracy);

        // if they weren't good enough
        if (accuracy < InventoryScript.difficultyThreshholds[InventoryScript.difficultySetting])
        {
            // TODO: show message that they weren't good enough...
            accuracyText.text = "Failed: needed " + InventoryScript.difficultyThreshholds[InventoryScript.difficultySetting] + "% average";
            GameHelper.LoadScene(0, true, 5); // load the main menu...?
        } else
        {
            // they were good enough, progress through the levels...(only if they aren't on a previous level)

            if (LevelTrackerStaticClass.levelNum == 0 && LevelTrackerStaticClass.currentLevel == 0)
            {
                LevelTrackerStaticClass.levelNum = 1;
                InventoryScript.money = 100;
                // load the shop scene so they can buy the thing...
                GameHelper.LoadScene(3, true);
            }
            else if (LevelTrackerStaticClass.levelNum == 1 && LevelTrackerStaticClass.currentLevel == 1)
            {
                LevelTrackerStaticClass.levelNum = 2;
                InventoryScript.money = 200;
                // load the shop scene so they can buy the thing...
                GameHelper.LoadScene(3, true);
            }
            else if (LevelTrackerStaticClass.levelNum == 2 && LevelTrackerStaticClass.currentLevel == 2)
            {
                LevelTrackerStaticClass.levelNum = 3;
                InventoryScript.money = 300;
                // load the shop scene so they can buy the thing...
                GameHelper.LoadScene(3, true);
            }
            else if (LevelTrackerStaticClass.levelNum == 3 && LevelTrackerStaticClass.currentLevel == 3)
            {
                // unlock the demo level at the end
                LevelTrackerStaticClass.levelNum = 4;

                // load the credits scene
                GameHelper.LoadScene(7, true, 6); // slow fade for credits
            }

            
        }

        
    }

    private void NextPainting()
    {
         StartCoroutine(SmoothChangeAccuracyAlpha(false, 0.5f));
        cpm.MoveToRightCanvas();
        pauseLoop = false;
    }
}
