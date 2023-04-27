using UnityEngine;

[CreateAssetMenu (fileName= "explosions", menuName = "Weapon/explosions")]
public class explosionData : ScriptableObject
{
    public float radius;
    public float closestDamage;
    public float farthestDamage;
}
