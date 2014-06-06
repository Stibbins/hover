using UnityEngine;
using System.Collections;

/*
 * 
 * Modifes a light below the player change its intensity on a sin curve.
 * When a player is starting its jump the intensity resets and the color
 * changes.
 * 
 * Created by: Erik Åsén
 * Edited by: Felix (Wolfie) Mossberg
 * 
 */


public class LightningHoverBoard : MonoBehaviour {

	public Movement m_MovementScript;

	[SerializeField]
	private float IntensityThreshold = 8, PulseSpeed = 1;
	[SerializeField][Range(0f,1f)]
	private float coloChangeSpeed = 0.025f;
	[SerializeField]
	private Color col = Color.cyan, col_charged = Color.red;
	private float t = 0;

	float TimeSin;
	// Use this for initialization
	void Start () 
	{		
		light.color = col;
	}
	
	// Update is called once per frame
	void Update () {
		t = Mathf.Clamp (t, 0f, 1f);

		TimeSin = Mathf.Sin(Time.time*PulseSpeed);
		
		fluctuateLightStrength();

		changeColor();
		//test ();
	}
	private void fluctuateLightStrength()
	{
		if(Input.GetButton("Jump"))
		{
			light.intensity += 0.1f; 
		}
		else
		{
			light.intensity = IntensityThreshold * Mathf.Abs(TimeSin);
		}
	}
	private void changeColor()
	{
		if (m_MovementScript.jumpVelocity > 0f)
		{
			t += coloChangeSpeed;
			light.color = Color.Lerp(col, col_charged, t);
		} 
		else
		{
			t -= coloChangeSpeed;
			light.color = Color.Lerp(col, col_charged, t);
		}
	}
}
