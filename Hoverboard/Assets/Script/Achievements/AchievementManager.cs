/*
 * Created by: Robbin (Source: http://www.stevegargolinski.com/progress-a-free-achievement-framework-for-unity/)
 * Modified by: 
 * 
 * Description:
 * Script for handling achievements.
 * The script is under the MIT license. It's free to use (even commercially), but needs to be credited by using the readme.txt in this folder (check the link above for more info)
 */

using System.Linq;
using UnityEngine;
using System.Collections;
using System.IO;

[System.Serializable]
public class Achievement
{
    public string Name;
    public string Description;
    public Texture2D IconIncomplete;
    public Texture2D IconComplete;
    public int RewardPoints;
    public float TargetProgress;
    public bool Secret;
    public bool StoreProgress;

    [HideInInspector]
    public bool Earned = false;
    private float currentProgress = 0.0f;
    public float CurrentProgress { get { return currentProgress; } }

    public bool AddProgress(float progress)
    {
        if (Earned)
        {
            currentProgress = TargetProgress;
            return false;
        }

        currentProgress += progress;
        if (currentProgress >= TargetProgress)
        {
            Earned = true;
            return true;
        }

        return false;
    }

    public bool SetProgress(float progress)
    {
        if (Earned)
        {
            currentProgress = TargetProgress;
            return false;
        }

        currentProgress = progress;
        if (progress >= TargetProgress)
        {
            currentProgress = TargetProgress;
            Earned = true;
            return true;
        }

        return false;
    }

    public void OnGUI(Rect position, GUIStyle GUIStyleAchievementEarned, GUIStyle GUIStyleAchievementNotEarned)
    {
        GUIStyle style = GUIStyleAchievementNotEarned;
        if (Earned)
        {
            style = GUIStyleAchievementEarned;
        }

        GUI.BeginGroup(position);
        GUI.Box(new Rect(0.0f, 0.0f, position.width, position.height), "");

        if (Earned)
        {
            GUI.Box(new Rect(0.0f, 0.0f, position.height, position.height), IconComplete);
        }
        else
        {
            GUI.Box(new Rect(0.0f, 0.0f, position.height, position.height), IconIncomplete);
        }

        GUI.Label(new Rect(80.0f, 5.0f, position.width - 80.0f - 50.0f, 25.0f), Name, style);

        if (Secret && !Earned)
        {
            GUI.Label(new Rect(80.0f, 25.0f, position.width - 80.0f, 25.0f), "Description Hidden!", style);
            GUI.Label(new Rect(position.width - 50.0f, 5.0f, 25.0f, 25.0f), "???", style);
            GUI.Label(new Rect(position.width - 250.0f, 50.0f, 250.0f, 25.0f), "Progress Hidden!", style);
        }
        else
        {
            GUI.Label(new Rect(80.0f, 25.0f, position.width - 80.0f, 25.0f), Description, style);
            GUI.Label(new Rect(position.width - 50.0f, 5.0f, 25.0f, 25.0f), RewardPoints.ToString(), style);
            GUI.Label(new Rect(position.width - 250.0f, 50.0f, 250.0f, 25.0f), "Progress: [" + currentProgress.ToString("0.#") + " out of " + TargetProgress.ToString("0.#") + "]", style);
        }

        GUI.EndGroup();
    }
}

public class AchievementManager : MonoBehaviour
{
    public Achievement[] Achievements;
    public AudioClip EarnedSound;
    public GUIStyle GUIStyleAchievementEarned;
    public GUIStyle GUIStyleAchievementNotEarned;

    public bool m_visible = false;

    private int currentRewardPoints = 0;
    private int potentialRewardPoints = 0;
    private Vector2 achievementScrollviewLocation = Vector2.zero;
    private string filePath, attributeSeparator="\t";

    void Start()
    {
        filePath = Application.persistentDataPath + "/AchievementProgress.txt";

        LoadProgressFromFile();

        ValidateAchievements();
        UpdateRewardPointTotals();
    }

    // Make sure the setup assumptions we have are met.
    private void ValidateAchievements()
    {
#if UNITY_EDITOR
        ArrayList usedNames = new ArrayList();
        foreach (Achievement achievement in Achievements)
        {
            if (achievement.RewardPoints < 0)
            {
                Debug.LogError("AchievementManager::ValidateAchievements() - Achievement with negative RewardPoints! " + achievement.Name + " gives " + achievement.RewardPoints + " points!");
            }

            if (usedNames.Contains(achievement.Name))
            {
                Debug.LogError("AchievementManager::ValidateAchievements() - Duplicate achievement names! " + achievement.Name + " found more than once!");
            }
            usedNames.Add(achievement.Name);
        }
#endif
    }

    private Achievement GetAchievementByName(string achievementName)
    {
        return Achievements.FirstOrDefault(achievement => achievement.Name == achievementName);
    }

    private void UpdateRewardPointTotals()
    {
        currentRewardPoints = 0;
        potentialRewardPoints = 0;

        foreach (Achievement achievement in Achievements)
        {
            if (achievement.Earned)
            {
                currentRewardPoints += achievement.RewardPoints;
            }

            potentialRewardPoints += achievement.RewardPoints;
        }
    }

    public int GetCurrentRewardPoints()
    {
        return currentRewardPoints;
    }

    private void AchievementEarned()
    {
        UpdateRewardPointTotals();
        //AudioSource.PlayClipAtPoint(EarnedSound, Camera.main.transform.position);
    }

    public void AddProgressToAchievement(string achievementName, float progressAmount)
    {
        Achievement achievement = GetAchievementByName(achievementName);
        if (achievement == null)
        {
            Debug.LogWarning("AchievementManager::AddProgressToAchievement() - Trying to add progress to an achievement that doesn't exist: " + achievementName);
            return;
        }

        if (achievement.AddProgress(progressAmount))
        {
            AchievementEarned();
        }

        SaveProgressToFile();
    }

    public void SetProgressToAchievement(string achievementName, float newProgress)
    {
        Achievement achievement = GetAchievementByName(achievementName);
        if (achievement == null)
        {
            Debug.LogWarning("AchievementManager::SetProgressToAchievement() - Trying to add progress to an achievement that doesn't exist: " + achievementName);
            return;
        }

        if (achievement.SetProgress(newProgress))
        {
            AchievementEarned();

            if (!achievement.StoreProgress) //This is run below if achievement.StoreProgress is true
            {
                SaveProgressToFile(); //Store that the achievement is earned
            }
        }

        if (achievement.StoreProgress)
        {
            SaveProgressToFile(); //Store the progress
        }
    }

    //Saves the progress of all achievements to file
    private void SaveProgressToFile()
    {
        StreamWriter file = new StreamWriter(filePath);
        string stringToStore="";

        //Store the achievements, one row per achievement
        foreach (Achievement achievement in Achievements)
        {
            stringToStore = achievement.Name; //Add achievement name
            stringToStore += attributeSeparator + achievement.Earned; //Add wether the achivement is earned or not
            if (achievement.StoreProgress) //Check if the progress should be stored
            {
                stringToStore += attributeSeparator + achievement.CurrentProgress; //Add current progress of the achievement
            }
            else
            {
                stringToStore += attributeSeparator + "0"; //Add 0 as progress
            }

            file.WriteLine(stringToStore);
        }

        file.Close();
    }

    //Loads the progress of the file. Assumes each row use the format "name:::earned:::progress"
    public void LoadProgressFromFile()
    {
        if (File.Exists(filePath))
        {
            StreamReader file = new StreamReader(filePath);

            string row;
            string name, earned, progress;
            string [] splitRow;
            Achievement achievement;

            while ((row = file.ReadLine()) != null)
            {
                splitRow = row.Split(attributeSeparator.ToCharArray());

                name = splitRow[0];
                earned = splitRow[1];
                progress = splitRow[2];

                achievement = GetAchievementByName(name);

                //Set if the achievement is earned or not
                if (earned == "True")
                {
                    achievement.Earned = true;
                }
                else
                {
                    achievement.Earned = false;
                }

                achievement.SetProgress(float.Parse(progress));
            }

            file.Close();
        }
    }

    public bool IsAchievementReached(string achievementName)
    {
        Achievement achievement = GetAchievementByName(achievementName);

        if (achievement == null)
        {
            Debug.LogWarning("AchievementManager::SetProgressToAchievement() - Trying to check if an achievement is reached, but the achievement doesn't exist: " + achievementName);
            return false;
        }

        return achievement.Earned;
    }
     
    void OnGUI()
    {
        if (m_visible)
        {
            float yValue = 5.0f;
            float achievementGUIWidth = 500.0f;

            GUI.Label(new Rect(200.0f, 5.0f, 200.0f, 25.0f), "-- Achievements --");

            // Setup a scrollview, and then fill it with each achievement in our list.

            achievementScrollviewLocation = GUI.BeginScrollView(new Rect(0.0f, 25.0f, achievementGUIWidth + 25.0f, 400.0f), achievementScrollviewLocation,
                                                                new Rect(0.0f, 0.0f, achievementGUIWidth, Achievements.Count() * 80.0f));

            foreach (Achievement achievement in Achievements)
            {
                Rect position = new Rect(5.0f, yValue, achievementGUIWidth, 75.0f);
                achievement.OnGUI(position, GUIStyleAchievementEarned, GUIStyleAchievementNotEarned);
                yValue += 80.0f;
            }

            GUI.EndScrollView();

            GUI.Label(new Rect(10.0f, 440.0f, 200.0f, 25.0f), "Reward Points: [" + currentRewardPoints + " out of " + potentialRewardPoints + "]");
        }
    }
}
