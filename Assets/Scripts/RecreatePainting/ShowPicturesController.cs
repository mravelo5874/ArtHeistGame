using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ShowPicturesController : MonoBehaviour
{
    private Vector2 defaultPos;
    private Vector2 openPos;
    private bool isOpen;
    public float duration;

    private IEnumerator coroutine;

    [SerializeField] private GameObject pictureHolder;
    [SerializeField] private Transform contentHolder;

    void Start()
    {
        defaultPos = transform.position;
        openPos = new Vector2(0f, defaultPos.y);

        // get pictures from folder
        string path = Application.dataPath + CameraItem.photoFolder;
        if (Directory.Exists(path))
        {
            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                RawImage image = Instantiate(pictureHolder, contentHolder).GetComponent<RawImage>();

                Texture2D texture = new Texture2D(CameraItem.photoResolution, CameraItem.photoResolution);
                byte[] imageData = File.ReadAllBytes(file);
                texture.LoadImage(imageData);

                image.texture = texture;
            }
        }
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
}