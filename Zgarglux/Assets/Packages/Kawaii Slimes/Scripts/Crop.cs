using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{

    public GameObject[] growthStages;
    private int currentStage = 0;
    private bool isFullyGrown = false;

    private void Start()
    {
        StartCoroutine(GrowCrop());
    }

    private IEnumerator GrowCrop()
    {
        while (currentStage < growthStages.Length - 1)
        {
            yield return new WaitForSeconds(20f);
            currentStage++;
            UpdateCropAppearence();
        }

        Debug.Log("It's fully grown !");
        isFullyGrown = true;
    }

    private void UpdateCropAppearence()
    {
        for (int i = 0; i < growthStages.Length; i++)
        {
            growthStages[i].gameObject.SetActive(i == currentStage);
        }
    }

    public bool IsFullyGrown()
    {
        return isFullyGrown;
    }
}
