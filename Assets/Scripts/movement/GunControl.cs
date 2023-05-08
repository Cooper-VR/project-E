using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class GunControl : MonoBehaviour
{
	public Animator playerAnimator;

    private bool shooting;
	public GameObject gunSwitcher;
	public Animator movementAnimation;
	public KeyCode ADScode;
	private GunData gunData;

	public PositionConstraint triggerContraint;
	public PositionConstraint muzzleContraint;
	public List<ConstraintSource> triggerSources = new List<ConstraintSource>();
    public List<ConstraintSource> muzzleSources = new List<ConstraintSource>();

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
		playerAnimator.SetBool("canADS", gunData.canADS);
    }

	private void handContraints()
	{
		triggerContraint.SetSources(triggerSources);
		muzzleContraint.SetSources(muzzleSources);
	}
	private void getSources()
	{
		GameObject currentWeapon = gunSwitcher.GetComponent<weaponSwitch>().currentWeapon;
        GameObject sourcesParent = currentWeapon.transform.GetChild(0).gameObject;
    }
}
