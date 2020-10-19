using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryScript
{
    public static int money = 10000;

    public static void setMoney(int newAmount)
    {
        // TODO: actually have a function for buying a specific item, and adding the item to the inventory...
        if (newAmount < 0)
        {
            Debug.Log("negative money not allowed");
        } else
        {
            money = newAmount;
        }
        
    }
}
