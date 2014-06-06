using UnityEngine;
using System.Collections;

/*
 * Created by: Niklas, Erik
 * Description:
 * This script is a component of grindable objects.
 * On collision it gives the railstate a new direction vector.
 * When grindKey triggers, it transforms the hoverboards position aswell as its rotation.
 * 
 */

public class Grindable : MonoBehaviour {

	private DetectState detectState;
	private bool Grindactive;
	private GameObject player;
	private Vector3 push;
	private float pushLength;
	private float pullLength;
	private Vector3 grindBounds;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Grindactive)
		{
			if(detectState.m_getRailPermission && GlobalFuncVari.getNum() > 0)
			{
				player.transform.eulerAngles = push;
				detectState.m_getRayCastState = false;
				detectState.changeKeyState("Rail");
				detectState.m_getRailPermission = false;
			}
		}
	}

	void OnTriggerEnter(Collider col)
	{
		player = col.gameObject;
		detectState = player.gameObject.GetComponent<DetectState> ();
		
		if(player.transform.localScale.x != 2)
		{
			GlobalFuncVari.setRailBounds(col.transform.localScale);
			player.transform.localScale = new Vector3 (2, player.transform.localScale.y, 2) ;
		}

		if(GlobalFuncVari.getNum() == 0)
		{
			if(Vector3.Angle(transform.forward, player.transform.right) <= 90)
			{
				GlobalFuncVari.railFalse();
				detectState.updateKeyState ("Rail").setVector = -transform.right;
			}
			else
			{
				GlobalFuncVari.railTrue();
				detectState.updateKeyState ("Rail").setVector = transform.right;
			}
		}
		else if(GlobalFuncVari.getallowRail())
		{
			detectState.m_getRayCastState = false;
			detectState.changeKeyState("Rail");
			detectState.m_getRailPermission = false;

			if(GlobalFuncVari.getRailbool())
			{
				push = new Vector3(-transform.eulerAngles.z, transform.eulerAngles.y+90, player.transform.eulerAngles.z);
				player.transform.position = transform.position + (player.transform.position-transform.position).magnitude*-transform.right;
				player.transform.eulerAngles = new Vector3(-transform.eulerAngles.z, transform.eulerAngles.y+90, player.transform.eulerAngles.z);
				detectState.updateKeyState ("Rail").setVector = transform.right;
			}
			else
			{
				push = new Vector3(transform.eulerAngles.z, transform.eulerAngles.y-90, player.transform.eulerAngles.z);
				player.transform.position = transform.position + (player.transform.position-transform.position).magnitude*transform.right;
				player.transform.eulerAngles = new Vector3(transform.eulerAngles.z, transform.eulerAngles.y-90, player.transform.eulerAngles.z);
				detectState.updateKeyState ("Rail").setVector = -transform.right;
			}
		}
		Grindactive = true;
		GlobalFuncVari.incNum();
		
		if(detectState.m_getRailPermission)
		{	
			detectState.m_getRayCastState = false;
			detectState.changeKeyState("Rail");
			detectState.m_getRailPermission = false;
			GlobalFuncVari.allowRailTrue();

			if(GlobalFuncVari.getRailbool())
			{
				player.transform.position = transform.position + (player.transform.position-transform.position).magnitude*-transform.right;
				player.transform.eulerAngles = new Vector3(-transform.eulerAngles.z, transform.eulerAngles.y+90, player.transform.eulerAngles.z);
				push = new Vector3(-transform.eulerAngles.z, transform.eulerAngles.y+90, player.transform.eulerAngles.z);
			}
			else
			{
				player.transform.position = transform.position + (player.transform.position-transform.position).magnitude*transform.right;
				player.transform.eulerAngles = new Vector3(transform.eulerAngles.z, transform.eulerAngles.y-90, player.transform.eulerAngles.z);
				push = new Vector3(transform.eulerAngles.z, transform.eulerAngles.y-90, player.transform.eulerAngles.z);
			}
		}
	}

	void OnTriggerExit(Collider col)
	{

		GlobalFuncVari.decNum();
		if(GlobalFuncVari.getNum() <= 0)
		{
			col.transform.localScale = GlobalFuncVari.getRailBounds();
			GlobalFuncVari.allowRailFalse();
			Grindactive = false;
			col.gameObject.GetComponent<DetectState>().m_getRayCastState = true;
		}
	}
}
