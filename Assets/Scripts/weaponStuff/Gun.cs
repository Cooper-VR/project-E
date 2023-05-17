using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
{
    #region public variables
    [Header("References")]
	public GunData gunData;
	public Transform Muzzle;
	public GameObject Bullet;
	public GameObject particleEffects;
	public Animator player;
	public bool shooting;
	public ParticleSystem[] particles;

    #endregion

    #region private variables
    private float timeSinceLastShot;
	[SerializeField] private Transform cam;
	private UnityEngine.ParticleSystem.MinMaxCurve emmisionAmount;
    private float particleTime = 0f;
    private float particleCurrentTime = 0f;

    #endregion

    #region start/update
    private void Start()
	{
		cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
		gunData.currentAmmo = gunData.magSize;
		PlayerShoot.shootInput += Shoot;
		PlayerShoot.reloadInput += StartReload;

		for(int i = 0;  i < particles.Length; i++)
		{
			var yourParticleEmission = particles[i].emission;

			yourParticleEmission.enabled = true;
			emmisionAmount = yourParticleEmission.rateOverTime;
			yourParticleEmission.rateOverTime = 0f;
			

        }
    }

    private void Update()
    {
        //player.SetBool("reload", gunData.reloading);
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(cam.position, cam.forward * 5);

        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
        else shooting = false;
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartReload();
        }

        particleCurrentTime = Time.time;

        if (particleCurrentTime - particleTime > gunData.particleCooldown)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                var yourParticleEmission = particles[i].emission;

                yourParticleEmission.enabled = true;
                yourParticleEmission.rateOverTime = 0f;
            }
        }
    }
    #endregion

    #region helpers
    private void OnDisable()
	{
		gunData.reloading = false;
		PlayerShoot.shootInput -= Shoot;
		PlayerShoot.reloadInput -= StartReload;
	}

	public void StartReload()
	{
		if (!gunData.reloading && this.gameObject.activeSelf)
		{
			//StartCoroutine(Reload());player.SetBool("reload", false);
		}
	}

	private IEnumerator Reload()
	{
		gunData.reloading = true;

		yield return new WaitForSeconds(gunData.reloadTime);

		gunData.currentAmmo = gunData.magSize;

		gunData.reloading = false;


    }

	private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

	private void Shoot()
	{
		if (gunData.currentAmmo > 0)
		{
			if (CanShoot())
			{

                for (int i = 0; i < particles.Length; i++)
                {
                    var yourParticleEmission = particles[i].emission;

                    yourParticleEmission.enabled = true;
                    yourParticleEmission.rateOverTime = emmisionAmount;
                }

                GameObject newBullet = GameObject.Instantiate(Bullet, Muzzle.transform.position, Muzzle.transform.rotation);
				Rigidbody rb = newBullet.GetComponent<Rigidbody>();
				

				Vector3 direction = cam.forward;
				direction /= direction.magnitude;

				direction *= 200;

				rb.AddForce(direction * gunData.speed);

                gunData.currentAmmo--;
				
				timeSinceLastShot = 0;

				particleTime = Time.time;
				particleCurrentTime = Time.time;
                shooting = true;
                OnGunShot();
			} 
		}
        
    }

	

	private void OnDestroy()
	{
		PlayerShoot.shootInput -= Shoot;
		PlayerShoot.reloadInput -= StartReload;
	}

    private void OnGunShot() { }
    #endregion
}