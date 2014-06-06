using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class FMOD_EngineEmitter : MonoBehaviour {
	
	
	private FMOD.Studio.EventInstance hoverSound;
	private FMOD.Studio.ParameterInstance speedPitch;
	private float speedSmooth;
	private float speedValue;
	private FMOD.Studio.ParameterInstance boostPitch;
	private float boostSmooth;
	private float boostValue;
	
	private Movement movementScript;
	private Boost boostScript;
	
	// Use this for initialization
	void Start () 
	{		
		hoverSound = FMOD_StudioSystem.instance.GetEvent("event:/Hoverboard/Motor");		
		hoverSound.start();
		if (hoverSound == null)
			Debug.Log("Laddar inte in eventet!!!");		
			
		hoverSound.getParameter("Speed", out speedPitch);		
		if (speedPitch == null)
			Debug.Log("Hittar inte variabeln!!!");
		hoverSound.getParameter("Boost", out boostPitch);
		if (boostPitch == null)
			Debug.Log("Hittar inte Boost variabeln");
			
		movementScript = transform.GetComponent<Movement>();
		boostScript = transform.GetComponent<Boost>();		
	}
	
	// Update is called once per frame
	void Update () 
	{		
		//speedPitch.setValue(Mathf.InverseLerp (0, Input.GetAxisRaw("Triggers"), 0.5f));
		speedPitch.getValue(out speedValue);			
		boostPitch.getValue(out boostValue);
		
		speedPitch.setValue(Mathf.SmoothDamp(speedValue,Mathf.InverseLerp(0.0f,1.0f,Input.GetAxisRaw("Triggers")), ref speedSmooth, 1.0f));
		
		if (boostScript.m_isBoosting == true)
		{
			boostPitch.setValue(Mathf.SmoothDamp(boostValue, 1.0f, ref boostSmooth, 1.0f));
		}
		else
		{
			boostPitch.setValue(Mathf.SmoothDamp(boostValue, 0.0f, ref boostSmooth, 1.0f));
		}
		
		hoverSound.set3DAttributes (UnityUtil.to3DAttributes (transform.position));		
	}
	
	void OnDestroy ()
	{
		hoverSound.stop ();
		hoverSound.release ();		
	}
	
	
}