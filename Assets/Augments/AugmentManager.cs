using UnityEngine;
using System;

public class AugmentManager : MonoBehaviour
{
    public static int CurrentPoints;
    public static int CurrentThreshHold;

    public static void StartCount()
    { 
        CurrentPoints = 0;
        CurrentThreshHold = 10;
    }

    public static void AddPoints()
    { 
        CurrentPoints += UnityEngine.Random.Range(1, 3);
        if (CurrentPoints >= CurrentThreshHold) 
        {
            Debug.Log("Got Augment!");
            CurrentThreshHold = (int)Math.Round(CurrentThreshHold * 1.75);
        }
    }


}
