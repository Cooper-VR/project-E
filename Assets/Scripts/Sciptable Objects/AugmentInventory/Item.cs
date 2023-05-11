using UnityEngine;

[CreateAssetMenu(fileName = "New Augment", menuName = "Augment/Create New Main Augment")]
public class Item : ScriptableObject
{
    public int Id;
    public string AugmentName;
    public int Value;
    public Sprite Icon;
    }
