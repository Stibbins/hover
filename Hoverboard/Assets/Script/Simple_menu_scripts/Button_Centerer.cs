/*
Det här skriptet centrerar en knapp om den är vald
 */

using UnityEngine;
using System.Collections;

public class Button_Centerer : MonoBehaviour {
	public UIDraggablePanel parentPanel; //Håller koll på vilken panel knappe tillhör. Sätt detta i editorn.
	public UIDraggablePanel MasterPanel;

	void OnHover()
	{
		Vector3 newPos = parentPanel.transform.worldToLocalMatrix.MultiplyPoint3x4(transform.position);
		SpringPanel.Begin (parentPanel.gameObject, new Vector3(parentPanel.transform.localPosition.x,-newPos.y), 8f); //SpringPanel.Begin startar en process där panelen ifråga "dras" till position

		//Eftersom alla rörelse i x-led ska ske på Master-panelen, och inte på någon av "undermeny-panelerna" så ändras bara positionen i y-led i denna kod. Panel_switcher och Panel_return_switcher
		//Sköter rörelse i x-led.
	}
}



