using UnityEngine;
using System.Collections;
/*
	This script rayscast in front of the hoverboard to Calculate incoming collisions
 *
 * Created by: Niklas , 2014-04-29
 * Modified by: Robbin, 2014-05-13
 */
public class CalcCollision : MonoBehaviour {

	private Movement movement;
	private Vector3 direction;
	void Start () 
	{
		movement = GetComponent<Movement> ();
	}
	

	void FixedUpdate () 
	{
        direction = movement.m_getVelocity;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction.normalized, out hit, 0.1f + direction.magnitude * Time.fixedDeltaTime))
        {
            movement.m_characterAnimator.SetBool("Collided", true);
            movement.ResetPosition(transform.position - direction.normalized/* * speed*/);
        }
        else// if (animation["Collided"].enabled)
        {
            movement.m_characterAnimator.SetBool("Collided", false);
        }

		/*direction = movement.m_getVelocity;
		RaycastHit hit;
        float speed = movement.forwardSpeed + movement.backwardSpeed;
        float hoverboardLengthFromOrigin;

        hoverboardLengthFromOrigin = transform.localScale.x / 2.0f; //Check sides
        if (Physics.Raycast(transform.position, Vector3.Cross(direction, Vector3.up).normalized, out hit, Mathf.Abs(movement.m_strafeSpeed) + hoverboardLengthFromOrigin))
        {
            HandleCollision(movement.m_strafeSpeed, transform.forward * hoverboardLengthFromOrigin, hit);
        }
        Debug.DrawRay(transform.position, direction.normalized, Color.white);
        hoverboardLengthFromOrigin = transform.localScale.z / 2.0f; //Check forward and backward
        if (Physics.Raycast(transform.position, direction.normalized, out hit, 0.1f + direction.magnitude * Time.fixedDeltaTime + hoverboardLengthFromOrigin))
        {
            HandleCollision(speed, transform.forward * hoverboardLengthFromOrigin, hit);
        }*/
	}

    void HandleCollision(float speed, Vector3 offset, RaycastHit hit)
    {
        if (speed > 0) 
        {
            movement.ResetPosition(hit.point - offset); //Set the position to the ray's end point, minus half the length of the hoverboard
        }
        else
        {
            movement.ResetPosition(hit.point + offset); //Set the position to the ray's end point, plus half the length of the hoverboard
        }
    }

	void OnCollisionEnter(Collision col)
	{
		//Debug.Log ("Collides");
		//movement.ResetPosition();
	}
}
