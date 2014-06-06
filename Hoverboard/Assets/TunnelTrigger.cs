using UnityEngine;
using System.Collections;

public class TunnelTrigger : MonoBehaviour {
	private float angle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider col)
	{
		col.GetComponent<Movement> ().m_MaxAngle = angle;
		Debug.Log ("OUT");
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log ("INSIDE");
		angle = col.GetComponent<Movement> ().m_MaxAngle;
		col.GetComponent<Movement> ().m_MaxAngle = 0;
	}
}
