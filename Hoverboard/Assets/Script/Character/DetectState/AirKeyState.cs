using UnityEngine;
using System.Collections;
/*
 * Hoverboards Airstate
 * This script handles the hoverboard when its not grounded.
 * Mostly changes in input control
 * Created by: Niklas Linder, Erik Åsén
 * Edited by: Niklas Linder, Erik Åsén
 * 
 */
public class AirKeyState : KeyState
{
	private Movement movement;
	private float forwardAcc;
	private float backWardAcc;
	private float timeIni;

	private bool useVCR;
	private InputVCR vcr;

	public AirKeyState(Movement Movement)
	{
		movement = Movement;
	
	}


	
	public override void start()
	{
		timeIni = Time.time;

	}

	public override void update () 
	{

		if(Time.time > timeIni+0.3f)
		{
			movement.Strafe(Input.GetAxisRaw("RightHorizontal")/2);
			//movement.Direction = RotateY(movement.Direction, Input.GetAxisRaw("RightHorizontal")/10);
			movement.rotateBoardInY(Input.GetAxisRaw("LeftHorizontal"));

			movement.rotateBoardInX(Input.GetAxisRaw("LeftVertical"));

		}
	}

	public override void end()
	{
		movement.jumpVelocity = 0;
		movement.setGravity = 0;
	}

	public static Vector3 RotateY( Vector3 v, float angle )
	{
		float sin = Mathf.Sin( angle );
		
		float cos = Mathf.Cos( angle );
		
		float tx = v.x;
		
		float tz = v.z;
		
		v.x = (cos * tx) + (sin * tz);
		
		v.z = (cos * tz) - (sin * tx);
		return v.normalized;
	}

}
