using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemInfo : MonoBehaviour
{

    public static GameObject itemName;
    public static GameObject itemInfo;

    void Start()
    {
        itemName = GameObject.Find("Item Name");
        itemInfo = GameObject.Find("Item Information");
        ChangeItemInfo("", "");
    }

    public static void ChangeItemInfo(string nameText, string infoText)
    {
        itemName.GetComponent<Text>().text = nameText;
        itemInfo.GetComponent<Text>().text = infoText;
    }
}
