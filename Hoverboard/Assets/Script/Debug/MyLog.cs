/*
 * Created by: Robbin (Source: http://zaxisgames.blogspot.se/2012/03/let-me-debug-in-your-gui.html)
 * Modified by: 
 * 
 * Description:
 * Creates a debug log in game
 */

using UnityEngine;
using System.Collections;

public class MyLog : MonoBehaviour
{
    static string myLog;
    static Queue myLogQueue = new Queue();
    public string output = "";
    public string stack = "";
    private bool hidden = false;
    private Vector2 scrollPos;
    public int maxLines = 30;

    //Show automatically if on xbox one
#if UNITY_XBOXONE
    void Start()
    {
        hidden=false;
    }
#endif

    void OnEnable()
    {
        Application.RegisterLogCallback(HandleLog);
    }

    void OnDisable()
    {
        Application.RegisterLogCallback(null);
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type.ToString() != "Warning")
        {
            output = logString;
            stack = stackTrace;
            string newString = "\n [" + type + "] : " + output;
            myLogQueue.Enqueue(newString);
            if (type == LogType.Exception)
            {
                newString = "\n" + stackTrace;
                myLogQueue.Enqueue(newString);
            }
            while (myLogQueue.Count > maxLines)
            {
                myLogQueue.Dequeue();
            }

            myLog = string.Empty;
            foreach (string s in myLogQueue)
            {
                myLog += s;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            hidden = !hidden;
        }
    }

    void OnGUI()
    {
        if (!hidden)
        {
            GUI.TextArea(new Rect(0, 0, Screen.width / 3, Screen.height), myLog);
            /*if (GUI.Button(new Rect(Screen.width - 100, 10, 80, 20), "Hide"))
            {
                hide(true);
            }*/
        }
        else
        {
            /*if (GUI.Button(new Rect(Screen.width - 100, 10, 80, 20), "Show"))
            {
                hide(false);
            }*/
        }
    }

    public void hide(bool shouldHide)
    {
        hidden = shouldHide;
    }
}