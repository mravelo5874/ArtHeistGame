using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScriptToUpdateMoney : MonoBehaviour
{

    // ALL THIS SCRIPT DOES IS UPDATE THE MONEY TEXT IN THE SHOP

    public TextMeshProUGUI moneyText;

    void Update()
    {
        moneyText.text = "$" + InventoryScript.money;
    }
}
