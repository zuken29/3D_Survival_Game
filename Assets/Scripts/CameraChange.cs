using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject FirstCam;
    public GameObject ThirdCam;
    public int CamMode;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (CamMode == 1)
            {
                CamMode = 0;
            }
            else
            {
                CamMode += 1;
            }

            StartCoroutine(CamChange());
        }
    }

    IEnumerator CamChange()
    {
        yield return new WaitForSeconds(0.01f);

        if (CamMode == 0)
        {
            FirstCam.SetActive(true);
            ThirdCam.SetActive(false);
        }
        else
        {
            FirstCam.SetActive(false);
            ThirdCam.SetActive(true);
        }
    }
}