using UnityEngine;
using System.Collections;

public class Bounce_tmp : MonoBehaviour {

	Vector3 TargetPos;
	Vector3 StartPos;
	// Use this for initialization
	void Start () {
		StartPos = gameObject.transform.localPosition;
		TargetPos = StartPos + new Vector3(0,2,0);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.localPosition = Vector3.Lerp (gameObject.transform.localPosition, TargetPos, Time.deltaTime/4);
	}
}
