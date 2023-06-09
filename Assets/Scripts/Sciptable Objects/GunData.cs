using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{

    [Header("Info")]
    public new string name;
    public int speed;
    public int damageAmount;
    public bool canADS;

    [Header("Shooting")]
    public float maxDistance;

    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    [Tooltip("In RPM")] public float fireRate;
    public float reloadTime;
    public float particleCooldown;
    [HideInInspector] public bool reloading;

    [Header("Placement")]
    public float hip;
    public float shootNoADS;

}   