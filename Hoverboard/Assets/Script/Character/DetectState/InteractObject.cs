/*
 * Created by: Robbin
 * 
 * Description:
 * Used by the hoverboard to specify how to interact with the object.
 * 
 * See DetectState.cs for a list of supported types.
 */

using UnityEngine;
using System.Collections;

public class InteractObject : MonoBehaviour {

    [SerializeField]
    string type;

    public string m_type
    {
        get { return type; }
    }

	// Use this for initialization
	void Start () 
    {
        if (!(gameObject.GetComponent<MeshCollider>() || 
            gameObject.GetComponent<BoxCollider>() || 
            gameObject.GetComponent<CapsuleCollider>() || 
            gameObject.GetComponent<SphereCollider>() || 
            gameObject.GetComponent<WheelCollider>() || 
            gameObject.GetComponent<TerrainCollider>()))
        {
            Debug.LogError("Collider not found in the object "+gameObject.name+"!");
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
