/*
 * Created by: Robbin
 * Modified by:
 * 
 * Description:
 * Diplays a loading screen and loads a level
 */

using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
	AsyncOperation operation;
	GUIText newGuiText;

    [SerializeField]
    GameObject loadingScreen;

    LoadingScreen loadingScreenScript;

    [SerializeField]
    string nameToPrint;

    void Start()
    {
        loadingScreenScript = loadingScreen.GetComponent<LoadingScreen>();
        loadingScreen.SetActive(false); //Hide the loading screen at start

#if UNITY_EDITOR
        if (nameToPrint == "")
        {
            Debug.LogWarning("Name to print is not defined in the object "+gameObject.name+". No name will be display in the loading screen.");
        }
#endif
    }

	public void LoadLevel(string levelName)
    {
        ShowLoadingScreen();

        //Load new scene
        //if (Application.HasProLicense())
        //{
		//	operation = Application.LoadLevelAsync(levelName);
		//	
		//	StartCoroutine(SetProgressBar());
        //}
        //else
        //{
            Application.LoadLevel(levelName);
        //}
    }

	public void LoadLevel(int level)
    {
		ShowLoadingScreen();

		//Load new scene
		//if (Application.HasProLicense())
		//{
		//	operation = Application.LoadLevelAsync(level);
		//	
		//	StartCoroutine(SetProgressBar());
		//}
		//else
		//{
            Application.LoadLevel(level);
		//}
    }

	
	IEnumerator SetProgressBar()
	{
		while (!operation.isDone)
		{
            loadingScreenScript.SetProgress((int)(operation.progress * 100));
			
			yield return(0);
		}
	}

    void ShowLoadingScreen()
    {
        loadingScreen.SetActive(true);
        loadingScreenScript.SetName(nameToPrint);
        //loadingScreen.GetComponent<LoadingScreen>().SetProgress(100);
    }
}
