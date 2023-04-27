using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class explosionDamager : MonoBehaviour
{
	public explosionData explosionData;
	public ParticleSystem rootParticle;
	public GameObject[] particles = new GameObject[4];

    private void Start()
    {
		rootParticle.enableEmission = false;
		float multiplier = explosionData.radius / 2f;

		for (int i = 0; i < particles.Length; i++)
		{
			particles[i].transform.localScale *= multiplier;
			ParticleSystem particleSystem = particles[i].GetComponent<ParticleSystem>();

            var emmition = particleSystem.emission;

			//change burst count based on multiplier
        }
    }

    public void onExplosions()
	{
		Object[] tempList = Resources.FindObjectsOfTypeAll(typeof(GameObject));
		List<GameObject> realList = new List<GameObject>();
		GameObject temp;

		foreach (Object obj in tempList)
		{
			if (obj is GameObject)
			{
				temp = (GameObject)obj;
				if (temp.hideFlags == HideFlags.None)
					realList.Add((GameObject)obj);
			}
		}
		GameObject[] gameObjects = realList.ToArray();

		for (int i = 0; i < gameObjects.Length; i++)
		{
			if (gameObjects[i].tag == "Player" || gameObjects[i].tag == "enemy")
			{
				float distance = (transform.position - gameObjects[i].transform.position).magnitude;

				if (distance < explosionData.radius)
				{
					float newMax = explosionData.closestDamage - explosionData.farthestDamage;

					float damage = ((newMax / explosionData.radius) * distance) + explosionData.farthestDamage;

					playerStats player = new playerStats();
					enemyController enemyData = new enemyController();

					if (gameObjects[i].TryGetComponent<playerStats>(out player))
					{
						Debug.Log(distance);
						player.health -= (int)damage;
					} else if (gameObjects[i].TryGetComponent<enemyController>(out enemyData))
					{
                        Debug.Log(distance);
                        enemyData.alterHP(damage);
					}
				}
			}
		}
		
		rootParticle.enableEmission = true;
		rootParticle.Play();
	}
}
