using UnityEngine;
using System.Collections;

public class Label_Movement : MonoBehaviour {

	public float modifier;
	Vector3 initial_pos;

	void Awake () {
		initial_pos = transform.position;
		Debug.Log (initial_pos);
	}
	void OnEnable()
	{
		transform.position = initial_pos;
		Debug.Log (initial_pos);
	}
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.up / modifier * Time.deltaTime);
	}
}