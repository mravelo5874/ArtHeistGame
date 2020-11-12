using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CaughtText : MonoBehaviour
{

    public static bool isDisplayed = false;

    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        isDisplayed = false;
    }

    public static void updateDisplay()
    {
        isDisplayed = true;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = !isDisplayed ? "" : "You Got Caught by the Guard!";
    }
}
