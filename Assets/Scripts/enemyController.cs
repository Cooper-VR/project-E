using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyController: MonoBehaviour
{
	public float health;
	private float maxHealth;
	public enemies enemyData;

	public Transform muzzle;
	public GameObject projectile;
	public float projectileSpeed;

	private NavMeshAgent agent;

	private float currentTime = 0;
	private float previousTime = 0;

	public enum enemyTypesEnum
	{
		running,
		flying,
		shooting
	};
	public enemyTypesEnum enemyTypes = enemyTypesEnum.running;

	public void alterHP(float damage)
	{
		health -= damage;
	}

	private void Start()
	{
		maxHealth = enemyData.maxHealth;
		health = maxHealth;
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.speed = enemyData.speed;
		
	}

	private void Update()
	{
		if (enemyTypes == enemyTypesEnum.running)
		{
			zombieSet();
		} else if (enemyTypes == enemyTypesEnum.flying)
		{
			flyerSet();
		} else if (enemyTypes == enemyTypesEnum.shooting)
		{
			sooterSet();
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

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
}
