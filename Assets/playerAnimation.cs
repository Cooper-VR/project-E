using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
	public KeyCode[] movement = new KeyCode[4];
	public Animator animator;

	private int forward;
	private int sideways;

	private void Start()
	{
		movement[0] = KeyCode.W;
		movement[1] = KeyCode.S;
		movement[2] = KeyCode.D;
		movement[3] = KeyCode.A;
	}

	private void Update()
	{
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

      

		animator.SetFloat("forward", forward);
        animator.SetFloat("sideWays", sideways);

    }
}
