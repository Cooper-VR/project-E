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

	[Header("Bullet Speed")]
	public int bulletSpeed;



    float timeSinceLastShot;

	private void Start()
	{
		gunData.currentAmmo = 30;
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
		Debug.Log("starting");
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
                GameObject newBullet = GameObject.Instantiate(Bullet, Muzzle.transform.position, Muzzle.transform.rotation);
				Rigidbody rb = newBullet.GetComponent<Rigidbody>();

				Vector3 direction = cam.forward;
				direction /= direction.magnitude;

				direction *= 200;

				


				rb.AddForce(direction * bulletSpeed);

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

		if (Input.GetMouseButton(0))
		{
            Debug.Log(gunData.currentAmmo);
            Shoot();
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			Debug.Log(gunData.reloadTime);
			StartReload();
        }
    }

	private void OnDestroy()
	{
		PlayerShoot.shootInput -= Shoot;
		PlayerShoot.reloadInput -= StartReload;
	}

    private void OnGunShot() { }
}