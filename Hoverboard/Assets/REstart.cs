using UnityEngine;
using System.Collections;

public class REstart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.anyKey)
		{
			Application.LoadLevel("Start");
			GlobalFuncVari.setTutorialSkipped(false);
		}
	}
}
