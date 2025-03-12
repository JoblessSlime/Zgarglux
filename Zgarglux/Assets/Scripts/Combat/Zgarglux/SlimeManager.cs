using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private Vector3 spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
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
            slimeInfos.activeSplit.GetComponent<NavMeshAgent>().enabled = true;
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
                if(splits.Length > 3)
                {
                    int mergeIndex = 1;
                    for (int i = 0; i < splits.Length; i++)
                    {
                        Collider col = splits[i];
                        if (col.gameObject != this.gameObject)
                        {
                            mergeIndex = i;
                        }
                    }
                    split.MergeSlime(this.gameObject, splits[mergeIndex].gameObject);
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
            slimeInfos.activeSplit.GetComponent<NavMeshAgent>().enabled = false;
            slimeInfos.activeSplit.GetComponent<Rigidbody>().AddForce(transform.forward * 5, ForceMode.Impulse);
            attackTimer = 0;
            hasChanged = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("???");
        if (collision.gameObject.CompareTag("KillZone"))
        {
            Debug.Log("collision work");
            Death();
        }
    }

    private void Death()
    {
        for (int i = 0; i < slimeInfos.splits.Count; i++)
        {
            if (slimeInfos.splits[i] != slimeInfos.activeSplit)
            {
                Destroy(slimeInfos.splits[i]);
            }
        }
        GameObject newActive = Instantiate(slimeInfos.activeSplit, spawnPoint, Quaternion.identity);
        Destroy(slimeInfos.activeSplit);
        slimeInfos.splits.Clear();
        slimeInfos.splits.Add(newActive);
        slimeInfos.activeSplit = newActive;
        slimeInfos.healthPoint = 100;
        Debug.Log("you died");
    }
}
