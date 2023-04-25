using UnityEngine;

public class ParticleTriggerTitleSword : MonoBehaviour 
{
    public ParticleSystem particles;
    public void InitateParticles()
    {
        ParticleSystem.EmissionModule em = particles.emission;
        em.enabled = true;
        particles.Play();
        em.enabled = false;
        Debug.Log("Particles!!");

    }

}
