using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/*
 * Script that record the hoverboards movement 
 * 
 * Created by: Andreas Sundberg Date: 2014-05-08
 * 
 * Edited by:
 */

public class Ghost : MonoBehaviour {
	
	public float m_howManyTimesPerSecond; // how many times the hoverboard's movement should be recorded per second
	private float timeToChange;			  //if time > timeToChange then it's time to update the recordings
	private List<string> stateList = new List<string>(); //List of the states that the hoverboard are in
	private List<Vector3> positionList = new List<Vector3>();	//list of hoverboard's positions
	private List<Quaternion> transformationList = new List<Quaternion>();  // list of hoverboard's rotations
	
	private List<float> timeBetweenUpdatesList = new List<float>();  // list of the time it took between two list updates
	public GameObject hoverboard;
	public bool isRecording;
	private int i = 0;
	private bool fetchFromTextFile = false;
	
	private Movement movement;
	
	private Vector3 positionMovingTo = new Vector3(0,0,0);	//the current position the ghost should move to
	private Quaternion anglesMovingTo;		// the current rotation the ghost should move to
	private float timeToLerp;		// how fast the position and rotation should slerp		
	private float reduceLerpTime;	//reduce how much time it should slerp
	
	
	public bool saveRecordings = false;
	public bool canRecordThisHoverboard;
	
	
	private string filepath;
	private float timebetweenTwoUpdates;		// how long time it took between two list updates
	private float currentTimeBetweenTwoUpdates; 
	private DetectState currentState;
	// Use this for initialization
	void Start () {
		timeToChange = 0;
		anglesMovingTo.Set (0, 0, 0, 0);
		if(canRecordThisHoverboard)
		{
			currentState = hoverboard.GetComponent<DetectState>();
			movement = hoverboard.GetComponent<Movement>();
		}
		if (!isRecording)
			fetchFromTextFile = true;
		filepath = Application.persistentDataPath + "/Ghost.txt";
		timeToLerp = 1/(m_howManyTimesPerSecond);
		reduceLerpTime = timeToLerp / (m_howManyTimesPerSecond);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!isRecording && !saveRecordings)	// if the hoverboard doesn't record its movement then call the function playback() and slerp the position and rotation 
		{
			PlayBack();
			hoverboard.transform.position = Vector3.Slerp (hoverboard.transform.position, positionMovingTo, timeToLerp);
			hoverboard.transform.rotation = Quaternion.Slerp (hoverboard.transform.rotation, anglesMovingTo, timeToLerp);
			if(timeToLerp > 0)
				timeToLerp -= reduceLerpTime;
			else
				timeToLerp = 0;
			
			
		}
		else if(isRecording && !saveRecordings) 
		{
			if(canRecordThisHoverboard)
				Recording();
			
		}
		else if(saveRecordings && canRecordThisHoverboard)
		{
			SaveRecordings();
		}
	}
	
	
	void Recording()
	{
		if(Time.time > timeToChange) //if time is greater than timeToChange then add the hoverboard's position, rotation, state and time it took between this update and last update
		{ 							  // and put it in the lists. Then give timeToChange a higher value
			if(positionList.Count == 0)
			{
				if(canRecordThisHoverboard)
				{
					stateList.Add(currentState.getKeyState);
				}
				positionList.Add(hoverboard.transform.position);
				transformationList.Add(hoverboard.transform.rotation);
				timebetweenTwoUpdates = Time.time;
				timeToChange = Time.time + 1f/m_howManyTimesPerSecond;
				
			}
			else
			{
				if(canRecordThisHoverboard)
				{
					stateList.Add(currentState.getKeyState);
				}
				positionList.Add(hoverboard.transform.position);
				transformationList.Add(hoverboard.transform.rotation);
				float temp = Time.time - timebetweenTwoUpdates;
				timeBetweenUpdatesList.Add(temp);
				timebetweenTwoUpdates = Time.time;
				timeToChange = Time.time + 1f/m_howManyTimesPerSecond;
				
			}
			
		}
	}
	
	void PlayBack()
	{ 					
		
		//Debug.Log ("Ghost position: " + transform.position.ToString ());
		//Debug.Log ("Position moving to: " + positionMovingTo.ToString ());
		
		int readInfo = 0;  // what kind of type the line in the textfile should be in
		if(positionList.Count == 0 && fetchFromTextFile) // if the lists are empty then get info for the ghost in a textfile and add the info to the lists
		{
			
			StreamReader readText = new StreamReader(filepath);
			while(!readText.EndOfStream)
			{
				
				string info = readText.ReadLine();
				
				if(info == "Rotation" || info == "State" || info == "Time")
				{
					if(info == "Rotation")
						readInfo = 2;
					if(info == "State")
					{
						readInfo = 1;
					}
					if(info == "Time")
					{
						readInfo = 3;
					}
				}
				else
				{
					if(readInfo == 0)	//if readInfo equals 0 convert the line to vector3 and add it to positionList
					{
						string[] xyz = info.Split(',');
						if(xyz.Length == 3)
						{
							float x = float.Parse(xyz[0]);
							float y = float.Parse(xyz[1]);
							float z = float.Parse(xyz[2]);
							Vector3 temp = new Vector3(x,y,z);
							
							positionList.Add(temp);
						}
					}
					else if(readInfo == 1)   //if readInfo equals 1 add the line in the stateList
					{
						
						stateList.Add(info);
					}
					else if(readInfo == 2)  //if readInfo equals 2 convert the line to the type Quaternion and add it to transformationList
					{
						string[] xyzw = info.Split(',');
						if(xyzw.Length == 4)
						{
							
							float x = float.Parse(xyzw[0]);
							float y = float.Parse(xyzw[1]);
							float z = float.Parse(xyzw[2]);
							float w = float.Parse(xyzw[3]);
							Quaternion angle = new Quaternion(x,y,z,w);
							
							transformationList.Add(angle);
						}
						
					}
					else if(readInfo == 3)   //if readInfo equals 3 convert the line to float and add it to timeBetweenUpdatesList
					{
						float temp = float.Parse(info);
						timeBetweenUpdatesList.Add(temp);
					}
				}
				
			}
			readText.Close();
			
			
		}
		int size = smallestSize (stateList.Count, positionList.Count, transformationList.Count, timeBetweenUpdatesList.Count);  //check which list that has the smallest size so 
		//it can't be out of bound.																									
		if(i < size && Time.time > timeToChange)  // if i < size and time is greater than timeToChange it's time to update what the ghost should do.
		{
			if(i == 0) // if i equals 0 then the ghost get the initialize values.
			{ 
				hoverboard.transform.position = positionMovingTo = positionList[i];
				if(canRecordThisHoverboard)
				{
					movement.ResetPosition(positionMovingTo);
					movement.isRecording = false;
					currentState.changeKeyState(stateList[i]);
				}
				
				
				
				anglesMovingTo.Set(transformationList[i].x, transformationList[i].y, transformationList[i].z,transformationList[i].w);
				transform.rotation.Set(anglesMovingTo.x,anglesMovingTo.y,anglesMovingTo.z,anglesMovingTo.w); 
				
				currentTimeBetweenTwoUpdates = timeBetweenUpdatesList[i];
				timeToLerp = 1/(m_howManyTimesPerSecond);
				timebetweenTwoUpdates = Time.time;
			}
			
			
			float x = Mathf.Abs(positionMovingTo.x - hoverboard.transform.position.x);
			float z = Mathf.Abs(positionMovingTo.z - hoverboard.transform.position.z);
			float temp = Time.time - timebetweenTwoUpdates;
			if(i != 0 &&Time.time > timeToChange && temp > currentTimeBetweenTwoUpdates) //if i < 0 and currentTimeBetweenTwoUpUpdates is smaller than the time it has taken
			{																			 //to change the ghost's values then update the values that the ghost should change to.
				if(canRecordThisHoverboard)
				{
					currentState.changeKeyState(stateList[i]);
				}
				
				positionMovingTo = positionList[i];
				anglesMovingTo.Set(transformationList[i].x, transformationList[i].y, transformationList[i].z,transformationList[i].w);
				currentTimeBetweenTwoUpdates = timeBetweenUpdatesList[i];
				timebetweenTwoUpdates = Time.time;
				timeToLerp = 1/(m_howManyTimesPerSecond);
			}
			i++;
			timeToChange = Time.time + (1f/m_howManyTimesPerSecond);
			
		}
		if(i == size && Time.time > timeToChange)
		{
			isRecording = true;
			if(canRecordThisHoverboard)
			{
				hoverboard.GetComponent<Movement>().isRecording = true;
			}
		}
		
	}
	
	void SaveRecordings()	//This function saves the recording to a textfile.
	{
		if(!fetchFromTextFile)
		{
			StreamWriter text = new StreamWriter(filepath);
			
			for(int j = 0; j < positionList.Count; j++)
			{
				text.WriteLine(positionList[j].x + "," + positionList[j].y + "," + positionList[j].z);
			}
			
			text.WriteLine("State");
			for(int j = 0; j < stateList.Count; j++)
			{
				text.WriteLine(stateList[j]);
			}
			
			text.WriteLine("Rotation");
			
			for(int j = 0; j < transformationList.Count; j++)
			{
				text.WriteLine(transformationList[j].x + "," + transformationList[j].y + "," + transformationList[j].z + "," + transformationList[j].w);
			}
			
			text.WriteLine("Time");
			
			for(int j = 0; j < timeBetweenUpdatesList.Count; j++)
			{
				text.WriteLine(timeBetweenUpdatesList[j]);
			}
			
			text.Close();
		}
		saveRecordings = false;
	}
	int smallestSize(int a, int b, int c, int d)
	{
		if (a <= b && a <= c && a <= d)
			return a;
		if (b <= a && b <= c && b <= d)
			return b;
		if (c <= a && c <= b && c <= d)
			return c;
		
		return d;
	}
	
}