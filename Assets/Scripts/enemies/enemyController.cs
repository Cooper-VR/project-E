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
	public bool startTimeSet = false;
	public MeshRenderer[] childRenders;

	private NavMeshAgent agent;
    
    private float currentTime = 0;
	private float previousTime = 0;

    private float startTime = 0f;
    private float endTime = 0f;
	private bool exploded = false;

	public explosionData explosion;

    public GameObject ExlotionsPrefab;

	public enum enemyTypesEnum
	{
		running,
		flying,
		shooting,
		creeper
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
		if (!startTimeSet)
		{
			startTime = Time.deltaTime;
			endTime = Time.deltaTime;

        }
		else
		{
			endTime += Time.deltaTime;
		}

		if (enemyTypes == enemyTypesEnum.running)
		{
			zombieSet();
		} else if (enemyTypes == enemyTypesEnum.flying)
		{
			flyerSet();
		} else if (enemyTypes == enemyTypesEnum.shooting)
		{
			sooterSet();
        } else if (enemyTypes == enemyTypesEnum.creeper)
		{
			creeperSet();
		}

        if (health <= 0)
        {
            Destroy(gameObject);
			AugmentManager.AddPoints();
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
	private async void creeperSet()
	{
		agent.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
		if ((transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).magnitude < enemyData.explotionProximity)
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
				component.onExplosions();
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

	IEnumerator delay()
	{
		yield return new WaitForSeconds(1);

		
        Destroy(gameObject);
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
