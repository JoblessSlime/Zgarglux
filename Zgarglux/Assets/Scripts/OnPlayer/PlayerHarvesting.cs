using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHarvesting : MonoBehaviour
{
    
    public TextMeshProUGUI mushroomCropsText;
    public TextMeshProUGUI cornCropsText;
    public TextMeshProUGUI carrotCropsText;
    public TextMeshProUGUI pumpkinCropsText;
    public TextMeshProUGUI radishCropsText;
    public TextMeshProUGUI tomatoCropsText;
    
    public Dictionary<string, int> cropInventory = new Dictionary<string, int>
    {
        { "Mushroom Plant", 0 },
        { "Corn Plant", 0 },
        { "Carrot Plant", 0 },
        { "Pumpkin Plant", 0 },
        { "Radish Plant", 0 },
        { "Tomato Plant", 0 }

    };

    public Crop nearbyCrop;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Crop"))
        {
            Crop crop = other.GetComponent<Crop>();
            if (crop != null && crop.IsFullyGrown())
            {
                Debug.Log("Nearby crop detected.");
                nearbyCrop = crop;
            }  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crop") && nearbyCrop != null)
        {
            nearbyCrop = null;
        }
    }

    private void Start()
    {
        UpdateHarvestedCropsUI();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && nearbyCrop != null && nearbyCrop.IsFullyGrown())
        {
            Debug.Log("Let's harvest it!");
            HarvestCrop();
        }
    }

    private void HarvestCrop()
    {
        string cropType = nearbyCrop.cropType; 
        if (cropInventory.ContainsKey(cropType))
        {
            cropInventory[cropType]++;
            UpdateHarvestedCropsUI();
            Debug.Log($"Harvested {cropType}. New count: {cropInventory[cropType]}");
        }
        else
        {
            Debug.LogWarning($"Unknown crop type: {cropType}");
        }

        Destroy(nearbyCrop.gameObject);
        nearbyCrop = null;
    }
    private void UpdateHarvestedCropsUI()
    {

        mushroomCropsText.text = ": " + cropInventory["Mushroom Plant"];
        cornCropsText.text = ": " + cropInventory["Corn Plant"];
        carrotCropsText.text = ": " + cropInventory["Carrot Plant"];
        pumpkinCropsText.text = ": " + cropInventory["Pumpkin Plant"];
        radishCropsText.text = ": " + cropInventory["Radish Plant"];
        tomatoCropsText.text = ": " + cropInventory["Tomato Plant"];

    }
}