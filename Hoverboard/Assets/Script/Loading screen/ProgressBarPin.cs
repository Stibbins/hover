/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Represents a pin in the progress bar
 */

using UnityEngine;
using System.Collections;

public class ProgressBarPin : MonoBehaviour {

    [Range (0, 100)][SerializeField]
    int progressNeeded;

    public bool ProgressReached(int progressNeededInPercent)
    {
        return progressNeededInPercent >= progressNeeded;
    }
}
