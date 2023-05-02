using UnityEngine;

[CreateAssetMenu(fileName = "New Augment", menuName = "Augment/Create New Main Augment")]
public class Item : ScriptableObject
{
    public int id;
    public string augmentName;
    public int value;
    public Sprite Icon;

}
