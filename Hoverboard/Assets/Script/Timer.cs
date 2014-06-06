using UnityEngine;
using System.Collections;

/*
 * This script is for the GUI element that shows the race time when a player leaves start.
 * When reaching finish it stops the time displayed time.
 *
 * Created by: Erik Åsén, 2014-04-11
 * Edited by: Robbin Torstensson, 2014-04-22 (added getter for finishTime), Felix (Wolfie) Mossberg
 */

public class Timer : MonoBehaviour {

	float timeMinutes, timeSeconds, timeMilli;
	float raceTime, leaveTime, finishTime;
	string text;

    public float m_finishTime
    {
        get { return finishTime; }
    }

    public float m_raceTime
    {
        get { return raceTime; }
    }
	
	// Update is called once per frame
	void Update () 
	{
		SetRaceTimer ();

		text="";
		if (finishTime == 0) {
			text = string.Format ("{0:00}:{1:00}.{2:000}", (int) raceTime / 60,(int) raceTime % 60, (int) (raceTime * 1000) % 1000);
		} else {
			text = string.Format ("{0:00}:{1:00}.{2:000}", (int) finishTime / 60, (int) finishTime % 60, (int) (finishTime * 1000) % 1000);
		}
		//render
		guiText.text = text;
	
	}

    //Sets the raceTimer to a specific time (checkpoint)
    public void SetRaceTimer(float time)
    {
        if (time != 0)
        {
            leaveTime = Time.time - time;
        }
        else
        {
            leaveTime = 0;
        }
    }
	//Non checkpoint version
	void SetRaceTimer()
	{
		if (leaveTime == 0f) 
		{
			raceTime = 0;
		}
		else 
		{
			raceTime = Time.time - leaveTime;
		}
	}

	//Called in spawn
	public void leaveStartTime()
	{
		leaveTime = Time.time;
	}
	//Called in finish
	public void StopTimer()
	{
		finishTime = raceTime;
	}
	//Called when the level resest is pushed
	public void nullTimer()
	{
		raceTime = 0;
	}
}