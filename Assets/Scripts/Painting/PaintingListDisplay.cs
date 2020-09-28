using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class PaintingListDisplayHelper
{
    public static void UpdateList()
    {
        var info = GameObject.Find("PaintingListDisplay").GetComponent<PaintingListDisplay>();
        info.UpdateList();
    }
}

public class PaintingListDisplay : MonoBehaviour
{
    [SerializeField] GameObject paintingTextObject;
    [SerializeField] Transform paintingTextParent;
    private List<GameObject> paintingNames;

    void Awake()
    {
        paintingNames = new List<GameObject>();
    }

    public void UpdateList()
    {
        List<Painting> paintings = GameHelper.GetPaintingList();

        // Remove all painting texts from canvas
        foreach(GameObject paintingText in paintingNames)
        {
            Destroy(paintingText);
        }

        foreach(Painting painting in paintings)
        {
            GameObject paintingText = Instantiate(paintingTextObject, paintingTextParent);
            paintingText.GetComponent<TextMeshProUGUI>().text = painting.title;
            paintingNames.Add(paintingText);
        }
    }
}
