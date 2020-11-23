using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyHotCold : MonoBehaviour
{
    public static string name = "Proximity Device";
    public static string info = "Displays a distance counter to track proximity to objective paintings.";
    public static Color startColor;
    public static GameObject outline;

    private void Start()
    {
        startColor = gameObject.GetComponent<Renderer>().material.color;
        outline = GameObject.Find("HotColdOutline");
        outline.GetComponent<Renderer>().enabled = false;

        if (InventoryScript.hasHotCold)
        {
            // make it look like it was bought
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(startColor.r, startColor.g, startColor.b, 0.1f));
            gameObject.GetComponent<Renderer>().material.EnableKeyword("_SPECULARHIGHLIGHTS_OFF");
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    private void OnMouseDown()
    {
        InventoryScript.buyHotCold();
    }

    private void OnMouseEnter()
    {
        ShopItemInfo.ChangeItemInfo(name, info);
        if (!InventoryScript.hasHotCold)
        {
            outline.GetComponent<Renderer>().enabled = true;
        }
    }

    private void OnMouseExit()
    {
        ShopItemInfo.ChangeItemInfo("", "");
        outline.GetComponent<Renderer>().enabled = false;
    }
}
