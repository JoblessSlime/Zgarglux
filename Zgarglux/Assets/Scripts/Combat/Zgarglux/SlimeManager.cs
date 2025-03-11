using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManager : MonoBehaviour
{
    public SlimeInfos slimeInfos;
    
    [SerializeField]
    private Split split;

    [SerializeField]
    private SplitAI splitAI;

    [SerializeField]
    private HealthBar healthBar;

    public LayerMask whatIsPlayer;

    private float recoveryTimer = 0;
    private bool hasChanged;
    private float attackTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        if(attackTimer > 0.5f && hasChanged)
        {
            slimeInfos.activeSplit.GetComponent<Rigidbody>().isKinematic = true;
            slimeInfos.activeSplit.GetComponent<Rigidbody>().mass = 0.00000001f;
            slimeInfos.activeSplit.GetComponent<SplitAI>().dealingDamages = false;
            slimeInfos.activeSplit.GetComponent<CharacterController>().enabled = true;
            hasChanged = false;
        }
        if(slimeInfos.healthPoint <= 0)
        {
            slimeInfos.healthPoint = 100;
            if (slimeInfos.splits.Count >= slimeInfos.MaxSplitNumber)
            {
                Death();
            }
            else
            {
                Debug.Log("hey");
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

        if (Input.GetKeyDown("e"))
        {
            if(!splitAI.AIisActive)
            {
                Collider[] splits = Physics.OverlapSphere(transform.position, slimeInfos.absorbSplitRange, whatIsPlayer);
                if(splits.Length > 0)
                {
                    split.MergeSlime(this.gameObject, splits[0].gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            slimeInfos.activeSplit.GetComponent<Rigidbody>().isKinematic = false;
            slimeInfos.activeSplit.GetComponent<Rigidbody>().mass = 1;
            slimeInfos.activeSplit.GetComponent<SplitAI>().dealingDamages = true;
            slimeInfos.activeSplit.GetComponent<SplitAI>().alreadyAttacked = true;
            slimeInfos.activeSplit.GetComponent<CharacterController>().enabled = false;
            slimeInfos.activeSplit.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);
            attackTimer = 0;
            hasChanged = true;
        }
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
