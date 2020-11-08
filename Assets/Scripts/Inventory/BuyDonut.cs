using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyDonut : MonoBehaviour
{
    private void OnMouseDown()
    {
        InventoryScript.buyDonut();
    }
}