using System;
using UnityEngine;

public class enemySpawnerController : MonoBehaviour
{
	methodClasses methods = new methodClasses();

	public spawner spawnData;
	public LayerMask ground;
	public Terrain terrainCollider;

	#region private variables
	private int timeInterval;
	private float currentTime = 0;
	private float timeOffset;
	private int totalEnemies = 0;
	#endregion


	void Start()
	{
        methods.getRandomSpawn(spawnData, transform.position, terrainCollider, ground);
		timeInterval = 60 / spawnData.EnemiesPerMin;

		totalEnemies++;
		GameObject.Instantiate(spawnData.enemyPrefab, methods.getRandomSpawn(spawnData, transform.position, terrainCollider, ground), transform.rotation, gameObject.transform);
	}

	// Update is called once per frame
	void Update()
	{
		currentTime += Time.deltaTime;
		
		if (currentTime - timeOffset >= timeInterval && totalEnemies <= spawnData.totalEnemies)
		{
			Vector3 spawn = methods.getRandomSpawn(spawnData, transform.position, terrainCollider, ground);

			if (spawn != new Vector3(0, -1 ,0)) 
			{
				GameObject.Instantiate(spawnData.enemyPrefab, spawn, transform.rotation, gameObject.transform);
				timeOffset = currentTime;
				totalEnemies++;
            }
		}

	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, spawnData.radius);
	}
}
