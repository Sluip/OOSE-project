﻿using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour
{

    private float HP;
    public Transform healthBar;
    [HideInInspector]
    public bool something;

    // Use this for initialization
    void Start()
    {
	HP = 10f;
    }

    // Update is called once per frame
    void Update()
    {

        // Change color of health bar to yellow
        if (HP <= 5)
        {
            healthBar.transform.renderer.material.color = Color.yellow;
        }

        // Change color of health bar to red
        if (HP < 3)
        {
            healthBar.transform.renderer.material.color = Color.red;
        }

        // If HP is 0 or less, destroy Player.
        if (HP <= 0)
        {

            something = true;
            Destroy(gameObject);
        }
        if (transform.position.y < 0.0f) 
        Death();
    }

    public void Hit(int damage)
    {
        HP -= damage;

        // Scale health bar down
        Vector3 temp = healthBar.localScale;
        temp.z -= damage*0.1f;
        if (temp.z < 0)
        temp.z = 0;
        healthBar.localScale = temp;
    }
    public void Death() {
    	Hit(1000);
    }
}
