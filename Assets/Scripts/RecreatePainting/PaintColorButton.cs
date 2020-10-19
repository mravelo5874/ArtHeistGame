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

    private Image img;

    void Start()
    {   
        if (isAddColorButton)
        {
            plusText.SetActive(true);
            GetComponent<Button>().onClick.AddListener(OnNewColorButtonPressed);
        }
        else
        {
            plusText.SetActive(false);
            GetComponent<Button>().onClick.AddListener(OnColorButtonPressed);
        }
    }

    public void Constructor(string _name, Color _color, bool _isAddColorButton = false)
    {
        this.isAddColorButton = _isAddColorButton;
        this.colorName = _name;
        this._color = _color;
        this.transform.SetSiblingIndex(0);

        img = GetComponent<Image>();
        img.color = _color;

        if (isAddColorButton)
        {
            plusText.SetActive(true);
            GetComponent<Button>().onClick.AddListener(OnNewColorButtonPressed);
        }
        else
        {
            plusText.SetActive(false);
            GetComponent<Button>().onClick.AddListener(OnColorButtonPressed);
        }
    }

    private void OnColorButtonPressed()
    {
        PaintbrushHelper.ChangeBrushColor(colorName);
    }

    private void OnNewColorButtonPressed()
    {
        // instantiate popup
        Instantiate(colorPopup, popupParent);
    }
}
