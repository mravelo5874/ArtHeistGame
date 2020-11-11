using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySpeedOne : MonoBehaviour
{
    public static string name = "Speed Pills";
    public static string info = "These make ye go fast.";

    private void OnMouseDown()
    {
        InventoryScript.buySpeedOne();
        ShopItemInfo.ChangeItemInfo(name, info);
    }

    private void OnMouseOver()
    {
        ShopItemInfo.ChangeItemInfo(name, info);
    }
}
