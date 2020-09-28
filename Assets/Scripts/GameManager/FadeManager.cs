using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class FadeHelper
{
    public static void FadeIn(float time = GameManager.transitionTime)
    {
        FadeManager obj = FindFadeObject();
        obj.FadeIn(time);
    }

    public static void FadeOut(float time = GameManager.transitionTime)
    {
        FadeManager obj = FindFadeObject();
        obj.FadeOut(time);
    }

    private static FadeManager FindFadeObject()
    {
        GameObject obj = GameObject.Find("FadeManager");
        return obj.GetComponent<FadeManager>();
    }
}

public class FadeManager : MonoBehaviour
{
    public bool testMode;
    [SerializeField] private Image vignette;

    void Update()
    {
        if (!testMode)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            FadeIn(1.2f);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            FadeOut(1.2f);
    }
    
    public void FadeIn(float time)
    {
        StartCoroutine(FadeEnumerator(time, true));
    }

    public void FadeOut(float time)
    {
        StartCoroutine(FadeEnumerator(time, false));
    }

    private IEnumerator FadeEnumerator(float time, bool fadeIn)
    {
        float to, from;
        if (fadeIn)
        {
            to = 0f;
            from = 1f;
        }
        else
        {
            to = 1f;
            from = 0f;
        }

        vignette.color = new Color(0f, 0f, 0f, from);

        float timer = 0f;
        while(true)
        {
            if (timer > time)
                break;

            timer += Time.deltaTime;
            vignette.color = new Color(0f, 0f, 0f, Mathf.Lerp(from, to, (timer / time)));

            yield return null;
        }

        vignette.color = new Color(0f, 0f, 0f, to);
    }
}
