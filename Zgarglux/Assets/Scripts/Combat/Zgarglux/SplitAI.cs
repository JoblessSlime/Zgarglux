using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SplitAI : MonoBehaviour
{
    public bool AIisActive = false;
    public SlimeInfos slimeInfos;

    public float impulsionForce;
    public float attackDuration;
    private float attackTime = 0;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsEnemy;

    // Patroling
    private Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    private bool enemyInSightRange, enemyInAttackRange;

    public bool dealingDamages;

    private Collider[] enemyColliderList;
    private GameObject chasedEnemy;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AIisActive)
        {
            SetPriotityTarget();
        }
        if (dealingDamages)
        {
            attackTime += Time.deltaTime;
            if (attackTime >= attackDuration)
            {
                dealingDamages = false;
                attackTime = 0;
            }
        }
    }

    private void SetPriotityTarget()
    {
        // Check for sight and attack range
        enemyInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsEnemy);
        enemyColliderList = Physics.OverlapSphere(transform.position, sightRange, whatIsEnemy);
        enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsEnemy);

        if (!enemyInSightRange && !enemyInAttackRange)
        {
            Patroling();
            chasedEnemy = null;
        }

        if (enemyInSightRange)
        {
            chasedEnemy = enemyColliderList[0].gameObject;
            if (!enemyInAttackRange)
            {
                ChasePlayer();
            }
            else if (enemyInAttackRange)
            {
                AttackPlayer();
            }
        }
        if (alreadyAttacked)
        {
            attackTime += Time.deltaTime;
            if (attackTime >= attackDuration)
            {
                alreadyAttacked = false;
                attackTime = 0;
            }
        }

        // enemy a portée
        // attack

        // enemy visible
        // approcher

        // rien de tout ça
        // wander
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        else
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(chasedEnemy.transform.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(chasedEnemy.transform);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * impulsionForce, ForceMode.Impulse);
        dealingDamages = true;
        Debug.Log("attacked");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (alreadyAttacked)
        {
            if (dealingDamages)
            {
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    collision.gameObject.GetComponent<EnemyManager>().healthPoint -= slimeInfos.damages;
                    alreadyAttacked = false;
                    dealingDamages = false;
                    attackTime = 0;
                }
            }
        }
    }
}
