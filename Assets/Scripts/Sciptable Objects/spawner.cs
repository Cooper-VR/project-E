using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "enemySpawner", menuName = "enemies/spawner")]
public class spawner : ScriptableObject
{
    [Header("enemyStuff")]
    public GameObject enemyPrefab;
    public int EnemiesPerMin;
    public int totalEnemies;

    [Header("size")]
    public float radius;
}
