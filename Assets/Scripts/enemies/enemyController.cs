using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController: MonoBehaviour
{
	//methods
	methodClasses methods = new methodClasses();

    #region public variables
    public float health;
	public enemies enemyData;
	public Transform muzzle;
	public GameObject projectile;
	public float projectileSpeed;
	public bool startTimeSet = false;
	public MeshRenderer[] childRenders;
	public explosionData explosion;
    public GameObject ExlotionsPrefab;
    #endregion

    #region private variables
    private float maxHealth;
	private NavMeshAgent agent;
    
    private float currentTime = 0;
	private float previousTime = 0;

    private float startTime = 0f;
    private float endTime = 0f;
	private bool exploded = false;
    #endregion

	/// <summary>
	/// different enemy types
	/// </summary>
    public enum enemyTypesEnum
	{
		running,
		flying,
		shooting,
		creeper
	};

	//sets a default value
	public enemyTypesEnum enemyTypes = enemyTypesEnum.running;

    #region start/update

    private void Start()
	{
		maxHealth = enemyData.maxHealth;
		health = maxHealth;
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.speed = enemyData.speed;
		
	}

	private void Update()
	{
		if (!startTimeSet)
		{
			startTime = Time.deltaTime;
			endTime = Time.deltaTime;

        }
		else
		{
			endTime += Time.deltaTime;
		}

		enemyTypeCheck();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region enemy types
    private void zombieSet()
	{
		agent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
	}

	private void flyerSet()
	{
		if ((transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).magnitude > 15f)
		{
			agent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
	
		}
		else
		{
			Vector3 mainPosition  = transform.position;
			agent.destination = mainPosition;

			currentTime += Time.deltaTime;
			shootProjectile();
		}
	}

    private void sooterSet()
    {
        if ((transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).magnitude > 15f)
        {
            agent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;

        }
        else
        {
            Vector3 mainPosition = transform.position;
            agent.destination = mainPosition;

            currentTime += Time.deltaTime;
            shootProjectile();
        }
    }
	private void creeperSet()
	{
		
		agent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
		if ((transform.position - agent.destination).magnitude < enemyData.explotionProximity)
		{
			startTimeSet = true;
		}

		if (endTime - startTime >= enemyData.explotionDelay)
		{
			if (!exploded)
			{
				Vector3 spawnPosition = transform.position;
				spawnPosition.y += 20;

				GameObject explotion = GameObject.Instantiate(ExlotionsPrefab, spawnPosition, transform.rotation, gameObject.transform);
				explosionDamager component = explotion.GetComponent<explosionDamager>();
				component.explosionData = explosion;
                methods.onExplosions(component.explosionData, component.rootParticle, transform.position, component.terrainCollider);
				exploded = true;
			}
			
			for (int i = 0; i < childRenders.Length; i++)
			{
				childRenders[i].enabled = false;
            }

            MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();
			CapsuleCollider collider = gameObject.GetComponent<CapsuleCollider>();
			
			mesh.enabled = false;
			collider.enabled = false;

			StartCoroutine(delay());
        }

    }
    #endregion

    #region helpers
    /// <summary>
    /// will subtract from enemy health
    /// </summary>
    /// <param name="damage">amount subtracted</param>
    public void alterHP(float damage)
    {
        health -= damage;
    }

    /// <summary>
    /// will check the enemy type and set it
    /// </summary>
    private void enemyTypeCheck()
    {
        if (enemyTypes == enemyTypesEnum.running)
        {
            zombieSet();
        }
        else if (enemyTypes == enemyTypesEnum.flying)
        {
            flyerSet();
        }
        else if (enemyTypes == enemyTypesEnum.shooting)
        {
            sooterSet();
        }
        else if (enemyTypes == enemyTypesEnum.creeper)
        {
            creeperSet();
        }
    }

    /// <summary>
    /// will wait for 2 seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator delay()
	{
		yield return new WaitForSeconds(2);

		
        Destroy(gameObject);
    }

	/// <summary>
	/// shoots projectile
	/// </summary>
    private void shootProjectile()
	{

		if (currentTime - previousTime >= (60 / enemyData.fireRate))
		{
			GameObject bullet = GameObject.Instantiate(projectile, muzzle.position, muzzle.rotation);
			Vector3 positionDifference = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;

			positionDifference = positionDifference.normalized;

			Rigidbody rb = bullet.GetComponent<Rigidbody>();

			previousTime = currentTime;

			Vector3 direction = transform.forward;
			direction /= direction.magnitude;

			direction *= 200;

			rb.AddForce(positionDifference * projectileSpeed);
		}

		
	}
    #endregion
}
