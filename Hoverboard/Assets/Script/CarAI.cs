using UnityEngine;
using System.Collections;

public class CarAI : MonoBehaviour {
	 
	public float m_MaxSpeed;
	public float m_RouteX;
	public float m_RouteZ;



	public bool m_isDrivingZ;
	public bool m_MovingToRight;
	public bool m_MovingFromScreen;
	public bool m_TurningLeft;


	float m_Speed;
	private float positiveRouteX;
	private float positiveRouteZ;
	private float negativeRouteX;
	private float negativeRouteZ;

	void Start () {
		m_Speed = 0;
			
		positiveRouteX = transform.position.x + m_RouteX;
		positiveRouteZ = transform.position.z + m_RouteZ;
		negativeRouteX = transform.position.x - m_RouteX;
		negativeRouteZ = transform.position.z - m_RouteZ;

	}
	
	// Update is called once per frame
	void Update () {
		if (m_Speed < m_MaxSpeed)
			m_Speed += 0.05f;
		else if(m_Speed > m_MaxSpeed)
			m_Speed = m_MaxSpeed;

		if (m_isDrivingZ) 
		{
			if(m_MovingToRight && transform.position.z <= negativeRouteZ)
			{
				if(m_TurningLeft)
					transform.Rotate(0,-90,0);
				else
					transform.Rotate(0,90,0);

				m_isDrivingZ = false;
				m_MovingToRight = false;
			}

			else if(!m_MovingToRight && transform.position.z >= positiveRouteZ)
			{
				if(m_TurningLeft)
					transform.Rotate(0,-90,0);
				else
					transform.Rotate(0,90,0);
				
				m_isDrivingZ = false;
				m_MovingToRight = true;
			}

		}
		else if(!m_isDrivingZ)
		{
			if(m_MovingFromScreen && transform.position.x >= positiveRouteX)
			{
				if(m_TurningLeft)
					transform.Rotate(0,-90,0);
				else
					transform.Rotate(0,90,0);

				m_isDrivingZ = true;
				m_MovingFromScreen = false;
			}
			else if(!m_MovingFromScreen && transform.position.x <= negativeRouteX)
			{
				if(m_TurningLeft)
					transform.Rotate(0,-90,0);
				else
					transform.Rotate(0,90,0);
				
				m_isDrivingZ = true;
				m_MovingFromScreen = true;
			}
		}



		if (m_isDrivingZ) 
		{
			if(m_MovingToRight)
				transform.position -= new Vector3 (0, 0, m_Speed);
			else
				transform.position += new Vector3 (0, 0, m_Speed);
		}
		else
		{
			if(m_MovingFromScreen)
				transform.position += new Vector3 (m_Speed, 0, 0);
			else
				transform.position -= new Vector3 (m_Speed, 0, 0);
		}
		
		
	}
}
