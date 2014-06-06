using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class FirstTutorialScript : MonoBehaviour {


	[SerializeField]
	private GUITextureDisplay textureDisplay;
	[SerializeField]
	private GUI_Sound_Emitter soundEmitter;
	[SerializeField]
	private string logicBoard = "Hoverboard 4.0";
	private GameObject playerObject;
	private Movement movementScript;
	private Boost boostScript;
	
	[SerializeField]
	private FMODAsset tutorialSound;
	private FMOD.Studio.EventInstance tutorialEvent;
	private FMOD.Studio.PLAYBACK_STATE tutorialEventState;
	private bool tutorialPlayed;
	
	
	[SerializeField]
	private Texture tutorialHint1;
	
	[SerializeField]
	private Texture tutorialHint2;
	
	[SerializeField]
	private Texture tutorialHint3;
	
	
	private float timer1 = 20;
	private float timer2 = 40;
	private bool switch1;
	private bool switch2;

	// Use this for initialization
	void Start () 
	{
		playerObject = GameObject.Find(logicBoard);
		movementScript = playerObject.GetComponent<Movement>();
		boostScript = playerObject.GetComponent<Boost>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		textureDisplay.tutorialTexture(null);
		if (col.tag == "Player" && GlobalFuncVari.getTutorialSkipped() != true && tutorialPlayed == false)
		{
			movementScript.enabled = false;
			boostScript.enabled = false;
			tutorialEvent = soundEmitter.startEvent(tutorialSound, false);
			textureDisplay.tutorialTexture(tutorialHint1);
			timer1 += Time.time;
			timer2 += Time.time;
		}
	}
	
	void OnTriggerStay (Collider col)
	{
		if (col.tag == "Player")
		{
			tutorialEvent.getPlaybackState(out tutorialEventState);
			if (tutorialEventState == PLAYBACK_STATE.STOPPED)
			{
				movementScript.enabled = true;
				boostScript.enabled = true;
				tutorialPlayed = true;
				textureDisplay.tutorialTexture(null);
			}
			if (Input.GetButtonDown("Cancel"))
			{
				GlobalFuncVari.setTutorialSkipped(true);
				soundEmitter.stopEvent(tutorialEvent);
				textureDisplay.tutorialTexture(null);
				movementScript.enabled = true;
				boostScript.enabled = true;
			}
			float timePos;
			timePos = Time.time;
			
			if (timePos >= timer1 && timePos < timer2)
			{
				if (switch1 == false)
				{
					textureDisplay.tutorialTexture(tutorialHint2);
					switch1 = true;
				}
			}
			
			if (timePos >= timer2)
			{
				if (switch2 == false)
				{
					textureDisplay.tutorialTexture(tutorialHint3);
					switch2 = true;
				}
			}
			
		}
	}
	
	void OnTiggerExit(Collider col)
	{
		if (col.tag == "Player" && GlobalFuncVari.getTutorialSkipped() == false)
		{
			soundEmitter.stopEvent(tutorialEvent);
			textureDisplay.tutorialTexture(null);
		}
	}
	
}
