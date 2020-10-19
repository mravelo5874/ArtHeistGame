using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasParentManager : MonoBehaviour
{
    private const float distanceBtwnCanvases = 150f;
    private List<GameObject> canvases;
    private bool isMoving;

    [SerializeField] private float moveDuration;

    void Awake()
    {
        canvases = new List<GameObject>();
    }

    public void AddCanvas(GameObject canvas)
    {
        canvas.transform.parent = this.transform;
        Vector3 newPos = this.transform.position;
        newPos.x += (distanceBtwnCanvases * canvases.Count);
        //print ("new pos: " + newPos);
        canvas.transform.position = newPos;
        canvases.Add(canvas);
    }

    public void DeleteAllCanvases()
    {
        canvases.Clear();
    }

    public void MoveToLeftCanvas()
    {
        if (isMoving) return;
        StartCoroutine(SmoothMoveCanvas(this.transform.position.x, this.transform.position.x + distanceBtwnCanvases, moveDuration));
    }

    public void MoveToRightCanvas()
    {
        if (isMoving) return;
        StartCoroutine(SmoothMoveCanvas(this.transform.position.x, this.transform.position.x - distanceBtwnCanvases, moveDuration));
    }

    private IEnumerator SmoothMoveCanvas(float from, float to, float duration)
    {
        isMoving = true;
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > duration)
            {
                break;
            }
            Vector3 newPos = this.transform.position;
            newPos.x = Mathf.Lerp(from, to, timer / duration);
            this.transform.position = newPos;
            yield return null;
        }
        Vector3 endPos = this.transform.position;
        endPos.x = to;
        this.transform.position = endPos;
        isMoving = false;
    }
}
