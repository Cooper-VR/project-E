using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController: MonoBehaviour
{
    public float health;
    private float maxHealth;
    public enemies enemyData;
    private NavMeshAgent agent;


    public void alterHP(float damage)
    {
        health -= damage;
    }

    private void Start()
    {
        maxHealth = enemyData.maxHealth;
        health = maxHealth;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = enemyData.speed;
        
    }

    private void Update()
    {
        agent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
