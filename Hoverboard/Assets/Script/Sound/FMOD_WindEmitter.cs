using UnityEngine;
using System.Collections;
using FMOD.Studio;


public class FMOD_WindEmitter : MonoBehaviour {


	private FMOD.Studio.EventInstance windEvent;
	private FMOD.Studio.ParameterInstance speedParam;
	//private FMOD.Studio.ParameterInstance jumpParam;

	[SerializeField]
	private Movement moveScript;
	
	// Use this for initialization
	void Start () 
	{
		windEvent = FMOD_StudioSystem.instance.GetEvent("event:/Vind/Vind");
		windEvent.start ();
		
		windEvent.getParameter("Speed", out speedParam);
		//windEvent.getParameter("Jump", out jumpParam);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		windEvent.set3DAttributes(UnityUtil.to3DAttributes(transform.position));
		
		speedParam.setValue(Mathf.InverseLerp(0.0f, moveScript.boostMaxAccSpeed, moveScript.m_getVelocity.magnitude));
	}
}
