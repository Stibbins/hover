using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMOD.Studio;

/*
* Created by: Spjuth 2014-05-20
*
* A simple way to play and manage sound
* from GUI (or other sounds where 3D simulation of sounds is not important).
*
* Sounds that only fire once are not maintained at the moment.
*
*
* TODO:
* Interacting with sounds while they are played
* Break up oneshots and true events into two separate functions
* 
*/


public class GUI_Sound_Emitter : MonoBehaviour {

	
	private List<FMOD.Studio.EventInstance> eventList = new List<FMOD.Studio.EventInstance>();
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	//Kill events that aren't active anymore and update the event's position to 
	//the position of the camera, to avoid fading, panning or loosing events while in motion.
		foreach (FMOD.Studio.EventInstance listedSoundEvent in eventList)
		{
			FMOD.Studio.PLAYBACK_STATE state;
			listedSoundEvent.getPlaybackState(out state);
			if (state == FMOD.Studio.PLAYBACK_STATE.STOPPED)
			{
				listedSoundEvent.stop();
				listedSoundEvent.release();
			}
			else
			{
				listedSoundEvent.set3DAttributes(UnityUtil.to3DAttributes(transform.position));
			}
		}
	}
	
	
	//Not overloaded since people might not be aware of how FMOD handles oneshots
	public FMOD.Studio.EventInstance startEvent(FMODAsset soundEvent, bool playOnce)
	{
		if (FMOD_StudioSystem.instance.GetEvent (soundEvent) != null)
		{
			if (playOnce == true)
			{
				FMOD_StudioSystem.instance.PlayOneShot(soundEvent, transform.position);
				return null;
			}
			else
			{
				eventList.Add(FMOD_StudioSystem.instance.GetEvent(soundEvent));
				FMOD.Studio.EventInstance temp = eventList.Last();
				temp.start();
				return temp;
			}
		}
		else
		{
			Debug.LogError("FMOD Asset " + soundEvent + " not found");
			return null;
		}
		
	}
	
	//Not relevant since all inactive events are killed in Update()
	/*
	//Restart (not reload) a previously stopped event
	public void playEvent (FMODAsset soundEvent)
	{
		FMOD.Studio.EventInstance temp = FMOD_StudioSystem.instance.GetEvent(soundEvent);
		eventList.Find(listedSoundEvent => listedSoundEvent == temp).start();
	}
	*/
	
	
	
	//Does not kill the event, can be resumed with playEvent
	//Not really since it will be killed on next Update()
	public void stopEvent (FMOD.Studio.EventInstance soundEvent)
	{
		//FMOD.Studio.EventInstance temp = FMOD_StudioSystem.instance.GetEvent(soundEvent);
		eventList.Find(listedSoundEvent => listedSoundEvent == soundEvent).stop();
	}
	
	public FMOD.Studio.PLAYBACK_STATE eventState (FMOD.Studio.EventInstance soundEvent)
	{
		//FMOD.Studio.EventInstance temp = FMOD_StudioSystem.instance.GetEvent(soundEvent);
		FMOD.Studio.PLAYBACK_STATE temp;
		eventList.Find(listedSoundEvent => listedSoundEvent == soundEvent).getPlaybackState(out temp);
		return temp;
	}
	
	
	//To make sure the events are dropped correctly.
	void OnDestroy()
	{
		if (eventList.Count() > 0)
		{
			foreach (FMOD.Studio.EventInstance listedSoundEvent in eventList)
			{
				listedSoundEvent.stop();
				listedSoundEvent.release();
			}
		}
	}
}
