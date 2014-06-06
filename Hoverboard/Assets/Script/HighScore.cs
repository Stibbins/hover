/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Handles storing a high score list for each level
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class HighScore : MonoBehaviour {
    [SerializeField]
    int maxScoreCount = 10;

    string userName="Platform not defined";

    //HighScoreList is stored on file as: username:time
    List<KeyValuePair<string, float>> highScoreList;
    public List<KeyValuePair<string, float>> m_highScoreList
    {
        get { return highScoreList; }
    }

    bool hasAddedTime=false;

    Finish finishScript;

    string filePath;
    
	void Start ()
    {
#if UNITY_STANDALONE
        InitPC();
#elif UNITY_XBOXONE
        InitXBoxOne();
#endif

        highScoreList = new List<KeyValuePair<string, float>>();
        finishScript = GameObject.Find("Finish").GetComponent<Finish>();

        filePath = Application.persistentDataPath + "/" + Application.loadedLevelName + ".txt";
        if (File.Exists(filePath))
        {
            StreamReader file = new StreamReader(filePath);
            string row;
            while ((row = file.ReadLine()) != null)
            {
                highScoreList.Add(new KeyValuePair<string, float>(row.Split(":".ToCharArray())[0], float.Parse(row.Split(":".ToCharArray())[1])));//new KeyPair(row.Split(":".ToCharArray())[0], row.Split(":".ToCharArray())[1], true));
            }
            file.Close();
        }
    }
	
	void Update () 
    {
        if (finishScript.m_finishTime > 0.0001 && !hasAddedTime) //Player reached the goal
        {
            hasAddedTime = true;

            AddToHighScore(finishScript.m_finishTime);
        }
	}

    void AddToHighScore(float time)
    {
        highScoreList.Add(new KeyValuePair<string, float>(userName, time));

        highScoreList.Sort((firstPair, nextPair) =>
        {
            return firstPair.Value.CompareTo(nextPair.Value);
        });

        //Make sure highScoreList doesn't have too many elements
        if (highScoreList.Count > maxScoreCount)
        {
            //Remove the last element
            highScoreList.RemoveAt(highScoreList.Count - 1);
        }

        StreamWriter file=new StreamWriter(filePath);

        //Write the updated high score list to the file
        for (int i = 0; i < highScoreList.Count; i++) //Iterate through highScoreList
        {
            file.WriteLine(highScoreList[i].Key.ToString() + ":" + highScoreList[i].Value.ToString());
        }

        file.Close();
    }

#if UNITY_STANDALONE
    void InitPC()
    {
        userName = "UserOnPC";
    }
#elif UNITY_XBOXONE
    void InitXBoxOne()
    {
        userName = "UserOnXBoxOne";
    }
#endif
}
