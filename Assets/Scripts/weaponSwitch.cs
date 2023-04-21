using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSwitch : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Transform[] Weapons;

	[Header("Keys")]
	[SerializeField] private KeyCode[] Keys;

	[Header("Settings")]
	[SerializeField] private float switchtime;

	private int selectedWeapon;
	private float timeSinceSwitch;


    private void Start()
    {
		SetWeapon();
		Select(selectedWeapon);
		timeSinceSwitch = 0;
    }

    private void Update()
    {
		int previousSelection = selectedWeapon;

		for (int i = 0; i < Keys.Length; i++)
		{
			if (Input.GetKeyDown(Keys[i]) && timeSinceSwitch >= switchtime)
			{
				selectedWeapon = i;
			}
		}

		if (previousSelection != selectedWeapon)
		{
			Select(selectedWeapon);
		}

		timeSinceSwitch += Time.deltaTime;
    }

    private void SetWeapon()
	{
		Weapons = new Transform[transform.childCount];

		for (int i = 0; i < transform.childCount; i++) 
		{
			Weapons[i] = transform.GetChild(i);
		}

		if (Keys == null)
		{
			Keys = new KeyCode[Weapons.Length];
		}
	}

    private void Select(int weaponIndex)
    {
		for (int i = 0; i < Weapons.Length; i++)
		{
			Weapons[i].gameObject.SetActive(i == weaponIndex);
		}

		timeSinceSwitch = 0;

		OnWeaponSelected();
    }

	private void OnWeaponSelected()
	{
		
	}
}
