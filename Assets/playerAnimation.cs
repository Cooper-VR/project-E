using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
	public KeyCode[] movement = new KeyCode[4];
	public Animator animator;
	public float transitionMultiplier;
	public Transform playerBody;

	private int forward;
	private int sideways;
	private Vector3 playerTransform;
    private float YTime = 0;
    private float XTime = 0;

    private void Start()
	{
		playerTransform = playerBody.transform.localPosition;
		movement[0] = KeyCode.W;
		movement[1] = KeyCode.S;
		movement[2] = KeyCode.D;
		movement[3] = KeyCode.A;
	}

	private void Update()
	{
		playerBody.transform.localPosition = playerTransform;

		if (Input.GetKey(movement[0]) && Input.GetKeyDown(movement[1]))
		{
			forward = 0;
		}
		else if (Input.GetKey(movement[0]))
		{
			Debug.Log(forward);
			forward = 1;
		} 
		else if (Input.GetKey(movement[1]))
		{
			forward = -1;
		}
		else
		{
			forward = 0;
		}


		if (Input.GetKey(movement[2]) && Input.GetKeyDown(movement[3]))
		{
			sideways = 0;
		}
		else if (Input.GetKey(movement[2]))
		{
			Debug.Log(sideways);
			sideways = 1;
		}
		else if (Input.GetKey(movement[3]))
		{
			sideways = -1;
		}
		else
		{
			sideways = 0;
		}


		float xDifference = sideways - animator.GetFloat("sideWays");
        float yDifference = forward - animator.GetFloat("forward");

        XTime += xDifference * Time.deltaTime * transitionMultiplier;
		YTime += yDifference * Time.deltaTime * transitionMultiplier;

        XTime = Mathf.Clamp(XTime, -1, 1);
        YTime = Mathf.Clamp(YTime, -1, 1);

        animator.SetFloat("sideWays", XTime);
        animator.SetFloat("forward", YTime);




    }
}
