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
		timeInterval -= 2;
		if (other.gameObject.name == "enemy") 
		{
			other.gameObject.GetComponent<enemyController>().alterHP(5);
		}
		Destroy(gameObject);
		
	}
}
