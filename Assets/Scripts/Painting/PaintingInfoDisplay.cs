using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class PaintingInfoDisplayHelper
{
    public static void SetPaintingInfo(Painting painting)
    {
        var info = GameObject.Find("PaintingInfoDisplay").GetComponent<PaintingInfoDisplay>();
        info.SetPaintingInfo(painting);
    }

    public static void SetInfoDisplayActive(bool opt)
    {
        var info = GameObject.Find("PaintingInfoDisplay").GetComponent<PaintingInfoDisplay>();
        info.SetInfoDisplayActive(opt);
    }
}

public class PaintingInfoDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI artist;
    [SerializeField] private TextMeshProUGUI medium;
    [SerializeField] private TextMeshProUGUI date;
    [SerializeField] private TextMeshProUGUI size;

    [SerializeField] private float fadeDuration;

    private CanvasGroup canvasGroup;
    private bool isActive;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        isActive = false;
        canvasGroup.alpha = 0f;
    }

    public void SetPaintingInfo(Painting painting)
    {
        title.text = "title: " + painting.title;
        artist.text = "artist: " + painting.artist;
        medium.text = "medium: " + painting.medium;
        date.text = "date: " + painting.dateMade;
        size.text = "size: " + painting.size.x + "x" + painting.size.y;
    }

    public void SetInfoDisplayActive(bool opt)
    {
        if (opt == isActive)
            return;

        //Debug.Log("Looking at painting? -> " + opt);

        isActive = opt;
        StartCoroutine(SmoothSetAlpha(opt, fadeDuration));
    }

    private IEnumerator SmoothSetAlpha(bool opt, float duration)
    {
        float timer = 0f;
        float from;
        float to;

        if (opt) { from = 0f; to = 1f; }
        else { from = 1f; to = 0f; }

        canvasGroup.alpha = from;

        while (true)
        {
            canvasGroup.alpha = Mathf.SmoothStep(from, to, timer / duration);

            timer += Time.deltaTime;
            if (timer >= duration) break;
            yield return null;
        }

        canvasGroup.alpha = to;
    }
}
