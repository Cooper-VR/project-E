using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class playerData
{
    public int level;
    public int health;
    public float[] position = new float[3];
    public float[] rotation = new float[4];

    public playerData(playerStats playerData)
    {
        level = playerData.level;

        health = GameObject.FindGameObjectWithTag("Player").GetComponent<playerStats>().health;

        position[0] = playerData.transform.position.x;
        position[1] = playerData.transform.position.y;
        position[2] = playerData.transform.position.z;

        rotation[0] = playerData.transform.rotation.w;
        rotation[1] = playerData.transform.rotation.x;
        rotation[2] = playerData.transform.rotation.y;
        rotation[3] = playerData.transform.rotation.z;
    }
}
