using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GunControl : MonoBehaviour
{
	public bool shooting;
	public GameObject gunSwitcher;
	public Animator movementAnimation;
	public KeyCode ADScode;
	private GunData gunData;
	public Vector3 gunPosition = new Vector3();
	public float height;
	public TwoBoneIKConstraint contraint;


    public Transform triggerTarget;

	private void Update()
	{
		gunData = gunSwitcher.GetComponent<weaponSwitch>().gunData;
		shooting = gunSwitcher.GetComponent<weaponSwitch>().shooting;
		moveGun(new Vector3());

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
			
			contraint.weight = 0;
			moveGun(gunPosition);
        }
    }

	private Vector3 positionCalculation(GunData gundata)
	{
		Vector3 gunPosition = new Vector3();

		return gunPosition;

    }

	private void moveGun(Vector3 position)
	{
		Vector3 head;
		head = movementAnimation.GetBoneTransform(HumanBodyBones.Head).position;
        Vector3 neck;
        neck = movementAnimation.GetBoneTransform(HumanBodyBones.Neck).position;
        Vector3 chest;
        chest = movementAnimation.GetBoneTransform(HumanBodyBones.Chest).position;
        Vector3 spine;
        spine = movementAnimation.GetBoneTransform(HumanBodyBones.Spine).position;
        Vector3 hip;
        hip = movementAnimation.GetBoneTransform(HumanBodyBones.Hips).position;

		float distance =  (head - neck).magnitude + (neck - chest).magnitude + (chest - spine).magnitude + (spine - hip).magnitude + (hip - transform.position).magnitude;
		height = distance;


        float yPostion = distance * gunData.hip;

		position = movementAnimation.GetBoneTransform(HumanBodyBones.RightHand).position;
		

		position.y = yPostion;

        triggerTarget.transform.localPosition = position;

        GameObject weapon = gunSwitcher.GetComponent<weaponSwitch>().currentWeapon;
		weapon.transform.position = position;


    }
	private void moveGun() 
	{
		//default position
	}
}
