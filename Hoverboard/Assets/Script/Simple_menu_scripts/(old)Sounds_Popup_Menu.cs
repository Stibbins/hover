using UnityEngine;
using System.Collections;

public class Sounds_Popup_Menu : MonoBehaviour {
	public UIPopupList popup;
	public string myItem;

	void Update()
	{
		if (popup.selection == "Driver caps") 
		{
			AudioSettings.speakerMode = AudioSettings.driverCaps;
		}
	}
}
