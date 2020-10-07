using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpencerSceneManager : MonoBehaviour
{
    void Awake()
    {
        GameHelper.SceneInit(true); // every scene must call this in Awake()
    }
}
