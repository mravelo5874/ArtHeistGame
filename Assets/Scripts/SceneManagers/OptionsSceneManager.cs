﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsSceneManager : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private TextMeshProUGUI musicVolumeValue;

    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private TextMeshProUGUI mouseSensitivityValue;

    void Awake()
    {
        GameHelper.SceneInit(true); // every scene must call this in Awake()
    }

    void Start()
    {
        float vol = AudioHelper.GetMusicVolume();
        musicVolumeSlider.value = vol;
        musicVolumeValue.text = Mathf.RoundToInt(vol * 100f).ToString();

        float sens = GameHelper.GetSensitivity();
        mouseSensitivitySlider.value = sens;
        mouseSensitivityValue.text = Mathf.RoundToInt(sens).ToString();
    }

    public void OnVolumeSliderChanged()
    {
        float vol = musicVolumeSlider.value;
        AudioHelper.ChangeMusicVolume(vol);
        musicVolumeValue.text = Mathf.RoundToInt(vol * 100f).ToString();
    }

    public void OnMouseSensitivitySliderChange()
    {
        float sens = mouseSensitivitySlider.value;
        GameHelper.SetSensitivity(sens);
        mouseSensitivityValue.text = Mathf.RoundToInt(sens).ToString();
    }

    public void OnMainMenuScenePressed()
    {
        GameHelper.LoadScene("MainMenuScene", true);
    }
}
