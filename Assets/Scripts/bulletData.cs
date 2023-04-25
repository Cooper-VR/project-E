using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletData : MonoBehaviour
{
    public int publicDamge;

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
        endTime = Time.time;
    }

    private void OnCollisionEnter(Collision other)
    {
        timeInterval -= 2;

        Destroy(gameObject);

    }
}
