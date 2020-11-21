using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaintingIntroManager : MonoBehaviour
{
    public static PaintingIntroManager instance;

    private CanvasGroup canvasGroup;
    public int timeToShowIntro;
    public float fadeTime;

    [SerializeField] private TextMeshProUGUI paintingText;
    [SerializeField] private TextMeshProUGUI timeLeftText;
    [SerializeField] private RawImage paintingImage;
    [SerializeField] private GameObject raycastBlocker;

    void Awake()
    {
        instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    public static void SetStartPainting_static(Painting painting)
    {
        instance.SetStartPainting(painting);
    }

    public void SetStartPainting(Painting painting)
    {
        StopAllCoroutines();

        // set stuff
        paintingText.text = "Recreating: " + painting.title;
        timeLeftText.text = "Time left: " + timeToShowIntro;
        paintingImage.texture = painting.texture;
        paintingImage.rectTransform.sizeDelta = new Vector2(painting.size.x * 100f, painting.size.y * 100f);
        raycastBlocker.SetActive(true);

        StartCoroutine(PaintingIntroCoroutine());
    }

    private IEnumerator PaintingIntroCoroutine()
    {
        DetectCellHelper.BlockRaycasts(true);

        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= fadeTime)
                break;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeTime);
            yield return null;
        }

        for (int i = timeToShowIntro; i >= 0; i--)
        {
            timeLeftText.text = "Time left: " + i;
            yield return new WaitForSeconds(1f);
        }

        timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= fadeTime)
                break;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeTime);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        DetectCellHelper.BlockRaycasts(false);
        raycastBlocker.SetActive(false);
    }

    public void SkipIntro()
    {
        StopAllCoroutines();
        StartCoroutine(SkipCoroutine());
    }

    private IEnumerator SkipCoroutine()
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer >= fadeTime)
                break;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeTime);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        DetectCellHelper.BlockRaycasts(false);
        raycastBlocker.SetActive(false);
    }
}
