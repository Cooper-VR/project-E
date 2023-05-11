using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollected : MonoBehaviour
{
    [SerializeField] playerStats stats;
    [SerializeField] ParticleSystem deathParticles;

    IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(5);
        Detroyed();
    }


    private void Detroyed()
    {
        Debug.Log(stats.health);
        Destroy(gameObject);
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
            Debug.Log(stats.health);
            if ((stats.health + 45) > 150)
            {
                stats.health = 150;
            }
            else
            {
                stats.health += 45;  
            }
            Detroyed();
        }
    }
}
