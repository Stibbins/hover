using UnityEngine;
using System.Collections;

public class Levitation : MonoBehaviour {

	public float force;

	void OnTriggerStay(Collider other)
	{
		other.rigidbody.AddForce (Vector2.up * force, ForceMode.Acceleration);
	}
}
