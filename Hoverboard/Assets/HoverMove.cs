using UnityEngine;
using System.Collections;

public class HoverMove : MonoBehaviour {

	public bool moving;
	
	[SerializeField] private float sideMultiplier;
	[SerializeField] private float forwardMultiplier;
	
	private float zRotation; //Strafe
	private float xRotation; //Forward & Backward
	
	private Quaternion rotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		zRotation = Input.GetAxisRaw("RightHorizontal") * sideMultiplier; 
		xRotation = Input.GetAxisRaw("Triggers") * forwardMultiplier;
		if (zRotation != 0 || xRotation != 0)
		{
			moving = true;
		}
		else
		{
			moving = false;
		}
	}
	
	void FixedUpdate()
	{
		rotation = Quaternion.Euler(new Vector3 (xRotation, 0, zRotation));
		
		
		rigidbody.MoveRotation(rotation);
	}
	
	
}
