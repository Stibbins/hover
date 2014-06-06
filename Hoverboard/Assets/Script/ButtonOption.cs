using UnityEngine;
using System.Collections;
// detta script används inte för stunden
public class ButtonOption : MonoBehaviour {
									//Currently default if you dont want to change the buttons:
	public KeyCode jump;          //Space
	public KeyCode forward;       //W
	public KeyCode back; 		  //S
	public KeyCode leftRotation;  //A
	public KeyCode rightRotation; //D
	public KeyCode right;		  //L
	public KeyCode left;		  //J
	public KeyCode boost;		  //B


	// Use this for initialization
	void Awake () {
		forward = KeyCode.W;


	}
	
	// Update is called once per frame



 	/*public void setKey(string changeKey, KeyCode key)
	{
		switch(changeKey)
		{
		case "jump":
			jump = key;
			break;

		case "forward":
			forward = key;
			break;

		case "back":
			back = key;
			break;

		case "leftRotation":
			leftRotation = key;
			break;

		case "rightRotation":
			rightRotation = key;
			break;

		case "right":
			right = key;
			break;

		case "left":
			left = key;
			break;
		}
	}*/

	public KeyCode getKey( string key)
	{
		switch (key) 
		{
		case "jump":
		
			return jump;

		case "forward":
			Debug.Log("Returning" + forward);
			return forward;


		case "back":
			return back;

		case "leftRotation":
			return leftRotation;

		case "rightRotation":
			return rightRotation;

		case "right":
			return right;

		case "left":
			return left;

		case "boost":
			return boost;

		default:
			return KeyCode.UpArrow;
		
		}
	}

}