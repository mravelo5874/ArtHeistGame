using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XchelSceneManager : MonoBehaviour
{
    void Awake()
    {
        GameHelper.SceneInit(); // every scene must call this in Awake()
    }
}
