/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * A base class for trigged objects (bridges that can open etc)
 */

using UnityEngine;
using System.Collections;

public class TriggedObject : MonoBehaviour {

    protected bool isActive = false;
    public bool m_isActive
    {
        get { return isActive; }
    }

    protected virtual void Start(){}
    protected virtual void Update(){}

    public virtual void TriggerEnter() 
    {
        isActive = true;
    }

    public virtual void TriggerExit() 
    {
        isActive = false;
    }

}
