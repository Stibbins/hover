using UnityEngine;
using System.Collections;

/*
 *  Script to set position,rotaiton of the player at the start of the scene.
 *  Also to start the raceTimer when the player leave the trigger area of spawn.
 *
 * Created by: Erik Åsén, 2014-04-02
 * Edited by: 
 */

public class SpawnPosition : MonoBehaviour {
	[SerializeField]
	private Transform m_TargetLogicHoverBoard = null, m_TargetGraphicHoverboard = null;
	[SerializeField]
	private Timer m_TimerReference;

	// Use this for initialization
	void Awake () 
	{
		ResetTransform();
	}

	public void ResetTransform()
    {
        m_TargetLogicHoverBoard.transform.position = transform.position;
        m_TargetLogicHoverBoard.transform.rotation = transform.rotation;
        m_TargetGraphicHoverboard.transform.rotation = transform.rotation;
    }

	void OnTriggerExit(Collider col)
	{
		if(col.CompareTag("Player"))
		{
			m_TimerReference.leaveStartTime();
		}
	}
}
