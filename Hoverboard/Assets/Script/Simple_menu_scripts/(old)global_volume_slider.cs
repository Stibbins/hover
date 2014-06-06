using UnityEngine;
using System.Collections;

public class global_volume_slider : MonoBehaviour {
	public UISlider Bar;
	float val;
	void Update()
	{
		val = Bar.sliderValue;
		AudioListener.volume = val;
	}
}
