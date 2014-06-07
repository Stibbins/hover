using UnityEngine;
using System.Collections;

public class Hoverforce : MonoBehaviour {

	[SerializeField] private float forceMultiplier;
	
	[SerializeField] private float forceDistanceMulitplier;
	
	[SerializeField] private float hoverDistance;
	
	[SerializeField] private float distanceForce;
	
	private bool moveBoost;
	
	private float moveBoostAmount;
	
	
	void Start()
	{
		rigidbody.centerOfMass.Set(transform.position.x, transform.position.y - 0.5f, transform.position.z);
	}

	void Update()
	{
		moveBoost = transform.GetComponent<HoverMove>().moving;
		if (moveBoost)
		{
			moveBoostAmount = 1.5f;
		}
		else
		{
			moveBoostAmount = 1;
		}
	}

	void FixedUpdate()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, -transform.up, out hit, hoverDistance))
		{
			distanceForce = forceMultiplier * (-1*(hoverDistance - (hoverDistance/hit.distance)));
			rigidbody.AddForceAtPosition(transform.up*distanceForce*moveBoostAmount,transform.position, ForceMode.VelocityChange);
		}
	}
}
