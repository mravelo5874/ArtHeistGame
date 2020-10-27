using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveTag : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TMP_Text textComponent;
    public Painting painting;
    public bool isComplete;

    public void SetObjective(Painting _painting)
    {
        this.painting = _painting;
        this.isComplete = false;
        this.text.text = "- Find " + painting.title;
    }

    public void CompleteObjective()
    {
        this.isComplete = true;
        this.textComponent.fontStyle = FontStyles.Strikethrough;
    }
}
