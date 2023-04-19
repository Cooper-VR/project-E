using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController: MonoBehaviour
{
    public float health;
    public float speed;
    private float maxHealth;
    public enemies enemyData;

    public void alterHP(float damage)
    {
        health -= damage;
    }

    private void Start()
    {
        maxHealth = enemyData.maxHealth;
        health = maxHealth;
    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
