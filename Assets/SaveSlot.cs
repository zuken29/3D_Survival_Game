using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    private Button button;
    private TextMeshProUGUI buttonText;

    public int slotNumber;

    public GameObject alertUI;
    private Button yesBTN;
    private Button noBTN;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();

        yesBTN = alertUI.transform.Find("YesBTN").GetComponent<Button>();
        noBTN = alertUI.transform.Find("NoBTN").GetComponent<Button>();
    }

    public void Start()
    {
        button.onClick.AddListener(() =>
        {
            if (SaveManager.Instance.IsSLotEmpty(slotNumber))
            {
                SaveGameConfirmed();
            }
            else
            {
                DisplayOverrideWarning();
            }
        });
    }

    private void Update()
    {
        if (SaveManager.Instance.IsSLotEmpty(slotNumber))
        {
            buttonText.text = "Empty";
        }
        else
        {
            buttonText.text = PlayerPrefs.GetString("Slot" + slotNumber + "Description");
        }
    }

    public void DisplayOverrideWarning()
    {
        alertUI.SetActive(true);

        yesBTN.onClick.AddListener(() =>
        {
            SaveGameConfirmed();
            alertUI.SetActive(false);
        });

        noBTN.onClick.AddListener(() => { alertUI.SetActive(false); });
    }

    private void SaveGameConfirmed()
    {
        SaveManager.Instance.SaveGame(slotNumber);

        DateTime dt = DateTime.Now;
        string time = dt.ToString("yyyy-MM-dd HH:mm");

        string description = "Saved Game " + slotNumber + " | " + time;

        buttonText.text = description;

        PlayerPrefs.SetString("Slot" + slotNumber + "Description", description);

        SaveManager.Instance.DeselectButton();
    }
}