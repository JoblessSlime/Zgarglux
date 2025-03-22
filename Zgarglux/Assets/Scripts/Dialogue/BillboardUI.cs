using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            transform.LookAt(player);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180, 0); // Keep it upright
        }
    }
}