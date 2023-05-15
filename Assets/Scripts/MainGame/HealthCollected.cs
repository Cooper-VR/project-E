using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollected : MonoBehaviour
{
    ParticleSystem deathParticles;
    MeshRenderer renderer;
    BoxCollider colider;

    IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    IEnumerator ParticleTimer()
    {
        yield return new WaitForSeconds(1.3f);
        Destroy(gameObject);
    }


    private void Detroyed()
    {
        deathParticles.Play();
        colider.enabled = false;
        renderer.enabled = false;
        StartCoroutine(ParticleTimer());
    }


    private void Start()
    {
        deathParticles = gameObject.GetComponent<ParticleSystem>();
        renderer = gameObject.GetComponent<MeshRenderer>();
        colider = gameObject.GetComponent<BoxCollider>();
        StartCoroutine(DespawnTimer());
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {

            playerStats playerHealth;
            playerHealth = col.gameObject.GetComponent<playerStats>();

            if ((playerHealth.health + 25) > 150)
            {
                playerHealth.health = 150;
            }
            else
            {
                playerHealth.health += 25;  
            }
            Detroyed();
        }
    }
}
