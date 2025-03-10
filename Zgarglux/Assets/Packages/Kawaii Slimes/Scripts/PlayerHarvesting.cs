using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvesting : MonoBehaviour
{
    public int cropCount = 0;

    public Crop nearbycrop;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crop"))
        {
            Crop crop = other.GetComponent<Crop>();
            if (crop != null && crop.IsFullyGrown())
            {
                Debug.Log("Nearbycrop");
                nearbycrop = crop;
            }  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crop") && nearbycrop != null)
        {
            nearbycrop = null;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && nearbycrop != null && nearbycrop.IsFullyGrown())
        {
            Debug.Log("let's harvest it !");
            HarvestCrop();
        }
    }

    private void HarvestCrop()
    {
        cropCount++;
        Destroy(nearbycrop.gameObject);
        nearbycrop = null;
    }
}
