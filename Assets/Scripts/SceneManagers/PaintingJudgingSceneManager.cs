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

    void Awake() 
    {
        GameHelper.SceneInit(true);
    }

    void Start()
    {
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
        accuracyText.text = "Accuracy: " + System.Math.Round(GetTruePaintingAccuracy(index), 2) + "%";

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
        StartCoroutine(SmoothChangeAccuracyAlpha(false, 0.5f));

        // TODO: change their money here, cause they will earn it and then go to the shop scene

        string levelName = GameHelper.GetCurrentLevel().levelName;

        if (levelName == "Demo")
        {
            InventoryScript.money = 100;
        } else if (levelName == "Level 1")
        {
            InventoryScript.money = 200;
        } else if (levelName == "Level 2")
        {
            InventoryScript.money = 300;
        } else if (levelName == "Level 3")
        {
            InventoryScript.money = 400;
        }

        // load the shop scene so they can buy the thing...
        GameHelper.LoadScene(3, true);
    }

    private void NextPainting()
    {
         StartCoroutine(SmoothChangeAccuracyAlpha(false, 0.5f));
        cpm.MoveToRightCanvas();
        pauseLoop = false;
    }
}
