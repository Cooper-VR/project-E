using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public int health;
    private float totalTime;
    // Start is called before the first frame update
    void Start()
    {
        health = 150;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("do ui animation for when they die");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            totalTime += Time.deltaTime;

            if (Mathf.RoundToInt(totalTime) % 5 == 0)
            {
                totalTime = 0.51f;
                health -= collision.gameObject.GetComponent<enemyController>().enemyData.damage;
            }
        }
    }
}