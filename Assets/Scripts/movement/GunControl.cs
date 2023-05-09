using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

	public ParentConstraint triggerContraint;
	public ParentConstraint muzzleContraint;
	public ConstraintSource triggerSource = new ConstraintSource();
    public ConstraintSource muzzleSource = new ConstraintSource();
	public ConstraintSource muzzleSource2 = new ConstraintSource();

	private List<ConstraintSource> muzzleSouces = new List<ConstraintSource>();

	private string[] contraintNames = { "muzzle", "trigger", "Mag" };

    private void Start()
    {
		
    }

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

	public void SetSources()
	{
		
		GameObject currentWeapon = gunSwitcher.GetComponent<weaponSwitch>().currentWeapon;
        GameObject sourcesParent = currentWeapon.transform.GetChild(0).gameObject;

		

		int childCount = sourcesParent.transform.childCount;

		muzzleSouces.Clear();
		


        for (int i = 0; i < childCount; i++)
		{
			
			string name = sourcesParent.transform.GetChild(i).name;

			if (name == contraintNames[0])
			{
				
				muzzleSource.sourceTransform = sourcesParent.transform.GetChild(i).GetChild(0);
			} else if (name == contraintNames[1])
			{
				triggerSource.sourceTransform = sourcesParent.transform.GetChild(i).GetChild(0);
			} else if (name == contraintNames[2])
			{
				muzzleSource2.sourceTransform = sourcesParent.transform.GetChild(i).GetChild(0);
			}
		}

		triggerSource.weight = 1;
		muzzleSouces.Add(muzzleSource);
		muzzleSouces.Add(muzzleSource2);

		float[] muzzleWeights = new float[2];
		muzzleWeights[0] = muzzleContraint.GetSource(0).weight;
        muzzleWeights[1] = muzzleContraint.GetSource(1).weight;

        triggerContraint.SetSource(0, triggerSource);
		muzzleContraint.RemoveSource(0);
        muzzleContraint.RemoveSource(0);

		muzzleSource.weight = muzzleWeights[0];
		muzzleSource2.weight = muzzleWeights[1];

		muzzleContraint.AddSource(muzzleSource);
        muzzleContraint.AddSource(muzzleSource2);
    }
}
