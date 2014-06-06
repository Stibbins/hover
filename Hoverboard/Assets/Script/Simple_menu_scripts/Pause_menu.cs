using UnityEngine;
using System.Collections;

public class Pause_menu : MonoBehaviour {
	public GameObject MasterPanel;
	[HideInInspector]
	public float pDeltaTime;
	private float realtimeSinceLast;
	[HideInInspector]
	public bool Meny_Active;
	public DetectState detectState;

	private string keyState;
	// Use this for initialization
	void Start () {
		Meny_Active = false;
	}
	
	// Update is called once per frame
	void Update () {
		pDeltaTime = Time.realtimeSinceStartup - realtimeSinceLast;
		realtimeSinceLast = Time.realtimeSinceStartup;
	if (Input.GetKeyDown(KeyCode.P))
		{
			keyState = detectState.getKeyState;

			Meny_Active = !Meny_Active;
			MasterPanel.SetActive(Meny_Active);

			if(Meny_Active)
			{
				Time.timeScale = 0;
				detectState.changeKeyState("MenuState");
			}
			if(!Meny_Active)
			{
				Time.timeScale = 1;
				detectState.changeKeyState(keyState);
			}
		}
	}

	public void Forced_Unpause()
	{
		Time.timeScale = 1;
	}
}
