using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public static int health;
    public int level;
    private float totalTime;
    // Start is called before the first frame update
    void Start()
    {
        health = 150;
        StartCoroutine(HealthDetuction());
    }

    IEnumerator HealthDetuction()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);  
            health -= 1;
            if (health < 0)
            {
                health = 0;
            }
        }
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
        else if (collision.gameObject.tag == "enemyBullet")
        {
            Debug.Log(collision.gameObject.tag);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "enemyBullet")
        {
            health -= collision.gameObject.GetComponent<bulletData>().publicDamge;
        }
    }

    /// <summary>
    /// call this to load the data in this script, data saved is defined in the saveData script
    /// </summary>
    public void savePlayer()
    {
        saveSystem.saveData(this);
    }
    /// <summary>
    /// call this to load the player data and apply it
    /// </summary>
    public void loadPlayer()
    {
        playerData data = saveSystem.loadPlayer();

        level = data.level;
        health = data.health;

        Vector3 position = new Vector3();

        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;

        Quaternion rotation = new Quaternion();

        rotation.w = data.rotation[0];
        rotation.x = data.rotation[1];
        rotation.y = data.rotation[2];
        rotation.z = data.rotation[3];
    }
}
