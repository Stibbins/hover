using UnityEngine;
using System.Collections;

/*
 * This script calls the function to stop the camera when the player falls into the gaint 
 * invis kill box below the world.On exit it will reset the players position to its
 * last checkpoint and reenable the camera.
 * 
 * Created by: Erik Åsén (2014-05-22)
 * Edited by:
 *
 */

public class KillBoxScript : MonoBehaviour {

	[SerializeField]
	private Checkpoint checkpoint;

	void OnTriggerEnter(Collider col)
	{
		if(col.CompareTag("Player"))
		{
			GlobalFuncVari.stopCam ();
		}

	}

	void OnTriggerExit(Collider col)
	{
		if(col.CompareTag("Player"))
		{
			GlobalFuncVari.followCam ();
			checkpoint.SpawnAtCheckpoint ();
		}
	}
}
