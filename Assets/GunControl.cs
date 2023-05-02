using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{
	public bool shooting;
	public GameObject gunSwitcher;
	public Animator movementAnimation;
	public KeyCode ADScode;

	public Vector3 gunPosition;

	private void Update()
	{
		shooting = gunSwitcher.GetComponent<weaponSwitch>().shooting;

		if (Input.GetKeyDown(ADScode) && shooting)
		{
			moveGun(gunPosition);

        }
        else if (shooting)
        {
			moveGun(gunPosition);
        }
        else if (Input.GetKeyDown(ADScode))
        {
			moveGun(gunPosition);
        }
		else
		{
			moveGun();
        }
    }

	private Vector3 positionCalculation(GunData gundata)
	{
		Vector3 gunPosition = new Vector3();

		return gunPosition;

    }

	private void moveGun(Vector3 position)
	{

	}
	private void moveGun() 
	{
		//default position
	}
}
