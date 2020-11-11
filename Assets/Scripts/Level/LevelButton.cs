using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int buttonNumber = 0;

    public Button button;

    void Start()
    {
        if (LevelTrackerStaticClass.levelNum >= buttonNumber)
        {
            button.interactable = true;
        } else
        {
            button.interactable = false;
        }
    }
}
