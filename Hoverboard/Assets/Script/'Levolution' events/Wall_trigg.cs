using UnityEngine;
using System.Collections;



public class Wall_trigg : MonoBehaviour {
	public GameObject Hoverboard;
	public GameObject Target_wall;
	public float wall_speed;

	float speed_modifier = 0.1f;

	bool isTriggered = false;


	void OnTriggerEnter(Collider collision)
	{
		isTriggered = true;	
	}

	void Update()
	{
		if(isTriggered == true)
		{
			//Target_wall.transform.Translate(new Vector3(-1,0,0) * wall_speed * speed_modifier);
			Target_wall.rigidbody.AddForce(new Vector3(-1,0,0) * wall_speed * speed_modifier);
		}
	}
}
