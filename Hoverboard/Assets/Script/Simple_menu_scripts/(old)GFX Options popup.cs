using UnityEngine;
using System.Collections;

public class GFX_Options_popup : MonoBehaviour {
	public UIPopupList popup;

	//string[] names = QualitySettings.names;
	
	void Update()
	{
		if (popup.selection == "Low") 
		{
			QualitySettings.SetQualityLevel(1, true);
		}
	}
}
