using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitAI : MonoBehaviour
{
    public bool AIisActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AIisActive)
        {
            SetPriotityTarget();
        }
    }

    private void SetPriotityTarget()
    {
        // enemy a portée
            // attack

        // enemy visible
            // approcher

        // rien de tout ça
            // wander
    }
}
