using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyGuardKey : MonoBehaviour
{
    private void OnMouseDown()
    {
        InventoryScript.buyGuardKey();
    }
}