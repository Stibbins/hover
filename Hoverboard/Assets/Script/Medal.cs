using UnityEngine;
using System.Collections;

/*
 * This script return how well/fast the player finished the level
 * 
 * Created by: Andreas Sundberg, date: 2014-04-21
 * 
 * Edited by:
 */

public class Medal : MonoBehaviour {
	
  	public int bronzeTime;
	public int silverTime;
	public int goldTime;


	//public class Timer : MonoBehaviour;
	private Timer timer;
	// Use this for initialization
	void Start () {
		timer = GameObject.Find("TimerText").GetComponent<Timer> ();
	}

	// Update is called once per frame
	void Update () {

		
	}

	/*public void setBronze(float minutes, float seconds, float milliSeconds)
	{
		bronzeMinutes = minutes;
		bronzeSeconds = seconds;
		bronzeMilliSeconds = milliSeconds;
	}

	public void setSilver(float minutes , float seconds, float milliSeconds)
	{
		silverMinutes = minutes;
		silverSeconds = seconds;
		silverMilliSeconds = milliSeconds;
	}

	public void setGold(float minutes, float seconds, float milliSeconds)
	{
		goldMinutes = minutes;
		goldSeconds = seconds;
		goldMilliSeconds = milliSeconds;
	}*/

	public string getMedal()
	{
	
		int time = (int)(timer.m_finishTime);


		if(goldTime > time)
			return "Gold";
		else if(silverTime > time)
			return "Silver";
		else if(bronzeTime > time)
			return "Bronze";

		return "FAIL";
	}

}
