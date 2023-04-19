using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "enemies", menuName = "enemies/enemy")]
public class enemies : ScriptableObject
{
    [Header("Info")]
    public new string name;
    public int speed;
    public float maxHealth;

    public int damage;
}   