using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryScript
{
    // GLOBAL SCRIPT FOR INVENTORY -> use by simply calling 'InventoryScript.Variable' or 'InventoryScript.Function()'

    public static int money = 10000;

    public static bool hasSpeedOnePills = false;
    //public static bool hasSpeedTwo = false;
    //public static bool hasSpeedThree = false;

    public static void setMoney(int newAmount) // TODO: don't use this method... (use specific item methods instead)
    {
        if (newAmount < 0)
        {
            Debug.Log("negative money not allowed");
        } else
        {
            money = newAmount;
        }
    }

    public static void buySpeedOne()
    {
        if (hasSpeedOnePills) // already has them
        {
            return; // TODO: feedback to user that they already have them? (consider feedback to user for any number of problems that occur while shopping...)
        }

        // TODO: check that they have enough money

        // TODO: make the pills greyed out? (accomplished likely in the BuySpeedOne.cs script instead of here...)

        hasSpeedOnePills = true;
        money -= 100; // TODO: could make the cost of 'speedOnePills' into a variable listed at the top for easy access
    }
}
