using UnityEngine;
using System.Collections;

/*
 * Adds a controler for motion blur for easy edit. This script also
 * increases the amount of blur applied when a player reaches a speed
 * threshold. 
 * 
 * When editing a person can also change the area of the screen the blur
 * will be applied to. This is done by either increasing the value (more of
 * the screen will be coved with blur) or decreasing(less of the screen will
 * be coverd in blur).
 *
 * Created by: Erik Åsén, 2014-04-15
 * Edited by:
 */

public class MotionBlurControl : MonoBehaviour {


	[SerializeField]
	private Movement m_LogicHoverboard;
	//Recomended to keep Decrease and Increase the same
	[SerializeField]
	private float speedThreshold = 40.0f, increaseAmount = 0.1f, decreaseAmount = 0.1f, blurLimit = 5.0f, areaOfBlur = 1.0f;
    [SerializeField]
    private bool extraBlur = false;
	private float zero = 0f, prevAreaOfBlur;
	private MotionBlur m_BlurReference;

	// Use this for initialization
	void Start () 
	{
		m_BlurReference = GetComponent<MotionBlur> ();
		m_BlurReference.blurAmount = zero;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_LogicHoverboard.m_getVelocity.magnitude >= speedThreshold && m_BlurReference.blurAmount < (blurLimit/10))
		{
			m_BlurReference.blurAmount += increaseAmount/100;
		}
		else
		{
			m_BlurReference.blurAmount -= decreaseAmount/100;
		}
		if( areaOfBlur != prevAreaOfBlur)
		{
			m_BlurReference.size = areaOfBlur;
			prevAreaOfBlur = areaOfBlur;
		}
		if( m_BlurReference.extraBlur != extraBlur)
		{
			m_BlurReference.extraBlur = extraBlur;
		}
	}
}
