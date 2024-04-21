using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    [SerializeField] string _volumeParameter = "MasterVolume";
    [SerializeField] string _volumeParameter2 = "SFXVolume";
    [SerializeField] AudioMixer _mixer;
    [SerializeField] Slider _masterSlider;
    [SerializeField] Slider _sfxSlider;
    [SerializeField] float _multiplier = 30f;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Toggle _toggle2;
    private bool _disableToggleEvent;
    private bool _disableToggleEvent2;

    private void Awake()
    {
        _masterSlider.onValueChanged.AddListener(HandleSliderValueChanged);
        _toggle.onValueChanged.AddListener(HandleToggleValueChanged);

        _sfxSlider.onValueChanged.AddListener(HandleSliderValueChangedSFX);
        _toggle2.onValueChanged.AddListener(HandleToggleValueChangedSFX);
    }

    private void HandleToggleValueChanged(bool enableSound)
    {
        if (_disableToggleEvent)
            return;
        if (enableSound)
        {
            _masterSlider.value = _masterSlider.maxValue;
        }
        else
        {
            _masterSlider.value = _masterSlider.minValue;
        }
    }

    private void HandleToggleValueChangedSFX(bool enableSound)
    {
        if (_disableToggleEvent2)
            return;
        if (enableSound)
        {
            _sfxSlider.value = _sfxSlider.maxValue;
        }
        else
        {
            _sfxSlider.value = _sfxSlider.minValue;
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParameter, _masterSlider.value);

        PlayerPrefs.SetFloat(_volumeParameter2, _sfxSlider.value);
    }

    void Start()
    {
        _masterSlider.value = PlayerPrefs.GetFloat(_volumeParameter, _masterSlider.value);

        _sfxSlider.value = PlayerPrefs.GetFloat(_volumeParameter2, _sfxSlider.value);
    }

    private void HandleSliderValueChanged(float value)
    {
        _mixer.SetFloat(_volumeParameter, Mathf.Log10(value) * _multiplier);
        _disableToggleEvent = true;
        _toggle.isOn = _masterSlider.value > _masterSlider.minValue;
        _disableToggleEvent = false;
    }

    private void HandleSliderValueChangedSFX(float value)
    {
        _mixer.SetFloat(_volumeParameter2, Mathf.Log10(value) * _multiplier);
        _disableToggleEvent2 = true;
        _toggle2.isOn = _sfxSlider.value > _sfxSlider.minValue;
        _disableToggleEvent2 = false;
    }

    public void GoBack()
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }
}
