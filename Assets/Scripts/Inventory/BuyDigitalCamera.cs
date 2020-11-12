using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyDigitalCamera : MonoBehaviour
{
    public static string name = "Digital Camera";
    public static string info = "This allows you to take pictures.";

    private void OnMouseDown()
    {
        InventoryScript.buyDigitalCamera();
    }

    private void OnMouseOver()
    {
        ShopItemInfo.ChangeItemInfo(name, info);
    }
}