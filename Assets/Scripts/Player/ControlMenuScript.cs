using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class ControlMenuScript : MonoBehaviour
{
    private Vector2 defaultPos;
    private Vector2 openPos;
    private bool isOpen;
    public float duration;

    private IEnumerator coroutine;

    void Start()
    {
        defaultPos = transform.position;
        openPos = new Vector2(0f, defaultPos.y);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.C))
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
}