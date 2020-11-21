using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyHotCold : MonoBehaviour
{
    public static string name = "Hot-or-Cold Pills";
    public static string info = "These tell ya if you're close to a painting.";
    public static Color startColor;

    private void Start()
    {
        startColor = gameObject.GetComponent<Renderer>().material.color;

        if (InventoryScript.hasHotCold)
        {
            // make it look like it was bought
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(startColor.r, startColor.g, startColor.b, 0.1f));
            gameObject.GetComponent<Renderer>().material.EnableKeyword("_SPECULARHIGHLIGHTS_OFF");
        }
    }

    private void OnMouseDown()
    {
        InventoryScript.buyHotCold();
    }

    private void OnMouseEnter()
    {
        ShopItemInfo.ChangeItemInfo(name, info);
    }

    private void OnMouseExit()
    {
        ShopItemInfo.ChangeItemInfo("", "");
    }
}
