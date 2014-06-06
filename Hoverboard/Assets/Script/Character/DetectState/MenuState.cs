using UnityEngine;
using System.Collections;
/*
 * Created by: Niklas Linder
 * Description:
 * The games Menu state (in game menu)
 * This script starts a idle state for the game, to let the menu do its thing.
 * Edited by: Niklas Linder
 */
public class MenuState : KeyState
{
	private Movement movement;
	
	public MenuState(Movement Movement)
	{
		movement = Movement;
	}
	
	public override void start ()
	{
		Debug.Log ("menu_begin");
		movement.gameObject.GetComponent<Hover_WithTransform> ().enabled = false;
		movement.GetComponent<DetectState>().m_getRayCastState = false;
	}
	
	public override void update () 
	{		

		
	}
	
	public override void end()
	{
		Debug.Log ("menu_slut");
		movement.gameObject.GetComponent<Hover_WithTransform> ().enabled = true;
		movement.GetComponent<DetectState>().m_getRayCastState = true;
		
	}
	
}