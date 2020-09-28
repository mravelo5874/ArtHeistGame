using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class CircleFillHelper
{
    public static void SetFillAmount(float num)
    {
        CircularFill cf = GameObject.Find("CircularFill").GetComponent<CircularFill>();
        cf.SetFillAmount(num);
    }
}

public class CircularFill : MonoBehaviour
{
    private Image circleFillBar;

    void Awake()
    {
        circleFillBar = GetComponent<Image>();
        circleFillBar.fillAmount = 0f;
    }

    public void SetFillAmount(float num)
    {
        if (num >= 0f && num <= 1f)
        {
            circleFillBar.fillAmount = num;
        }
    }
}
