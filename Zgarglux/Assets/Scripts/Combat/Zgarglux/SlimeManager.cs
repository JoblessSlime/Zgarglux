using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManager : MonoBehaviour
{
    public SlimeInfos slimeInfos;
    
    [SerializeField]
    private Split split;

    [SerializeField]
    private HealthBar healthBar;

    private float recoveryTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(slimeInfos.healthPoint <= 0)
        {
            slimeInfos.healthPoint = 100;
            if (slimeInfos.splits.Count >= slimeInfos.MaxSplitNumber)
            {
                Death();
            }
            else
            {
                split.SplitSlime(slimeInfos.activeSplit);
            }
        }

        if (slimeInfos.RecoveringState)
        {
            recoveryTimer += Time.deltaTime;
            if (recoveryTimer > slimeInfos.hurtRecoveryTime)
            {
                slimeInfos.RecoveringState = false;
                recoveryTimer = 0;
            }
        }
        healthBar.hp = slimeInfos.healthPoint;
    }

    private void Death()
    {
        for (int i = 0; i < slimeInfos.splits.Count; i++)
        {
            Destroy(slimeInfos.splits[i]);
        }
        slimeInfos.splits.Clear();
        slimeInfos.healthPoint = 100;
    }
}
