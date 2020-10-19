using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasObject : MonoBehaviour
{
    public Painting painting;
    public float outlineWidth;
    public float outlineSpeed;
    public Color outlineCompletedColor;
    
    private bool isLookedAt;
    private bool addedToList;
    private bool firstTimeLooking;
    private float timer;
    public float timeToAddToList;
    public float minPaintingFOV;
    
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
        addedToList = false;
        firstTimeLooking = true;

        transform.localScale = new Vector3(painting.size.x, painting.size.y, canvasThickness); // set canvas size
        paintingMeshRenderer.material = painting.mat; // set painting material
        canvasMeshRenderer.material.SetColor("_OutlineColor", Color.black); // set outline color to black
    }

    void Update()
    {
        // how to add painting to player list
        if (isLookedAt && !addedToList)
        {
            if (firstTimeLooking)
            {
                firstTimeLooking = false;
                timer = 0f;
            }

            // player must hold down mouse button for certain amount of time
            if (Input.GetMouseButton(0))
            {
                PlayerMovementHelper.ToggleMovement(true); // restrict player movement

                timer += Time.deltaTime;
                if (timer > timeToAddToList)
                {
                    PlayerMovementHelper.ToggleMovement(true);
                    CircleFillHelper.SetFillAmount(0f);
                    CameraHelper.ResetFOV();
                    GameHelper.AddPaintingToList(painting);
                    addedToList = true;

                    canvasMeshRenderer.material.SetFloat("_OutlineWidth", outlineWidth);
                    canvasMeshRenderer.material.SetColor("_OutlineColor", outlineCompletedColor);
                    return;
                }

                CircleFillHelper.SetFillAmount(timer / timeToAddToList);
                CameraHelper.SetFOV(Mathf.Lerp(CameraController.defaultFOV, minPaintingFOV, timer / timeToAddToList));
            }
            else
            {
                timer = 0f; // reset timer if player stops holding down button
                PlayerMovementHelper.ToggleMovement(false);
                CircleFillHelper.SetFillAmount(0f);
                CameraHelper.ResetFOV();
            }
        }
        else
        {
            if (!firstTimeLooking)
            {
                PlayerMovementHelper.ToggleMovement(false);
                CircleFillHelper.SetFillAmount(0f);
                CameraHelper.ResetFOV();
                firstTimeLooking = true;
            }
        }
    }

    public void SetLookedAt(bool opt)
    {
        if (opt == isLookedAt)
            return;

        //Debug.Log("Looking at painting? -> " + opt);

        isLookedAt = opt;

        // only outline painting if it is not added to the list
        if (!addedToList)
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
