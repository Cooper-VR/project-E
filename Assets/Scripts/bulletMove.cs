using UnityEngine;
using System.Threading;

public class bulletMove : MonoBehaviour
{
    public float startTime;
    public float endTime;
    private void Update()
    {
        endTime = Time.time;
        if (endTime - startTime > 5)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        startTime = Time.time;
        endTime  = Time.time;
    }
}
