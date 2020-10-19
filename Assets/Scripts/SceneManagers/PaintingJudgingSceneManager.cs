using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingJudgingSceneManager : MonoBehaviour
{
    List<PaintingData> paintings;

    void Start()
    {
        paintings = GameHelper.GetRecreatedPaintingList();

        StartCoroutine(LoopPaintings());
    }

    private IEnumerator LoopPaintings()
    {
        // create painting object
        
        // move camera to correct place

        // zoom into painting slowly - build up anticipation

        // show percentage of accuracy to original painting

        // show how much money is awarded

        // press button to go to next painting
        // else (if last painting) return to menu

        yield return null;
    }
}
