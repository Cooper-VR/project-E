using UnityEngine;
using System;

public class AugmentManager : MonoBehaviour
{   
    public static int CurrentPoints;
    public static int CurrentThreshHold;
    public static int AugmentsInReserve;

    public static void StartCount()
    { 
        CurrentPoints = 0;
        AugmentsInReserve = 0;
        CurrentThreshHold = 10;
    }

    public static void AddPoints()
    { 
        CurrentPoints += UnityEngine.Random.Range(1, 3);
        if (CurrentPoints >= CurrentThreshHold) 
        {
            AugmentsInReserve += 1;
            CurrentThreshHold = (int)Math.Round(CurrentThreshHold * 1.75);
        }
    }

    
}
