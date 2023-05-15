using System;
using UnityEngine;

public class explosionDamager : MonoBehaviour
{
    #region public variables
    public Terrain terrainCollider;
    public explosionData explosionData;
	public ParticleSystem rootParticle;
	public GameObject[] particles = new GameObject[4];
    #endregion

    private void Start()
    {
        try
        {
            terrainCollider = GameObject.FindGameObjectWithTag("terrain").GetComponent<Terrain>();

            float multiplier = explosionData.radius / 2f;

            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].transform.localScale *= multiplier;

                ParticleSystem.EmissionModule emission = particles[i].GetComponent<ParticleSystem>().emission;

                // Get the current burst settings
                int burstCount = emission.burstCount;
                ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[burstCount];
                emission.GetBursts(bursts);

                // Increase the burst amount for each burst setting
                for (int x = 0; x < burstCount; x++)
                {
                    ParticleSystem.Burst burst = bursts[x];
                    ParticleSystem.MinMaxCurve newCount = new ParticleSystem.MinMaxCurve(burst.count.constant * multiplier);
                    burst.count = newCount;
                    bursts[x] = burst;
                }

                // Set the updated burst settings
                emission.SetBursts(bursts);
                //change burst count based on multiplier
            }
        }
        catch(NullReferenceException e)
        {
            Debug.Log("no terrain found" + " " + e.Message);
        } 
        
        finally
        {
            float multiplier = explosionData.radius / 2f;

            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].transform.localScale *= multiplier;

                ParticleSystem.EmissionModule emission = particles[i].GetComponent<ParticleSystem>().emission;

                // Get the current burst settings
                int burstCount = emission.burstCount;
                ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[burstCount];
                emission.GetBursts(bursts);

                // Increase the burst amount for each burst setting
                for (int x = 0; x < burstCount; x++)
                {
                    ParticleSystem.Burst burst = bursts[x];
                    ParticleSystem.MinMaxCurve newCount = new ParticleSystem.MinMaxCurve(burst.count.constant * multiplier);
                    burst.count = newCount;
                    bursts[x] = burst;
                }

                // Set the updated burst settings
                emission.SetBursts(bursts);
                //change burst count based on multiplier
            }
        }
        

		
    }
 
}
