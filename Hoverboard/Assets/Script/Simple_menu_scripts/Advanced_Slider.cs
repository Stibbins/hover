/*
Advanced_Slider är koden till "plupp-slidern". Den här koden bestämmer hur en slider fungerar, den har koll på alla Element ("pluppar") genom en array.
 */

using UnityEngine;
using System.Collections;

public class Advanced_Slider : MonoBehaviour {

	bool[] Slider_Elements = new bool[10]; // Slider_Elements är en array som håller (upp till) 10 bools (Sant, eller Falskt). Dessa kopplas senare till de faktiska objekten i spelet.

	Component[] SliderElements;
	public int Selected; //Håller koll på vilket element som är aktiverat (valt).



	BoxCollider2D newCollisionBox;

	void Start () {



	}
	
	// Update is called once per frame
	void Update () {

			int Length = Slider_Elements.Length;
			for(int i= 0; i <= Selected; i++) //Alla element som är, eller kommer innan Selected
			{
				Slider_Elements[i] = true;
			}
			for(int i=Selected+1; i < Length; i++) //Övriga..
			{
				Slider_Elements[i] = false;
			}

			SliderElements = gameObject.GetComponentsInChildren<Slider_element>(); //Hämtar alla element objekt
			foreach(Slider_element child in SliderElements) //Hämtar alla Element som tillhör slidern.
			{
				child.active = Slider_Elements[child.m_ID];	//Sätter varje childs active-variabel till vad det ska vara. (child med m_ID 1, får samma "active" värde som Slider_Element[1], t.ex.)
			}

			AudioListener.volume = (float)Selected/10; //Ändrar den globala volymen beroende på vilket element som är valt.
	}
}


/*varje element har ett ID (publik variabel). Och en bool, Active.
	
	Slidern håller i en array.
		
		När ett element är "hovrat" så skickar detta sitt id till Slidern
		Slidern sätter vilken state alla element är i.
		- Om Element[n] är hovrat så ska alla Element[x] där x < n vara aktiva. Alla Element[x] där x > n ska vara inaktiva.

		iterera alla childs
		jämför child ID med Selected?
		Profit????
 		*/