using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyChanger : MonoBehaviour
{

    public TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        dropdown.value = InventoryScript.difficultySetting;
    }

    // Update is called once per frame
    void Update()
    {
        InventoryScript.difficultySetting = dropdown.value;
    }
}
