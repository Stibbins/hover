using UnityEngine;
using System.Collections;

public class GrindVisuals : MonoBehaviour {
	public Transform hoverBoard;
	// Use this for initialization
	void Start () 
	{
		Debug.Log(Camera.main.ScreenToWorldPoint(hoverBoard.position));
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log(Camera.main.ScreenToWorldPoint(hoverBoard.position));
	}
}
