using UnityEngine;
using System.Collections;
/* Created by: Niklas Linder, Erik Åsén
 * Description:
 * Hoverboards Groundstate
 * This script handles the hoverboard when not grounded.
 * Mostly changes in input control aswell as adds velocity
 * Edited by: Niklas Linder, Erik Åsén
 * 
 */
public class MoveKeyState : KeyState
{
	private Movement movement;
	private float forwardAcc;
	private float backWardAcc;

	public MoveKeyState(Movement Movement)
	{
		movement = Movement;
	}

	public override void start ()
	{

	}

	// Update is called once per frame
	public override void update () 
	{
		movement.forwardSpeed += movement.m_ForwardAcc * Input.GetAxisRaw("Triggers");
		movement.backwardSpeed += movement.m_ForwardAcc * Input.GetAxisRaw("Triggers");

		if (movement.Direction != movement.transform.forward)
		{
			movement.Direction = Vector3.Slerp (movement.Direction, movement.transform.forward, Time.deltaTime * 5f);
		}
		else
		{
			movement.Direction = movement.transform.forward;
		}

		movement.Direction = movement.transform.forward;
		movement.rotateBoardInY(Input.GetAxisRaw("LeftHorizontal"));
		movement.Strafe(Input.GetAxisRaw("RightHorizontal"));
	}

	public override void end()
	{

	}
}
