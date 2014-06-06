using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif
/*
 * This script add option for the trail effect when the player reaches high speed.
 * This scripts sets the lifetime of the trail and on lower speed the lifetime
 * decresses to make a nice effect when the speed is 0.
 * 
 * Later modifications made it possible to change the width of the trail. This was made
 * as an request for a better effect to show that a player is boosting.
 *
 * This script got removed for a better effect.
 * 
 * Created by: Erik Åsén, 2014-04-09
 * Edited by:
 */

public class TrailRendScript : MonoBehaviour {
	
	public Movement m_MovementReference;
	private TrailRenderer m_RenderReference;
	[SerializeField]
	private float maxDisplayTime = 1.0f, decreaseRate = 0.1f, increaseRate = 0.1f, showRayThreshold = 40;
	[SerializeField]
	private Vector2 normalSize, boostSize;
	
	void Start () 
	{
		m_RenderReference = GetComponent<TrailRenderer> ();
		m_RenderReference.enabled = true;
		m_RenderReference.time = 0;
		m_RenderReference.startWidth = normalSize.x;
		m_RenderReference.endWidth = normalSize.y;

	}
	
	void Update () {
		if(m_MovementReference.boostSpeed < 1f)
		{
			trailWidth(normalSize.x,normalSize.y);
			//Tail will only be displayed when player is moving over a set speed and not boosting
			if (m_MovementReference.m_getVelocity.magnitude > showRayThreshold)
			{
				incTrailTime();
			}
			//When player is slowing down shorten tail, and when less then 0 set it to orignal length(time) and stop displaying
			else
			{
				decTrailTime();
			}
			//Going backward stop displaying tail.
		}
		else
		{
			trailWidth(boostSize.x,boostSize.y);
			incTrailTime();
		}
	}
	// Increase the life time of the trail there by making it longer
	private void incTrailTime()
	{
		if(m_RenderReference.time < maxDisplayTime)
		{
			m_RenderReference.time += increaseRate;
		}
	}
    // Decreasing the life time of the trail there by making it shorter
	private void decTrailTime()
	{
		if (m_RenderReference.time > 0)
		{
			m_RenderReference.time -= decreaseRate;
		}
	}

    //Chaning the width of the trail depending on the in data that is sent in the update
	private void trailWidth( float sizeStart, float sizeEnd )
	{
        //Every != is there so it stops increasing or decrecing the witdh of the trail/tail.
		if(m_RenderReference.startWidth < sizeStart)
		{
			m_RenderReference.startWidth += increaseRate;
		}
		else if(m_RenderReference.startWidth > sizeStart)
		{
			m_RenderReference.startWidth -= decreaseRate;
		}

		if(m_RenderReference.endWidth < sizeEnd)
		{
			m_RenderReference.endWidth += increaseRate;
		}
		else if(m_RenderReference.endWidth > sizeEnd)
		{
			m_RenderReference.endWidth -= decreaseRate;
		}
	}
}