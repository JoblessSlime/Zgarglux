using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSeed : MonoBehaviour
{
    public string seed;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CropPlanting>().grainInventory[seed] += 1;
            Destroy(this.gameObject);
        }
    }
}
