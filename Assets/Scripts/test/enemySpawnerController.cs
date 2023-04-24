using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemySpawnerController : MonoBehaviour
{
	public spawner spawnData;
	public LayerMask ground;

	private int timeInterval;
	private float currentTime = 0;
	private float timeOffset;

	private int totalEnemies = 0;
	
	
	void Start()
	{
		getRandomSpawn();
		timeInterval = 60 / spawnData.EnemiesPerMin;

        totalEnemies++;
        GameObject.Instantiate(spawnData.enemyPrefab, getRandomSpawn(), transform.rotation, gameObject.transform);
    }

	// Update is called once per frame
	void Update()
	{
		currentTime += Time.deltaTime;
		
        if (currentTime - timeOffset >= timeInterval && totalEnemies <= spawnData.totalEnemies)
		{
			GameObject.Instantiate(spawnData.enemyPrefab, getRandomSpawn(), transform.rotation, gameObject.transform);
			timeOffset = currentTime;
            totalEnemies++;
            getRandomSpawn();
			
        }

	}

	private Vector3 getRandomSpawn()
	{
		float radius = UnityEngine.Random.value;
		radius *= spawnData.radius;

		Quaternion angle = UnityEngine.Random.rotation;

		float randomYAngle = angle.eulerAngles.y;

		float xSpawn = MathF.Abs(radius * Mathf.Cos(randomYAngle));
		float ySpawn = MathF.Abs(radius * Mathf.Sin(randomYAngle));

		if (randomYAngle > 90 && randomYAngle <= 180)
		{
			xSpawn *= -1;
		} else if (randomYAngle > 180 && randomYAngle <= 270)
		{
			xSpawn *= -1;
			ySpawn *= -1;
		} else if (randomYAngle > 270 && randomYAngle <= 360)
		{
			ySpawn *= -1;
		}

		return checkPosition(xSpawn, ySpawn);
	}

	private Vector3 checkPosition(float x, float y)
	{
		bool gotPoint = false;
		RaycastHit hit;
		Vector3 position = new Vector3(x, 0, y);

		while (!gotPoint)
		{
			
			var raycast = Physics.Raycast(new Vector3( x, 0, y), Vector3.up, out hit, Mathf.Infinity, ground);
			if (Physics.Raycast(new Vector3(x, 0, y), Vector3.up, out hit, Mathf.Infinity, ground)) 
			{
				position = hit.point;
				gotPoint = true;
			} 
			else if (Physics.Raycast(new Vector3(x, 0, y), Vector3.down, out hit, Mathf.Infinity, ground))
			{
				position = hit.point;
				gotPoint = true;
			}
			else
			{
				Debug.Log("Not good");
			}
		}
		
		return position;
	}
}
