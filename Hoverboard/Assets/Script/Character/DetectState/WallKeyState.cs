using UnityEngine;
using System.Collections;
/* Created by: Niklas Linder, Erik Åsén
 * Description:
 * Hoverboards WallRide-state
 * This script handles the hoverboard when its riding on a wall.
 * Mostly changes in input control aswell as physics control.
 * Edited by: Niklas Linder, Erik Åsén
 * 
 */
public class WallKeyState : KeyState
{

	const float m_wallJumpForce	=	0.5f;


	private Movement movement;
	private float length;
	private Vector3 enterPoint;
	private Vector3 direction;
	private bool jumped;

	public WallKeyState(Movement Movement)
	{
		movement = Movement;
	}

	public override void start ()
	{
		movement.jumpVelocity = 0;
		movement.setGravity = 0;
		movement.isGrounded = true;
		length = new Vector3(movement.m_getVelocity.x, 0, movement.m_getVelocity.z).magnitude;
		enterPoint = movement.transform.position;
		movement.transform.LookAt (10*m_keyVector + movement.transform.position);
		direction = new Vector3 (m_keyVector.x, 0, m_keyVector.z);
		//jumpBehaviour ();

		movement.CustomJumpVec = m_UpjumpVec + m_RightjumpVec;
	}
	
	public override void update () 
	{	
		movement.Direction = direction;

		if((enterPoint-movement.transform.position).magnitude >=length || movement.m_getVelocity.magnitude <= 0)
		{
			movement.changeState("Grounded");
		}

	}
	
	public override void end()
	{
		movement.transform.LookAt (10*m_keyVector + movement.transform.position);
		movement.GetComponent<DetectState> ().m_getRayCastState = true;

	}
}
