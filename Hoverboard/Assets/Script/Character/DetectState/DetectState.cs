/*
 * Created by: Robbin
 * 
 * Description:
 * This script detects what state (grinding etc) the player the player is in.
 * The state is accessed with m_state, for use in other scripts.
 *      
 * If no state are found, it's set to Default
 * Edited by: Niklas Linder, Erik Åsen.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;

public class DetectState : MonoBehaviour {

	private KeyState currentState;
	private bool rayCastState = true;
	private bool railKeyPressed;
	private float keyIsPressed;
	
    private Animator animator; //The animator of the character model

	public bool m_getRailPermission
	{
		get{return railKeyPressed;}
		set{railKeyPressed = value;}
	}
	public bool m_getRayCastState
	{
		get { return rayCastState; }
		set { rayCastState = value;}
	}

	//public KeyState key = new MoveKeyState (gameObject.GetComponent<Movement>);

	private Dictionary<string,KeyState> keyStateDictionary = new Dictionary<string,KeyState>();
	private string currentKeyState;

	private Movement movementScript;
	//----------FMOD reqs.
	

	private FMOD.Studio.EventInstance grindEvent;
	
	private bool playLanding = false;
	private float fallSpeed = 1.0f;
	
	
	
	
	
	//---------end FMOD reqs

	public string getKeyState
	{
		set {
			currentKeyState = value;
		}
		get {return currentKeyState;}
	}
	// Use this for initialization
	void Start () 
    {
    	movementScript = gameObject.GetComponent<Movement>();
		animator = movementScript.m_characterAnimator;
    	
		keyStateDictionary.Add ("Grounded",new MoveKeyState(movementScript));
		keyStateDictionary.Add ("Air",new AirKeyState(movementScript));
		keyStateDictionary.Add("Rail",new GrindKeyState(movementScript));
		keyStateDictionary.Add("Wall",new WallKeyState(movementScript));
		keyStateDictionary.Add("MenuState",new MenuState(movementScript));
		currentKeyState = "Grounded";

		

        
        
        
        //-----FMOD initialization
		grindEvent = FMOD_StudioSystem.instance.GetEvent("event:/Hoverboard/Grind");
		
		
	}

    void CheckForErrors()
    {
        if (!animator)
        {
            Debug.LogError("Animator not defined in the object "+gameObject.name+"!");
        }

        if (!rigidbody)
        {
            Debug.LogError("Rigidbody not found in the object " + gameObject.name + "!");
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
		grindEvent.set3DAttributes(UnityUtil.to3DAttributes(transform.position));
		
		RailKey ();

		updateKeyState (currentKeyState).update();

	}

    //Called every time the state changes
    void UpdateAnimations()
    {
        if (currentKeyState == "Rail")
        {
            animator.SetBool("Grinding", true);
            grindEvent.start ();
        }
        else
        {
            animator.SetBool("Grinding", false);
            grindEvent.stop();
        }

        if (currentKeyState == "Air")
        {
            animator.SetBool("Falling", true);
            if (movementScript.m_getVelocity.y < fallSpeed)
            {
            	playLanding = true;
            }
        }
        else
        {
            animator.SetBool("Falling", false);
            animator.SetBool("Jumping", false);
            
            if (playLanding == true)
            {
            	playLanding = false;
            	FMOD_StudioSystem.instance.PlayOneShot("event:/Hoverboard/Landing", transform.position);
            }
        }

        if (currentKeyState == "Wall")
        {
            if (keyStateDictionary[currentKeyState].setVector.y == 0)
            { //Wall is to the right
                animator.SetBool("WallridingRight", true);
            }
            else
            { //Wall is to the left
                animator.SetBool("WallridingLeft", true);
            }
        }
        else
        {
            animator.SetBool("WallridingRight", false);
            animator.SetBool("WallridingLeft", false);
        }
    }

	public void changeKeyState(string state)
	{
		if(state != currentKeyState)
		{
			Debug.Log (state);
			keyStateDictionary [currentKeyState].end();
			keyStateDictionary [state].start();
			currentKeyState = state;

            UpdateAnimations();
		}

	}
	public KeyState updateKeyState(string keyState)
	{

		return keyStateDictionary[keyState];
	}

	private void RailKey()
	{
		// för XBOX
		//if(Input.GetButtonDown("Y-button"))
		//{
		//	keyIsPressed = Time.time;
		//	railKeyPressed = true;
		//}
		if(Input.GetButtonDown("Grind"))
		{
			keyIsPressed = Time.time;
			railKeyPressed = true;
		}
		if(Time.time > keyIsPressed+1.5f)
		{
			railKeyPressed = false;
		}

	}
}
