/*
 * Created by: Robbin
 * 
 * Description:
 * Put in a child of the hoverboard.
 * If this object collides with for instance a rail, the hoverboard will detect this.
 */

using UnityEngine;
using System.Collections;

public class ColliderObject : MonoBehaviour {

    [SerializeField]
    string type;

    public string m_type
    {
        get { return type; }
    }

    [HideInInspector]
    public ArrayList m_states;

	// Use this for initialization
	void Start () 
    {
        m_states = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    void OnTriggerEnter(Collider col)
    {
        InteractObject script;
        if ((script=col.gameObject.GetComponent<InteractObject>())!=null) //If the script exists in the collided object
        {
            m_states.Add(script.m_type);
        }

    }

    void OnTriggerExit(Collider col)
    {
        InteractObject script;
        if ((script = col.gameObject.GetComponent<InteractObject>()) != null) //If the script exists in the collided object
        {
            m_states.Remove(script.m_type);
        }
    }
}
