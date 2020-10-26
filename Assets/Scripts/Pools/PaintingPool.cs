using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PaintingPool", order = 3)]
public class PaintingPool : ScriptableObject
{
    public List<Painting> paintings;
}