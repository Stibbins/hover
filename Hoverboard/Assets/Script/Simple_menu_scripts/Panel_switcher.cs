/*
	Varje gång man "klickar" på en knapp som leder till en ny undermeny så ska den tidigare menyn flyttas undan. Detta visade sig betyda att det behövs mycket kod..
	Experimentera gärna med Button_move_distance. Det finns även en Button_move_distance i Panel_return_switcher.cs
	Du kan ändra konstanten som Time.deltaTime multipliceras med om du vill göra animationen snabbare/långsammare.

*/
using UnityEngine;
using System.Collections;

public class Panel_switcher : MonoBehaviour {

	public GameObject currentPanel;
	public GameObject nextPanel;
	public GameObject masterPanel;
	public Pause_menu pause_menu;


	Component[] CollisionBoxes; 
	Component[] ButtonKeys;
	Vector3 Target_pos;

	float myTime = 0;
	float Lerp_speed = 4f;
	[HideInInspector]
	public Vector3  Return_pos; 
	[HideInInspector]
	public Vector3  Starting_pos; 
	Vector3 Button_move_distance = new Vector3(150.0f,0.0f,0.0f);
	[HideInInspector]
	public bool Lerp = false;
	bool Lerp_done = false;
	bool isClicked = false;
	[HideInInspector]
	public bool Lerp_back = false;
	[HideInInspector]
	public bool Lerp_back_done = false; 


	
	void Update()
	{
		if (Lerp) //Om det är dags att "Lerpa" menypanelen framåt..
		{
			Lerp_done = false;
			myTime += pause_menu.pDeltaTime * Lerp_speed; //"tillbaka-animationen" är snabbare än "framåt-animationen". Jag har ingen aning om varför, men det är därför jag använder Time.deltaTime*6 här.
			//Men bara använder Time.deltaTime i den andra "Lerp if-satsen"
			masterPanel.transform.localPosition = Vector3.Lerp (Starting_pos, Target_pos, myTime);

			if(masterPanel.transform.localPosition == Target_pos) //När panelen är på sin slutposition så resettar vi ett gäng variabler och sätter Lerp_done = true
			{
				Lerp = false;
				Lerp_done = true;
				myTime = 0;
				Starting_pos=masterPanel.transform.localPosition;
			}
		}

		if (Lerp_back) //Samma sak som Lerp, fast baklänges.
		{
			Lerp_back_done = false;
			myTime += pause_menu.pDeltaTime * Lerp_speed;
			masterPanel.transform.localPosition = Vector3.Lerp (Starting_pos, Return_pos, myTime);

			if(masterPanel.transform.localPosition == Return_pos)
			{

				Lerp_back = false;
				Lerp_back_done = true;
				myTime = 0;
			}
		}

		if(Lerp_done) //När framåt-animationen är färdig
		{
			NGUITools.SetActive (nextPanel, true); //nextPanel (sätts i editorn) blir aktiv.
			TweenAlpha.Begin (currentPanel, 0.1f, 0.3f); //Den här panelen blir transparent. Första flyttalet är tiden som det tar för genomskinligheten att bli "färdig" (0.1f bör vara 0.1 sekunder).
			//Det andra talet är hur genomskinlig panelen ska bli. (0.0f 100% genomskinlig 1.0 0% genomskinlig)
			
			CollisionBoxes = currentPanel.GetComponentsInChildren<BoxCollider> (); //Hämtar panelens kollisionsboxar.
			foreach(BoxCollider box in CollisionBoxes)
			{
				box.enabled = false; //.. och avaktiverar dem.
			}
			Lerp_done = false;
		}

		if(Lerp_back_done) //När bakåt-animationen är färdig..
		{
			NGUITools.SetActive (nextPanel, false); //nextPanel blir inaktiv.
			//TweenAlpha.Begin (currentPanel, 0.1f, 1.0f); //Nuvarande panel blir icke genomskinlig
			
			CollisionBoxes = currentPanel.GetComponentsInChildren<BoxCollider> ();
			foreach(BoxCollider box in CollisionBoxes)
			{
				box.enabled = true; //Alla kollisionsboxar aktiveras igen.
			}
			Lerp_back_done = false;
			// Panel_return_Switcher sköter resten av vad som ska ske vid en "tillbaka-lerpning".
		}
	}
	

	void OnClick()
	{
		if(!Lerp && !Lerp_back)
		{
			Starting_pos = masterPanel.transform.localPosition;
			Target_pos = Starting_pos - Button_move_distance;
			Lerp = !Lerp;
		}
	}
}