using UnityEngine;
using System.Collections;

public class Destroy_trigg : MonoBehaviour {

	bool isTriggered = false;	
	public GameObject target;
	
	void OnTriggerEnter(Collider collision)
	{
		isTriggered = true;	
	}
	
	void Update()
	{
		if(isTriggered == true)
		{
			Destroy (target);

		}
	}
}