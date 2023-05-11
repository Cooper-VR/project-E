using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEditor;

public static class AugmentManager
{   
    public static int CurrentPoints;
    public static int CurrentThreshHold;
    public static int AugmentsInReserve;
    public static ScriptableObject[] AllAugments;
    public static ScriptableObject[] SelectedAugments;
    public static List<ScriptableObject[]> Augments;

    static int index;

    public static void StartCount()
    {
        index = 0;
        foreach (var item in AllAugments)
        {
            index++;
        }
        CurrentPoints = 0;
        AugmentsInReserve = 0;
        CurrentThreshHold = 10;
        Augments.Clear();
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

    public static void CreateAugments()
    {
        SelectedAugments = new ScriptableObject[] { AllAugments[UnityEngine.Random.Range(0, index)], AllAugments[UnityEngine.Random.Range(0, index)], AllAugments[UnityEngine.Random.Range(0, index)] };
        Augments.Add(SelectedAugments); 
    }
    
}
