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


    float timeSinceLastShot;
    float particleTime = 0f;
	float particleCurrentTime = 0f;

    private void Start()
	{
		Debug.Log(gunData.fireRate);

		gunData.currentAmmo = 30;
		PlayerShoot.shootInput += Shoot;
		PlayerShoot.reloadInput += StartReload;
        particleEffects.SetActive(false);
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
				particleEffects.SetActive(true);
				ParticleSystem singleParticle = particleEffects.transform.GetChild(0).GetComponent<ParticleSystem>();


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

		if (particleCurrentTime - particleTime > 0.15f)
		{
			particleEffects.SetActive(false);
		}
    }

	private void OnDestroy()
	{
		PlayerShoot.shootInput -= Shoot;
		PlayerShoot.reloadInput -= StartReload;
	}

    private void OnGunShot() { }
}