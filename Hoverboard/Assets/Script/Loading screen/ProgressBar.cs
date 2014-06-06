/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Handles the loading bar, by setting the pins to the correct texture.
 */

using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    [SerializeField]
    Texture pinLow, pinHigh;

    int debugProcess = 0;

    public void SetProgress(int progressInPercent)
    {
        foreach (ProgressBarPin pin in GetComponentsInChildren<ProgressBarPin>())
        {
            if (pin.ProgressReached(progressInPercent))
            {
                pin.guiTexture.texture = pinHigh;
            }
        }
    }

    void Start()
    {
        foreach (ProgressBarPin pin in GetComponentsInChildren<ProgressBarPin>())
        {
            pin.guiTexture.texture = pinLow;
        }

        //Fake percent loaded :P
        int randomPercent = Random.Range(0, 100);
        SetProgress(randomPercent);
	}
}
