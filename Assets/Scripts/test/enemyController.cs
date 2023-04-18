using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController: MonoBehaviour
{
    public float health;
    public float speed;
    private float maxHealth;
    public enemies enemyData;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");
        if (other.gameObject.tag == "bullet")
        {

            int damageAmount = other.gameObject.GetComponent<Gun>().gunData.damageAmount;

            health -= damageAmount;
        }
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
            //give player stuff
            Destroy(gameObject);
        }
    }
}
