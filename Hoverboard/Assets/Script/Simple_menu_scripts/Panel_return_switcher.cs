/*
	Varje gång man "klickar" på en knapp som leder till en tidigare meny ska detta skript köras.
*/
using UnityEngine;
using System.Collections;

public class Panel_return_switcher : MonoBehaviour {
	
	public GameObject currentPanel;
	public GameObject previousPanel;
	public GameObject masterPanel;
	public GameObject previousButton;

	Component[] CollisionBoxes; 
	UIButton[] UIButtons;
	Vector3 Target_pos;
	Vector3 Starting_pos;
	Vector3 Return_pos;

	Vector3 Button_move_distance = new Vector3(150.0f,0.0f,0.0f);

	void OnClick() //.. vid klick
	{

		if(previousButton.GetComponent<Panel_switcher>().Lerp_back == false && previousButton.GetComponent<Panel_switcher>().Lerp == false) //Om förra panelen inte redan håller på med någon animation
		{
			previousButton.GetComponent<Panel_switcher>().Lerp_back = true; //Skicka ett meddelande till förra panelen, Att starta tillbaka-animationen.

			previousButton.GetComponent<Panel_switcher>().Return_pos = masterPanel.transform.localPosition + Button_move_distance; //Ge förra panelen positionen den ska gå tillbaka till.

			TweenAlpha.Begin (previousPanel, 0.1f, 1f); ///Nuvarande panel blir icke genomskinlig

			NGUITools.SetActive (currentPanel, false); //avaktivera den här panelen.


			CollisionBoxes = previousPanel.GetComponentsInChildren<BoxCollider> (); //Hämtar tidigare panelens kollisionsboxar
			foreach(BoxCollider box in CollisionBoxes)
			{
				box.enabled = true; //aktiverar dem.
			}

			UIButtons = previousPanel.GetComponentsInChildren<UIButton> (); //Hämtar knapparna i förra penelen.
			foreach (UIButton button in UIButtons)
			{
				button.enabled = false;
				button.enabled = true; //resettar dem.
				//Det här påverkar endast musstyrningen. Utan den här loopen så kommer menyn ihåg vilken knapp som var markerad på panelen innan du gick in en undermeny.
				//Detta innebär att om du för musen över en annan knapp så lyser bägge upp. Helt irrelevant för tagnentstyrning, dock.
			}

		}
	}
}