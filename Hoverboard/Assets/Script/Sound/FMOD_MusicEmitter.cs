using UnityEngine;
using System.Collections;
using FMOD.Studio;

public class FMOD_MusicEmitter : MonoBehaviour {

	[SerializeField]
	private Transform listenerObject;
	[SerializeField]
	private FMODAsset musicAsset;
	
	
	private FMOD.Studio.EventInstance musicEvent;
	
	private FMOD.Studio.ParameterInstance readyParam;
	private FMOD.Studio.ParameterInstance steadyParam;
	private FMOD.Studio.ParameterInstance goParam;

	// Use this for initialization
	void Start () 
	{
		musicEvent = FMOD_StudioSystem.instance.GetEvent(musicAsset);
		musicEvent.start();
		musicEvent.getParameter("Ready", out readyParam);
		musicEvent.getParameter("Steady", out steadyParam);
		musicEvent.getParameter("Go", out goParam);
	}
	
	// Update is called once per frame
	void Update () 
	{
		musicEvent.set3DAttributes(UnityUtil.to3DAttributes(listenerObject.position));
	}
	
	public void setReady()
	{
		readyParam.setValue(1.0f);
		steadyParam.setValue(0.0f);
		goParam.setValue(0.0f);
	}
	
	public void setSteady()
	{
		readyParam.setValue(0.0f);
		steadyParam.setValue(1.0f);
		goParam.setValue(0.0f);
	}
	
	public void setGo()
	{
		readyParam.setValue(0.0f);
		steadyParam.setValue(0.0f);
		goParam.setValue(1.0f);
	}
	
}
