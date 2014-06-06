using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class TutorialScript : MonoBehaviour {

	[SerializeField]
	private GUITextureDisplay textureDisplay;
	[SerializeField]
	private GUI_Sound_Emitter soundEmitter;
	[SerializeField]
	private string logicBoard = "Hoverboard 4.0";
	private GameObject playerObject;
	private Movement movementScript;
	private Boost boostScript;
	private Jump jumpScript;
	
	[SerializeField]
	private FMODAsset tutorialSound;
	private FMOD.Studio.EventInstance tutorialEvent;
	private FMOD.Studio.PLAYBACK_STATE tutorialEventState;
	private bool tutorialPlayed;
	
	[SerializeField]
	private Texture tutorialHint;
	
	private bool startedPlaying;
	

	// Use this for initialization
	void Start () 
	{
		playerObject = GameObject.Find(logicBoard);
		movementScript = playerObject.GetComponent<Movement>();
		boostScript = playerObject.GetComponent<Boost>();
		jumpScript = playerObject.GetComponent<Jump>();
		//introEvent = FMOD_StudioSystem.instance.GetEvent(tutorialStartDialogue);
		startedPlaying = false;
	}
	
	// Update is called once per frame
	void OnTriggerStay (Collider col) 
	{
	
		if (col.tag == "Player" && GlobalFuncVari.getTutorialSkipped() != true && tutorialPlayed == false && startedPlaying == false)
		{
			movementScript.enabled = false;
			boostScript.enabled = false;
			jumpScript.enabled = false;
			tutorialEvent = soundEmitter.startEvent(tutorialSound, false);
			textureDisplay.tutorialTexture(tutorialHint);
			startedPlaying = true;
		}
		
		if (col.tag == "Player" && startedPlaying == true)
		{
			
			tutorialEventState = soundEmitter.eventState(tutorialEvent);
			if (tutorialEventState != PLAYBACK_STATE.PLAYING)
			{
				movementScript.enabled = true;
				boostScript.enabled = true;
				jumpScript.enabled = true;
				tutorialPlayed = true;
				//textureDisplay.tutorialTexture(null);
				Debug.Log("LOL");
			}
			if (Input.GetButtonDown("Cancel"))
			{
				GlobalFuncVari.setTutorialSkipped(true);
				soundEmitter.stopEvent(tutorialEvent);
				//textureDisplay.tutorialTexture(null);
				movementScript.enabled = true;
				boostScript.enabled = true;
				jumpScript.enabled = true;
			}
		}
	}
	

}
