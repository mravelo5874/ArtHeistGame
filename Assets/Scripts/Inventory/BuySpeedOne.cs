using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySpeedOne : MonoBehaviour
{
    public static string name = "Speed Pills";
    public static string info = "These make ye go fast.";

    private void Start()
    {
        if (InventoryScript.hasSpeedOnePills)
        {
            // make it look like it was bought
            gameObject.GetComponent<Renderer>().material.SetColor("_Color",
            new Color(gameObject.GetComponent<Renderer>().material.color.r, gameObject.GetComponent<Renderer>().material.color.g, gameObject.GetComponent<Renderer>().material.color.b, 0.1f));
            gameObject.GetComponent<Renderer>().material.EnableKeyword("_SPECULARHIGHLIGHTS_OFF");
        }
    }

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
