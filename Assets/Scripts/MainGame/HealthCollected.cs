using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollected : MonoBehaviour
{
    ParticleSystem deathParticles;

    IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(5);
        Detroyed();
    }


    private void Detroyed()
    {
        Debug.Log(playerStats.health);
        Destroy(gameObject);
        deathParticles = gameObject.GetComponent<ParticleSystem>();
        deathParticles.Play();
    }


    private void Start()
    {
        StartCoroutine(DespawnTimer());
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if ((playerStats.health + 25) > 150)
            {
                playerStats.health = 150;
            }
            else
            {
                playerStats.health += 25;  
            }
            Detroyed();
        }
    }
}
