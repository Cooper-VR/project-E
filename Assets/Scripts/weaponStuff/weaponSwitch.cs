using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSwitch : MonoBehaviour
{
    #region private variables
    [Header("References")]
	[SerializeField] private Transform[] Weapons;

	[Header("Keys")]
	[SerializeField] private KeyCode[] Keys;

	[Header("Settings")]
	[SerializeField] private float switchtime;
	private int selectedWeapon;
	private float timeSinceSwitch;

    #endregion

    #region public variables
    public bool shooting;
	public GameObject currentWeapon;
	public GunData gunData;
	public Gun gunScripts;
    public GunControl SetSources;

    #endregion

    #region start/update

    private void Start()
    {
		SetWeapon();
		Select(selectedWeapon);
		timeSinceSwitch = 0;
		currentWeapon = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
		if (!currentWeapon.gameObject.TryGetComponent<Gun>(out gunScripts))
		{
			gunScripts = currentWeapon.transform.GetChild(0).GetComponent<Gun>();
			gunData = currentWeapon.transform.GetChild(0).gameObject.GetComponent<Gun>().gunData;
		}
		shooting = gunScripts.shooting;
		gunData = gunScripts.gunData;


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
			SetSources.SetSources();

        }

		timeSinceSwitch += Time.deltaTime;
    }

    #endregion

    #region methods
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
			if (i == weaponIndex)
			{
                currentWeapon = Weapons[i].gameObject;
            }

        }

		timeSinceSwitch = 0;

		OnWeaponSelected();
    }

	private void OnWeaponSelected()
	{
		
	}
    #endregion
}
