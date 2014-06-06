using UnityEngine;
using System.Collections;

/*
 *This script decides how the camera should move, depending on the hoverboard's movement  
 * 
 * Created by: Found on Internet, date: sometime ago
 * Source: https://docs.unity3d.com/Documentation/ScriptReference/Mathf.SmoothDampAngle.html
 * 
 * Edited by Andreas Sundberg
 */

public class FELCameraMec : MonoBehaviour {
	
	
	public float m_Smooth = 0.5f;					//How smooth the camera should rotate around the hoverboard
	
	
	public float m_DefaultDistanceZ;				//The distance between camera and the hoverboard in z-axis when hoverboard's speed is 0
	
	
	private float distanceZ;
	public GameObject hoverboard;
	public Movement movement;
	
	private float yOffset = 1f;
	//private Hover_Physics physics;
	private Vector3 targetedPosition;
	
	private float yVelocity = 0.0F;			
	private float xVelocity = 0.0F;
	private float y = 0.0f;
	public bool inAir = false;
	
	private DetectState currentState;	
	
	private float currentYValue = 0;	
	public float distanceY = 1;

	void Start() {
		
	
		
		targetedPosition = hoverboard.transform.position;
		currentYValue = targetedPosition.y;
		
		currentState = hoverboard.GetComponent<DetectState>();
	}
	
	
	void Update() {																		
		
		
		
		//calculating how much the camera should rotate in y- and x-axis relative to the Hoverboard
		float yAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, hoverboard.transform.eulerAngles.y, ref yVelocity, m_Smooth);
		float xAngle = Mathf.SmoothDampAngle(transform.eulerAngles.x, hoverboard.transform.eulerAngles.x, ref xVelocity, m_Smooth);
		
		Vector3 position = hoverboard.transform.position;
		position.y += yOffset;
		
		//these three if-satser decide how the camera's y position should change. the x and z position always follow the hoveboard. 
		//if the hoverboard's position is higher than targetedPosition.y + 1 the camera is moving up

		if(position.y > (targetedPosition.y + 1f))


		{
			
			y = targetedPosition.y;
			targetedPosition = hoverboard.transform.position;
			targetedPosition.y = targetedPosition.y - yOffset;
		}
		// does the same thing but down instead for up.
		else if(position.y < (targetedPosition.y))
		{
			y = targetedPosition.y;
			targetedPosition = hoverboard.transform.position;
			targetedPosition.y = targetedPosition.y +yOffset;
		}
		// else the cameras y position doesnt change
		else
		{
			y = targetedPosition.y;
			
			targetedPosition = hoverboard.transform.position;
			targetedPosition.y = y;

		}


		
		Vector3 lookPos = targetedPosition;
		lookPos.y = targetedPosition.y + distanceY;

		Vector3 newPos = lookPos;
		
		//change distance to hoverboard depending on the hoverboard's speed
		
		if (movement.speedForCamera < -0.01f || movement.speedForCamera > 0.01f )
		{
			distanceZ = m_DefaultDistanceZ + (movement.speedForCamera/20);
			
		}
		else
		{
			distanceZ = m_DefaultDistanceZ;
		}

		
		
		
		/*if(currentState.getKeyState == "Air" || inAir)
		{

		}
		else
		{

		}*/
		

		newPos +=  Quaternion.Euler(xAngle, yAngle, 0) * new Vector3(0, 0, -distanceZ);
		
		
		
		//give camera the position "newPos"
		transform.position = newPos;
		
		
		//hoverboard.transform.up
		transform.LookAt(lookPos, hoverboard.transform.up);
	}
}