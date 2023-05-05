using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class GunControl : MonoBehaviour
{
	public Animator playerAnimator;

	public bool shooting;
	public GameObject gunSwitcher;
	public Animator movementAnimation;
	public KeyCode ADScode;
	private GunData gunData;
	public float height;

	private void Update()
	{
		gunData = gunSwitcher.GetComponent<weaponSwitch>().gunData;
		shooting = gunSwitcher.GetComponent<weaponSwitch>().shooting;

		
        if (Input.GetKey(ADScode))
		{
			moveGun(1);
		}
        else if (shooting)
        {
            moveGun(2);
		} 
		else
		{
			moveGun(0);
		}
    }

	private void moveGun(int value)
	{
		playerAnimator.SetInteger("gunPosition", value);
    }
}
