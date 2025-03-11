using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvesting : MonoBehaviour
{
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

    private void OnTriggerEnter(Collider other)
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
            Debug.Log($"Harvested {cropType}. New count: {cropInventory[cropType]}");
        }
        else
        {
            Debug.LogWarning($"Unknown crop type: {cropType}");
        }

        Destroy(nearbyCrop.gameObject);
        nearbyCrop = null;
    }
}