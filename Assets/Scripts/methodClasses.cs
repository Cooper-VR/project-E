using System;
using System.Linq;
using UnityEngine;

public class methodClasses
{
    /// <summary>
    /// will check the postition of at given point
    /// </summary>
    /// <param name="Position">the current poition</param>
    /// <param name="terrainCollider">the terrain</param>
    /// <returns></returns>
    private Vector3 checkPosition(Vector3 Position, Terrain terrainCollider)
    {
        RaycastHit hit;
        Vector3 position = Position;
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

    /// <summary>
	/// will calculate a random point in the circle with the defined radius
	/// </summary>
	/// <returns>a spawn point in the circle defined by the radius</returns>
	public Vector3 getRandomSpawn(spawner spawnData, Vector3 position, Terrain terrainCollider, LayerMask groundLayer)
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
        }
        else if (randomYAngle > 180 && randomYAngle <= 270)
        {
            xSpawn *= -1;
            ySpawn *= -1;
        }
        else if (randomYAngle > 270 && randomYAngle <= 360)
        {
            ySpawn *= -1;
        }

        xSpawn += position.x;
        ySpawn += position.z;


        Vector3 newPosition = checkPosition(xSpawn, ySpawn, terrainCollider, groundLayer);

        if (newPosition == new Vector3(0, -1, 0))
        {
            return new Vector3(0, -1, 0);
        }
        else
        {
            return newPosition;
        }
    }

    /// <summary>
    /// will use raycasting to check if the spawn point if valid, if not it will look up and down for a valid point
    /// </summary>
    /// <param name="x">the x value given by the random spawn function</param>
    /// <param name="y">the y value given by the random spawn function</param>
    /// <returns>a new valid point for the spawn</returns>
    public Vector3 checkPosition(float x, float y, Terrain terrainCollider, LayerMask ground)
    {
        RaycastHit hit;
        Vector3 position = new Vector3(x, 0, y);
        Ray ray = new Ray(position, Vector3.up);


        if (Physics.Raycast(new Vector3(x, 0, y), Vector3.up, out hit, Mathf.Infinity, ground))
        {
            position = hit.point;
        }
        else if (Physics.Raycast(new Vector3(x, 0, y), Vector3.down, out hit, Mathf.Infinity))
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

    /// <summary>
    /// will set values in the animator
    /// </summary>
    /// <param name="value">is the integer value to set for the position of the gun</param>
    /// <param name="playerAnimator">the animator that will move the gun</param>
    /// <param name="gunData">the data of the gun currently out</param>
    public void moveGun(int value, Animator playerAnimator, GunData gunData)
    {
        playerAnimator.SetInteger("gunPosition", value);
        playerAnimator.SetBool("canADS", gunData.canADS);
    }

    /// <summary>
    /// will create an explaotion that will affect everything not just player
    /// </summary>
    /// <param name="explosionData">the explaotion data to be used, like size and damge</param>
    /// <param name="rootParticle">the root particle of the explotion</param>
    /// <param name="Position">the position the explotion is happening at</param>
    /// <param name="terrainCollider">the terrain</param>
    public void onExplosions(explosionData explosionData, ParticleSystem rootParticle, Vector3 Position, Terrain terrainCollider)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        GameObject[] allDamagers = enemies.Concat(players).ToArray();


        for (int i = 0; i < allDamagers.Length; i++)
        {
            if (allDamagers[i].tag == "Player" || allDamagers[i].tag == "enemy")
            {
                Vector3 position = checkPosition(Position, terrainCollider);
                float distance = (position - allDamagers[i].transform.position).magnitude;

                if (distance < explosionData.radius)
                {
                    float newMax = explosionData.closestDamage - explosionData.farthestDamage;

                    float damage = ((newMax / explosionData.radius) * distance) + explosionData.farthestDamage;

                    playerStats player;
                    enemyController enemyData;
                    Debug.Log(distance);
                    if (allDamagers[i].TryGetComponent<playerStats>(out player))
                    {
                        player.health -= (int)damage;
                    }
                    else if (allDamagers[i].TryGetComponent<enemyController>(out enemyData))
                    {
                        enemyData.alterHP(damage);
                    }
                }
            }
        }
        rootParticle.Play();
    }


    
}
