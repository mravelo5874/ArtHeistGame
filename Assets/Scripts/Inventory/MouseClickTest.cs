using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickTest : MonoBehaviour
{
    public int itemCost = 100;

    private void OnMouseDown()
    {
        // TODO: call a specific method for buying a specific item...
        InventoryScript.setMoney(InventoryScript.money - itemCost);
    }
}
