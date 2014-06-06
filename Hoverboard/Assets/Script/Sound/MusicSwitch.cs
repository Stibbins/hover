using UnityEngine;
using System.Collections;

public class MusicSwitch : MonoBehaviour {

	[SerializeField]
	private bool switchToReady;
	[SerializeField]
	private bool switchToSteady;
	[SerializeField]
	private bool switchToGo;
	
	

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			if (switchToReady)
			{
				col.GetComponent<FMOD_MusicEmitter>().setReady();
				Debug.Log("Ready");
			}
			else if (switchToSteady)
			{
				col.GetComponent<FMOD_MusicEmitter>().setSteady();
				Debug.Log("Steady");
			}
			else if (switchToGo)
			{
				col.GetComponent<FMOD_MusicEmitter>().setGo();
				Debug.Log("Go");
			}
			else 
			{
				Debug.LogError("Too many or too few bools checked");	
			}
		}
	}
	
}
