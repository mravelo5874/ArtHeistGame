using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ObjectivesMenuScript : MonoBehaviour
{
    private Vector2 defaultPos;
    private Vector2 openPos;
    private bool isOpen;
    public float duration;

    [SerializeField] private GameObject objectiveTagObject;
    private List<ObjectiveTag> objectiveTags;

    private IEnumerator coroutine;

    void Start()
    {
        defaultPos = transform.position;
        openPos = new Vector2(defaultPos.x - Screen.width / 2.5f, defaultPos.y);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            if (!isOpen)
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }

                isOpen = true;
                coroutine = SmoothMoveMenu(openPos, duration);
                StartCoroutine(coroutine);
            }
        } 
        else
        {
            if (isOpen)
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }

                isOpen = false;
                coroutine = SmoothMoveMenu(defaultPos, duration);
                StartCoroutine(coroutine);
            }
        }
    }

    private IEnumerator SmoothMoveMenu(Vector2 target, float duration)
    {
        float timer = 0f;

        while (true)
        {
            this.transform.position = Vector2.Lerp(this.transform.position, target, timer / duration);

            timer += Time.deltaTime;
            if (timer >= duration) break;
            yield return null;
        }

        this.transform.position = target;
    }

    public void SetObjectives(List<Painting> paintings)
    {
        objectiveTags = new List<ObjectiveTag>();

        foreach (Painting painting in paintings)
        {
            ObjectiveTag tag = Instantiate(objectiveTagObject, this.transform).GetComponent<ObjectiveTag>();
            tag.SetObjective(painting);
            objectiveTags.Add(tag);
        }
    }

    public void CheckCompleteObjective(Painting painting)
    {
        foreach (ObjectiveTag objective in objectiveTags)
        {
            if (objective.painting == painting && !objective.isComplete)
            {
                objective.CompleteObjective();
            }
        }
    }
}

public static class ObjectiveHelper
{
    public static void CheckCompleteObjective(Painting painting)
    {
        ObjectivesMenuScript obj = GameObject.Find("ObjectiveMenu").GetComponent<ObjectivesMenuScript>();
        obj.CheckCompleteObjective(painting);
    }
}
