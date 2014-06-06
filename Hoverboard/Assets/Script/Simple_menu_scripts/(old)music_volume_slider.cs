using UnityEngine;
using System.Collections;

public class music_volume_slider : MonoBehaviour {
	public UISlider Bar;
	public AudioSource music_source;
	float val;
	void Update()
	{
		val = Bar.sliderValue;
		music_source.volume = val;
	}
}
