using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintColorButton : MonoBehaviour
{
    public string _ColorName { get { return colorName; } private set { colorName = value; } }
    [SerializeField] private string colorName = "WHITE";

    public Color _Color { get { return _color; } private set { _color = value; } }
    [SerializeField] private Color _color = Color.white;

    public bool isAddColorButton;
    [SerializeField] private GameObject plusText;
    [SerializeField] private GameObject colorPopup;
    [SerializeField] private Transform popupParent;

    [SerializeField] private Image colorImage;
    [SerializeField] private GameObject selectedImage;

    void Start()
    {   
        SetSelect(false);

        if (isAddColorButton)
        {
            plusText.SetActive(true);
            colorImage.gameObject.SetActive(false);
            selectedImage.gameObject.SetActive(true);
            GetComponent<Button>().onClick.AddListener(OnNewColorButtonPressed);
        }
        else
        {
            plusText.SetActive(false);
            colorImage.gameObject.SetActive(true);
            GetComponent<Button>().onClick.AddListener(OnColorButtonPressed);
        }
    }

    public void Constructor(string _name, Color _color, bool _isAddColorButton = false)
    {
        this.isAddColorButton = _isAddColorButton;
        this.colorName = _name;
        this._color = _color;
        this.transform.SetSiblingIndex(0);

        colorImage.color = _color;

        if (isAddColorButton)
        {
            plusText.SetActive(true);
            colorImage.gameObject.SetActive(false);
            selectedImage.gameObject.SetActive(true);
            GetComponent<Button>().onClick.AddListener(OnNewColorButtonPressed);
        }
        else
        {
            plusText.SetActive(false);
            colorImage.gameObject.SetActive(true);
            GetComponent<Button>().onClick.AddListener(OnColorButtonPressed);
        }
    }

    private void OnColorButtonPressed()
    {
        PaintbrushHelper.ChangeBrushColor(colorName);
        SetSelect(true);
    }

    public void SetSelect(bool opt)
    {
        selectedImage.SetActive(opt);
    }

    private void OnNewColorButtonPressed()
    {
        // instantiate popup
        Instantiate(colorPopup, popupParent);
    }
}
