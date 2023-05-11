using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class explosionDamager : MonoBehaviour
{
    public Terrain terrainCollider;
    public explosionData explosionData;
	public ParticleSystem rootParticle;
	public GameObject[] particles = new GameObject[4];

    private void Start()
    {
        terrainCollider = GameObject.FindGameObjectWithTag("terrain").GetComponent<Terrain>();

		float multiplier = explosionData.radius / 2f;

		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].transform.localScale *= multiplier;

            ParticleSystem.EmissionModule emission = particles[i].GetComponent<ParticleSystem>().emission;

            // Get the current burst settings
            int burstCount = emission.burstCount;
            ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[burstCount];
            emission.GetBursts(bursts);

            // Increase the burst amount for each burst setting
            for (int x = 0; x < burstCount; x++)
            {
                ParticleSystem.Burst burst = bursts[x];
                ParticleSystem.MinMaxCurve newCount = new ParticleSystem.MinMaxCurve(burst.count.constant * multiplier);
                burst.count = newCount;
                bursts[x] = burst;
            }

            // Set the updated burst settings
            emission.SetBursts(bursts);
            //change burst count based on multiplier
        }
    }

    public void onExplosions()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

		GameObject[] allDamagers = enemies.Concat(players).ToArray();


		for (int i = 0; i < allDamagers.Length; i++)
		{
			if (allDamagers[i].tag == "Player" || allDamagers[i].tag == "enemy")
			{
                Vector3 position = checkPosition();

                float distance = (position - allDamagers[i].transform.position).magnitude;

				if (distance < explosionData.radius)
				{
					float newMax = explosionData.closestDamage - explosionData.farthestDamage;

					float damage = ((newMax / explosionData.radius) * distance) + explosionData.farthestDamage;

					playerStats player = new playerStats();
					enemyController enemyData = new enemyController();

					if (allDamagers[i].TryGetComponent<playerStats>(out player))
					{
						Debug.Log(distance);
                        playerStats.health -= (int)damage;
					} else if (allDamagers[i].TryGetComponent<enemyController>(out enemyData))
					{
                        enemyData.alterHP(damage);
					}
				}
			}
		}
        rootParticle.Play();
    }

    private Vector3 checkPosition()
    {
        RaycastHit hit;
        Vector3 position = transform.position;
        Ray ray = new Ray(position, Vector3.up);


        if (Physics.Raycast(position, Vector3.up, out hit, Mathf.Infinity))
        {
            position = hit.point;
        }
        else if (Physics.Raycast(position, Vector3.down, out hit, Mathf.Infinity))
        {
            position = hit.point;
        }
        else
        {
            float height = terrainCollider.SampleHeight(position);

            if (height == 0)
            {
                return new Vector3(0, -1, 0);
            }
            position.y = height;
        }

        return position;
    }
}
