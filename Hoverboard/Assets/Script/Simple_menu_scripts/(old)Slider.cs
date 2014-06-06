using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour {
	public UISlider Bar;
	float val;
	void Update()
	{
		val = Bar.sliderValue;
		AudioListener.volume = val;
	}
}
