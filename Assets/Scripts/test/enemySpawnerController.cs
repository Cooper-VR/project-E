using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemySpawnerController : MonoBehaviour
{
	public spawner spawnData;
	
	
	
	void Start()
	{
		getRandomSpawn();

    }

	// Update is called once per frame
	void Update()
	{
		
	}

	private void getRandomSpawn()
	{
		bool aboveGround = false;

		
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

		Debug.Log(randomYAngle);
		Debug.Log(radius);
		Debug.Log(xSpawn);
		Debug.Log(ySpawn);
	}

	
}
