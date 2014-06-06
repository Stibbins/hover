using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Created by: Niklas Linder, Erik Åsen
 * Description:
 * Parent class to all keyState-classes. 
 * Edited by: Niklas Linder, Erik Åsen.
 */
public class KeyState
{
	public Vector3 m_keyVector;
	public Vector3 setVector
	{
		set{ m_keyVector = value;}
		get{ return m_keyVector;}
	}
	public Vector3 m_UpjumpVec {
				get; set;
	}
	public Vector3 m_RightjumpVec {
		get; set;
	}
	public virtual void update(){}
	public virtual void start(){}
	public virtual void end(){}
}