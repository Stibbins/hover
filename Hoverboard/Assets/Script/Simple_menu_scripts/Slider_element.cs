using UnityEngine;
using System.Collections;

public class Slider_element : MonoBehaviour {
	public int m_ID; //Håller koll vilket element det är. Sätt detta i editorn.
	public GameObject Parent; //Håller koll på vilken slider den tillhör. Sätt detta i editorn.
	public bool active; //Håller endast koll på om den ska "färgas" eller inte. (I framtiden ska vi nog göra den ljusare istället för att färga den)

	void Update()
	{
		if(active) //Om aktiv
		{
			gameObject.GetComponentInChildren<UISprite>().color = Color.green; //Färga din sprite grön.
		}
	}

	void OnHover () //När du är markerad
	{
		Parent.GetComponent<Advanced_Slider>().Selected = m_ID; //Skicka ditt id till slider-objektet så att den vet att du är vald just nu.
	}
}
