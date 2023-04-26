using System;
using UnityEngine;

public class enemySpawnerController : MonoBehaviour
{
	public spawner spawnData;
	public LayerMask ground;

    #region private variables
    private int timeInterval;
	private float currentTime = 0;
	private float timeOffset;
	private int totalEnemies = 0;
    #endregion


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

	/// <summary>
	/// will calculate a random point in the circle with the defined radius
	/// </summary>
	/// <returns>a spawn point in the circle defined by the radius</returns>
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

		xSpawn += transform.position.x;
		ySpawn += transform.position.z;

		return checkPosition(xSpawn, ySpawn);
	}

	/// <summary>
	/// will use raycasting to check if the spawn point if valid, if not it will look up and down for a valid point
	/// </summary>
	/// <param name="x">the x value given by the random spawn function</param>
	/// <param name="y">the y value given by the random spawn function</param>
	/// <returns>a new valid point for the spawn</returns>
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

    private void OnDrawGizmos()
    {
		Gizmos.DrawWireSphere(transform.position, spawnData.radius);
    }
}
