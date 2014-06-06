//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Attaching this script to a widget makes it react to key events such as tab, up, down, etc.
/// </summary>

[RequireComponent(typeof(Collider))]
[AddComponentMenu("NGUI/Interaction/Button Keys")]
public class MyButtonKeys : MonoBehaviour
{
	public bool startsSelected = false;
	public MyButtonKeys selectOnClick;
	public MyButtonKeys selectOnUp;
	public MyButtonKeys selectOnDown;
	public MyButtonKeys selectOnLeft;
	public MyButtonKeys selectOnRight;
	
	void OnEnable ()
	{
		if (startsSelected && UICamera.selectedObject == null)
		{
			if (!NGUITools.GetActive(UICamera.selectedObject))
			{
				UICamera.selectedObject = gameObject;
			}
			else
			{
				UICamera.Notify(gameObject, "OnHover", true);
			}
		}
	}
	
	void OnKey (KeyCode key)
	{
		if (enabled && NGUITools.GetActive(gameObject))
		{
			switch (key)
			{
				
			case KeyCode.LeftArrow:
				if (selectOnLeft != null) UICamera.selectedObject = selectOnLeft.gameObject;
				break;
			case KeyCode.RightArrow:
				if (selectOnRight != null) UICamera.selectedObject = selectOnRight.gameObject;
				break;
			case KeyCode.UpArrow:
				if (selectOnUp != null) UICamera.selectedObject = selectOnUp.gameObject;
//				gameObject.GetComponent<Button_Centerer>().moveme = true;
				break;
			case KeyCode.DownArrow:
				if (selectOnDown != null) UICamera.selectedObject = selectOnDown.gameObject;
//				gameObject.GetComponent<Button_Centerer>().moveme = true;
				break;
			case KeyCode.Tab:
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
				{
					if (selectOnLeft != null) UICamera.selectedObject = selectOnLeft.gameObject;
					else if (selectOnUp != null) UICamera.selectedObject = selectOnUp.gameObject;
					else if (selectOnDown != null) UICamera.selectedObject = selectOnDown.gameObject;
					else if (selectOnRight != null) UICamera.selectedObject = selectOnRight.gameObject;
				}
				else
				{
					if (selectOnRight != null) UICamera.selectedObject = selectOnRight.gameObject;
					else if (selectOnDown != null) UICamera.selectedObject = selectOnDown.gameObject;
					else if (selectOnUp != null) UICamera.selectedObject = selectOnUp.gameObject;
					else if (selectOnLeft != null) UICamera.selectedObject = selectOnLeft.gameObject;
				}
				break;
			}
		}
	}
	
	void OnClick ()
	{
		if (enabled && selectOnClick != null)
		{
			UICamera.selectedObject = selectOnClick.gameObject;
		}
	}
}
