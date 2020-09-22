using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasObject : MonoBehaviour
{
    public Painting painting;
    public float outlineWidth;
    public float outlineSpeed;
    
    private bool isLookedAt;
    
    [SerializeField] private MeshRenderer canvasMeshRenderer;
    [SerializeField] private MeshRenderer paintingMeshRenderer;
    [SerializeField] private float canvasThickness;

    void Start()
    {
        if (!painting)
        {
            Debug.LogError("No painting object found in canvas!");
            return;
        }

        isLookedAt = false;

        transform.localScale = new Vector3(painting.size.x, painting.size.y, canvasThickness); // set canvas size
        paintingMeshRenderer.material = painting.mat; // set painting material
    }

    public void SetLookedAt(bool opt)
    {
        if (opt == isLookedAt)
            return;

        Debug.Log("Looking at painting? -> " + opt);

        isLookedAt = opt;
        StartCoroutine(SmoothSetOutline(opt, outlineSpeed));
    }

    private IEnumerator SmoothSetOutline(bool opt, float duration)
    {
        float timer = 0f;
        float from;
        float to;

        if (opt) { from = 1.0f; to = outlineWidth; }
        else { from = outlineWidth; to = 1.0f; }

        canvasMeshRenderer.material.SetFloat("_OutlineWidth", from);

        while (true)
        {
            float num = Mathf.SmoothStep(from, to, timer / duration);
            canvasMeshRenderer.material.SetFloat("_OutlineWidth", num);

            timer += Time.deltaTime;
            if (timer >= duration) break;
            yield return null;
        }

        canvasMeshRenderer.material.SetFloat("_OutlineWidth", to);
    }
}
