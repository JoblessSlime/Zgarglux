using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField]
    private SlimeInfos slimeInfos;

    [SerializeField]
    private HealthBar healthBar;

    [SerializeField]
    private EnemyInfos enemyInfos;

    public int healthPoint;

    // Start is called before the first frame update
    void Start()
    {
        healthPoint = enemyInfos.hp;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.hp = healthPoint;
        if(healthPoint <= 0)
        {
            Instantiate(enemyInfos.seed, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
