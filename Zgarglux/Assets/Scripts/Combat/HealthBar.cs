using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public float hp;
    public Slider healthBar;
    [Range(0f, 1f)] public float lerpValue;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.value = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar.value != hp / 100) 
        {
            healthBar.value = Mathf.Lerp(healthBar.value, hp / 100, lerpValue);
        }
    }
}
