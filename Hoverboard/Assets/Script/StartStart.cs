using UnityEngine;
using System.Collections;

public class StartStart : MonoBehaviour {

	[SerializeField]
	private Finish finishScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	if (Input.GetButton("Jump"))
	{
		finishScript.LoadNextLevel();
	}
	
	
	}
}
