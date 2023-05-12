using Fragsurf.Movement;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
	//methods
	methodClasses methods = new methodClasses();

	#region public variables
	public KeyCode[] movement = new KeyCode[4];
	public Animator animator;
	public float transitionMultiplier;
	public Transform playerBody;
	public LayerMask groundLayer;
	#endregion

	#region private variables
	private int forward;
	private int sideways;
	private Vector3 playerTransform;
	private float YTime = 0;
	private float XTime = 0;
	private Vector3 previous;
	private Vector3 velocity;
	public Vector2 Velocity2D;
	private SurfCharacter moveData;
	private Vector3 ground;

	#endregion

	#region start/update
	private void Start()
	{
		moveData = GetComponent<SurfCharacter>();

		playerTransform = playerBody.transform.localPosition;
		movement[0] = KeyCode.W;
		movement[1] = KeyCode.S;
		movement[2] = KeyCode.D;
		movement[3] = KeyCode.A;
	}

	private void Update()
	{
		getVelocity();
		playerBody.transform.localPosition = playerTransform;
		setForwardValues();
		setDidewaysValues();
		setJump();
		setCrouch();
		curveValues();
	}
	#endregion

	#region helper
	private void setForwardValues()
	{
		if (Input.GetKey(movement[0]) && Input.GetKeyDown(movement[1]))
		{
			forward = 0;
		}
		else if (Input.GetKey(movement[0]))
		{
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
	}

	private void setDidewaysValues()
	{
		if (Input.GetKey(movement[2]) && Input.GetKeyDown(movement[3]))
		{
			sideways = 0;
		}
		else if (Input.GetKey(movement[2]))
		{
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
	}

	private void setJump()
	{
		if (velocity.y > 0 && transform.position.y - ground.y > 1.2f)
		{
			animator.SetBool("jump", true);
		}
		else
		{
			animator.SetBool("jump", false);
		}
	}

	private void setCrouch()
	{
		if (moveData.moveData.crouching && Velocity2D.magnitude > 5)
		{
			animator.SetBool("sliding", true);
			animator.SetBool("crouching", true);
		} else if(moveData.moveData.crouching && Velocity2D.magnitude < 3)
        {
			animator.SetBool("crouching", true);
			animator.SetBool("sliding", false);
		}
		else
		{
			animator.SetBool("crouching", false);
			animator.SetBool("sliding", false);
		}
	}

	private void curveValues()
	{
		float xDifference = sideways - animator.GetFloat("sideWays");
		float yDifference = forward - animator.GetFloat("forward");

		XTime += xDifference * Time.deltaTime * transitionMultiplier;
		YTime += yDifference * Time.deltaTime * transitionMultiplier;

		XTime = Mathf.Clamp(XTime, -1, 1);
		YTime = Mathf.Clamp(YTime, -1, 1);

		animator.SetFloat("sideWays", XTime);
		animator.SetFloat("forward", YTime);
	}

	private void getVelocity()
	{
		GameObject groundOBJ = GameObject.FindGameObjectWithTag("terrain");

		if (groundOBJ != null)
		{ 
		ground = methods.checkPosition(transform.position.x, transform.position.z, groundOBJ.GetComponent<Terrain>(), groundLayer);
		velocity = ((transform.position - previous)) / Time.deltaTime;
		Velocity2D.x = velocity.x;
		Velocity2D.y = velocity.z;
		previous = transform.position;

		}


       
	}
    #endregion
}
