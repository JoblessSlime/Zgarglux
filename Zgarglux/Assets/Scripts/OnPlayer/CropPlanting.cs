using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CropPlanting : MonoBehaviour
{
    public static event Action<int, string, string> EventOnCropPlanted;

    public Dictionary<string, int> grainInventory = new Dictionary<string, int>
    {
        { "Mushroom Plant", 1 },
        { "Corn Plant", 2 },
        { "Carrot Plant", 1 },
        { "Pumpkin Plant", 1 },
        { "Radish Plant", 1 },
        { "Tomato Plant", 1 },
    };

    public string selectedCrop = "Mushroom Plant"; // Default crop
    private PlantableTerrain currentTerrain;

    public GameObject mushroomPrefab;
    public GameObject cornPrefab;
    public GameObject carrotPrefab;
    public GameObject pumpkinPrefab;
    public GameObject radishPrefab;
    public GameObject tomatoPrefab;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CropTerrain"))
        {
            Debug.Log("on crop terrain");
            currentTerrain = other.GetComponent<PlantableTerrain>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("CropTerrain"))
        {
            currentTerrain = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!HasGrain(selectedCrop))
            {
                Debug.Log("You have no more grains!");
                SwitchToAvailableCrop();
                return;
            }

            if (currentTerrain == null || !currentTerrain.HasPlaceToPlant)
            {
                Debug.Log("Can't plant here!");
                return;
            }
            
            PlantCrop();
        }
    }

    private void PlantCrop()
    {
        Instantiate(GetCropPrefab(selectedCrop), currentTerrain.transform.position, Quaternion.identity);
        grainInventory[selectedCrop]--;
        Debug.Log($"Planted {selectedCrop}. Remaining grains: {grainInventory[selectedCrop]}");

        if (EventOnCropPlanted != null)
        {
            EventOnCropPlanted.Invoke(1, "PlantCrops", selectedCrop);  // This triggers the event
            Debug.Log("eventInvoked");
        }

        if (grainInventory[selectedCrop] <= 0)
        {
            SwitchToAvailableCrop();
        }
        
        currentTerrain.HasPlaceToPlant = false;
    }

    private bool HasGrain(string crop)
    {
        return grainInventory.ContainsKey(crop) && grainInventory[crop] > 0;
    }

    private void SwitchToAvailableCrop()
    {
        foreach (var crop in grainInventory)
        {
            if (crop.Value > 0)
            {
                selectedCrop = crop.Key;
                Debug.Log($"Switched to {selectedCrop}");
                return;
            }
        }

        Debug.Log("No more grains available!");
    }

    private GameObject GetCropPrefab(string crop)
    {
        switch (crop)
        {
            case "Mushroom Plant": return mushroomPrefab;
            case "Corn Plant": return cornPrefab;
            case "Pumpkin Plant": return pumpkinPrefab;
            case "Radish Plant": return radishPrefab;
            case "Tomato Plant": return tomatoPrefab;
            case "Carrot Plant": return carrotPrefab;
            default: return null;
        }
    }
}

