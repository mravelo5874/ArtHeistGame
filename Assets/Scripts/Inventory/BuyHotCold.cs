using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyHotCold : MonoBehaviour
{
    public static string name = "Hot-or-Cold Pills";
    public static string info = "These tell ya if you're close to a painting.";

    private void OnMouseDown()
    {
        InventoryScript.buyHotCold();
        ShopItemInfo.ChangeItemInfo(name, info);
    }
}
