using UnityEngine;
using System.Collections;

/*
 * Created by: Niklas, Erik
 * Description:
 * This script is a component of Wallrideable objects.
 * On collision it gives the wallstate new vectors for jump physiscs, and a new direction vector.
 * 
 */
public class WallRideTrigger : MonoBehaviour {

	private DetectState detectState;
	private Vector3 direction;
	private bool Wallactive;
	void Start ()
	{

	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Wallactive)
		{
			if(detectState.m_getRailPermission)
			{
				detectState.transform.position += transform.right;
				detectState.updateKeyState ("Wall").setVector = direction;
				detectState.updateKeyState ("Wall").m_RightjumpVec = -transform.right;
				detectState.updateKeyState ("Wall").m_UpjumpVec = transform.up;
				detectState.changeKeyState ("Wall");
				detectState.m_getRailPermission = false;
				detectState.m_getRayCastState = false;
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		Wallactive = true;
		detectState = col.GetComponent<DetectState> ();
		if(Vector3.Angle(transform.right, col.transform.right) <90)
		{
			direction = new Vector3(transform.forward.x, 0, transform.forward.z);
		}
		else
		{
			direction = new Vector3(-transform.forward.x, 1, -transform.forward.z);
		}
	}

	void OnTriggerExit(Collider col)
	{
		detectState.changeKeyState ("Grounded");
		Wallactive = false;
	}
}
