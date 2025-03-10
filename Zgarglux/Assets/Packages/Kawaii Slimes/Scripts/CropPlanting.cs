using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropPlanting : MonoBehaviour
{

    public int grainCount = 3;
    public bool hasGrain = true;
    private PlantableTerrain currentTerrain;
    public GameObject cropPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
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
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((KeyCode.E)))
        {
            if (!hasGrain)
            {
                Debug.Log("You have no more grains !");
                return;
            }

            if (currentTerrain == null || !currentTerrain.HasPlaceToPlant)
            {
                Debug.Log("You already planted something there !");
                return;

            }
            
            PlantCrop();
        }
    }

    void PlantCrop()
    {
        
        Instantiate(cropPrefab, transform.position, Quaternion.identity);
        grainCount--;
        if (grainCount <= 0)
        {
            hasGrain = false;
        }
        currentTerrain.HasPlaceToPlant = false;
    }
    
}
