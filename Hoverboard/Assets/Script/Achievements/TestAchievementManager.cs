/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Tests AchievementManager.cs
 * Should not be used in release
 */

using UnityEngine;
using System.Collections;

public class TestAchievementManager : MonoBehaviour {

    [SerializeField]
    bool progressingAchievement1, progressingAchievement2;

#if UNITY_EDITOR
	void Update () {
        if (Application.loadedLevelName == "Robbin")
        {
            if (progressingAchievement1)
            {
                gameObject.GetComponent<AchievementManager>().AddProgressToAchievement("Test achievement", Time.deltaTime);
            }
            else
            {
                gameObject.GetComponent<AchievementManager>().SetProgressToAchievement("Test achievement", 0);
            }

            if (progressingAchievement2)
            {
                gameObject.GetComponent<AchievementManager>().AddProgressToAchievement("Test achievement 2", Time.deltaTime);
            }

            gameObject.GetComponent<AchievementManager>().SetProgressToAchievement("It's over 9000!!!!!!!", gameObject.GetComponent<AchievementManager>().GetCurrentRewardPoints());

        }
	}
#endif
}
