using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; set; }

    public Button BackBTN;

    public Slider masterSlider;
    public GameObject masterValue;
    
    public Slider musicSlider;
    public GameObject musicValue;
    
    public Slider effectsSlider;
    public GameObject effectsValue;

    private void Start()
    {
        BackBTN.onClick.AddListener(() =>
        {
            SaveManager.Instance.SaveVolumeSettings(musicSlider.value, effectsSlider.value, masterSlider.value);
        });
        StartCoroutine(LoadAndApplySettings());

    }

    IEnumerator LoadAndApplySettings()
    {
        LoadAndSetVolume();
        
        // Load GraphicsSettings
        // Load KeyBinds
        yield return new WaitForSeconds(0.1f);
    }

    private void LoadAndSetVolume()
    {
        SaveManager.VolumeSettings volumeSettings = SaveManager.Instance.LoadVolumeSettings();

        masterSlider.value = volumeSettings.master;
        musicSlider.value = volumeSettings.music;
        effectsSlider.value = volumeSettings.effects;
        
        print("Volume Settings are Loaded");
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        masterValue.GetComponent<TextMeshProUGUI>().text = "" + (masterSlider.value) + "";
        musicValue.GetComponent<TextMeshProUGUI>().text = "" + (musicSlider.value) + "";
        effectsValue.GetComponent<TextMeshProUGUI>().text = "" + (effectsSlider.value) + "";
    }
}