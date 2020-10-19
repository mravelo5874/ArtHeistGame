using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewColorPopup : MonoBehaviour
{
    [SerializeField] Image colorDisplay;

    [SerializeField] Slider redSlider;
    [SerializeField] Slider greenSlider;
    [SerializeField] Slider blueSlider;

    [SerializeField] TextMeshProUGUI redText;
    [SerializeField] TextMeshProUGUI greenText;
    [SerializeField] TextMeshProUGUI blueText;

    [SerializeField] TMP_InputField inputField;

    private bool canEdit;

    void Start()
    {
        redSlider.value = 255;
        redText.text = "255";

        greenSlider.value = 255;
        greenText.text = "255";

        blueSlider.value = 255;
        blueText.text = "255";

        colorDisplay.color = Color.white;
        inputField.text = "WHITE";

        this.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
    }

    private Color GetColorFromSliderValues()
    {
        return new Color(redSlider.value / 255, greenSlider.value / 255, blueSlider.value / 255, 1.0f);
    }

    public void OnValueChanged()
    {
        redText.text = redSlider.value.ToString();
        greenText.text = greenSlider.value.ToString();
        blueText.text = blueSlider.value.ToString();

        Color color = GetColorFromSliderValues();
        colorDisplay.color = color;
    }

    public void ClosePopup()
    {
        Destroy(this.gameObject);
    }

    public void OnSetColorPressed()
    {
        CellColorHelper.AddColor(inputField.text, GetColorFromSliderValues());
        ClosePopup();
    }
}
