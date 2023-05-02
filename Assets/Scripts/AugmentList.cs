using System.Collections.Generic;
using UnityEngine;

public class AugmentList : MonoBehaviour
{

    public static List<Item> Items = new List<Item>();

    public static void AddItem(Item item)
    {
        Items.Add(item);
    }

    public static void RemoveItem(Item item)
    {
        Items.Remove(item);
    }
    
    
    
    private void Start()
    {
        Items.Add()
    }
    
}

