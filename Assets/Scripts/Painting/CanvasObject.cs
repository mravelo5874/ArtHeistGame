using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasObject : MonoBehaviour
{
    public Painting painting;
    
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private float canvasThickness;

    void Start()
    {
        if (!painting)
        {
            Debug.LogError("No painting object found in canvas!");
            return;
        }

        SetCanvasSize();
        SetPaintingTexture();
    }

    private void SetCanvasSize()
    {
        transform.localScale = new Vector3(painting.size.x, painting.size.y, canvasThickness);
    }

    private void SetPaintingTexture()
    {
        meshRenderer.material = painting.mat;
    }
}
