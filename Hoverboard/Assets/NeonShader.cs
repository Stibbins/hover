using UnityEngine;
using System.Collections;

public class NeonShader : MonoBehaviour {

	private Material material;
	private Material tempMaterial;
	public float Stuttering;
	void Start () {
		material = GetComponent<Renderer>().material;
		tempMaterial = material;
	}
	
	// Update is called once per frame
	void Update () 
	{
		material.color = new Color(material.color.r,material.color.g,material.color.b,Time.deltaTime*Stuttering);
		//material.color.a = 255f;
	}
}
