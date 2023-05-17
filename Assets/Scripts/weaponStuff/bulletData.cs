using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletData : MonoBehaviour
{
	public int publicDamge;

	public float startTime;
	public float endTime;
	public bool rocket;
	public GameObject ExlotionsPrefab;
	public explosionData explosion;
	private int timeInterval = 5;

	private void Update()
	{
		endTime = Time.time;
		if (endTime - startTime > timeInterval)
		{
			Destroy(gameObject);
		}
	}
	private void Start()
	{
		startTime = Time.time;
		endTime = Time.time;
	}

	private void OnCollisionEnter(Collision other)
	{
		methodClasses methods = new methodClasses();
		playerStats controller;
		timeInterval -= 2;
		if (other.gameObject.TryGetComponent<playerStats>(out controller))
		{
			if (rocket)
			{
				Vector3 spawnPosition = transform.position;
				spawnPosition.y += 20;
	
				GameObject explotion = GameObject.Instantiate(ExlotionsPrefab, spawnPosition, transform.rotation);
				explosionDamager component = explotion.GetComponent<explosionDamager>();
				component.explosionData = explosion;
				methods.onExplosions(component.explosionData, component.rootParticle, spawnPosition, component.terrainCollider);
			}
			else
			{
				controller.health -=5;
			}

			Destroy(gameObject);
		}
		else
		{

		}
	}
}
