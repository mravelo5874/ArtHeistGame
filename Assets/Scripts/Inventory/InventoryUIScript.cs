using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIScript : MonoBehaviour
{

    public GameObject PillsImageEmpty;
    public GameObject PillsImage;

    public GameObject GameItem2Empty;
    public GameObject GameItem2;

    public GameObject GameItem3Empty;
    public GameObject GameItem3;

    public GameObject GameItem4Empty;
    public GameObject GameItem4;

    // Start is called before the first frame update
    void Start()
    {
        // for each inventory item, see if you need to change the UI

        PillsImageEmpty.SetActive(!InventoryScript.hasSpeedOnePills);
        PillsImage.SetActive(InventoryScript.hasSpeedOnePills);

        GameItem2Empty.SetActive(!InventoryScript.hasHotCold);
        GameItem2.SetActive(InventoryScript.hasHotCold);

        GameItem3Empty.SetActive(!InventoryScript.hasDigitalCamera);
        GameItem3.SetActive(InventoryScript.hasDigitalCamera);

        GameItem4Empty.SetActive(true);
        GameItem4.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        PillsImageEmpty.SetActive(!InventoryScript.hasSpeedOnePills);
        PillsImage.SetActive(InventoryScript.hasSpeedOnePills);

        GameItem2Empty.SetActive(!InventoryScript.hasHotCold);
        GameItem2.SetActive(InventoryScript.hasHotCold);

        GameItem3Empty.SetActive(!InventoryScript.hasDigitalCamera);
        GameItem3.SetActive(InventoryScript.hasDigitalCamera);

        GameItem4Empty.SetActive(true);
        GameItem4.SetActive(false);
    }
}
