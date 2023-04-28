using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
{

	[Header("References")]
	[SerializeField] public GunData gunData;
	[SerializeField] private Transform cam;
	public Transform Muzzle;
	public GameObject Bullet;
	public GameObject particleEffects;

	private UnityEngine.ParticleSystem.MinMaxCurve emmisionAmount;

	public ParticleSystem[] particles;

    float timeSinceLastShot;
    float particleTime = 0f;
	float particleCurrentTime = 0f;

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

	private void OnDisable() => gunData.reloading = false;

	public void StartReload()
	{
		if (!gunData.reloading && this.gameObject.activeSelf)
			StartCoroutine(Reload());
		
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

                OnGunShot();
			}
		}
	}

	private void Update()
	{
		timeSinceLastShot += Time.deltaTime;

		Debug.DrawRay(cam.position, cam.forward * 5);

		if (Input.GetMouseButton(0))
		{
            Shoot();
		}
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

	private void OnDestroy()
	{
		PlayerShoot.shootInput -= Shoot;
		PlayerShoot.reloadInput -= StartReload;
	}

    private void OnGunShot() { }
}