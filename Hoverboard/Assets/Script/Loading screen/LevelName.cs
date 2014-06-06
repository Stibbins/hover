/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * 
 */

using UnityEngine;
using System.Collections;

public class LevelName : MonoBehaviour {

    void Start()
    {
        
    }

    public void SetName(string name)
    {
        float scale = Screen.height / 1080.0f;
        guiText.fontSize = (int)(guiText.fontSize * scale);

        guiText.text = name.ToUpper();
    }
}
