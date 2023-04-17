using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Gun : MonoBehaviour
{

	[Header("References")]
	[SerializeField] private GunData gunData;
	[SerializeField] private Transform cam;
	public Transform Muzzle;
	public GameObject Bullet;
	public int bulletSpeed;

	float timeSinceLastShot;

	private void Start()
	{
		PlayerShoot.shootInput += Shoot;
		PlayerShoot.reloadInput += StartReload;
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
		gunData.currentAmmo = 5;
		if (gunData.currentAmmo > 0)
		{
			if (CanShoot())
			{
				var direction = cam.forward;

				direction /= direction.magnitude;

				Debug.Log(direction);

				GameObject newBullet = GameObject.Instantiate(Bullet, Muzzle.transform.position, Muzzle.rotation);

				//this should call en event to move the instatated bullet
				//newBullet.transform.position += direction * bulletSpeed * Time.deltaTime;
					

				gunData.currentAmmo--;
				
				timeSinceLastShot = 0;
				OnGunShot();
			}
		}
	}

	private void Update()
	{
		timeSinceLastShot += Time.deltaTime;

		Debug.DrawRay(cam.position, cam.forward * 5);

		if (Input.GetAxis("Fire1") == 1)
		{
			Shoot();
		}
	}

	private void OnDestroy()
	{
		PlayerShoot.shootInput -= Shoot;
		PlayerShoot.reloadInput -= StartReload;
	}

	private void OnGunShot() { }
}