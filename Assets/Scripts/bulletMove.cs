using UnityEngine;
using System.Threading;

public class bulletMove : MonoBehaviour
{
	public float startTime;
	public float endTime;
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
		endTime  = Time.time;
	}

    private void OnCollisionEnter(Collision other)
	{
		enemyController controller;
		timeInterval -= 2;
		if (other.gameObject.TryGetComponent<enemyController>(out controller)) 
		{

			controller.alterHP(5);
		}
		else
		{

		}
		
		Destroy(gameObject);
		
	}
}
