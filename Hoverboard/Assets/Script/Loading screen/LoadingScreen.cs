/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Handles the loading screen
 */

using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

    ProgressBar progressBarScript;

    [SerializeField]
    GameObject[] objectsToHide;

	void OnEnable () {
        transform.position = Vector3.zero;

        progressBarScript = gameObject.GetComponentInChildren<ProgressBar>();

        foreach (GameObject obj in objectsToHide)
        {
            obj.SetActive(false);
        }
	}

    void Start()
    {
        //This is enabled when the player reaches the goal
        //gameObject.SetActive(false);
    }

    public void SetProgress(int progressInPercent)
    {
        progressBarScript.SetProgress(progressInPercent);
    }

    public void SetName(string name)
    {
        foreach (LevelName script in gameObject.GetComponentsInChildren<LevelName>())
        {
            script.SetName(name);
        }
    }
}
