using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* --------------------------------------------------------------------------------------------------------------------------
 * ERSTELLT VON:
 * Mojdeh Aliakbarzadeh
 * --------------------------------------------------------------------------------------------------------------------------
 * BESCHREIBUNG:
 * - Skript für die Erhöhung mit Raycast
 * --------------------------------------------------------------------------------------------------------------------------
*/


public class HUD_Raise_Plus : MonoBehaviour {

//	public GameObject HUD_Bet;
	public Text NewBet;

	private int bet;
	private int add_bet = 5;
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Raise minus") {
			
			if(int.Parse(NewBet.text) > 0)
				bet -= add_bet;
			
			setBetText ();
		}
	}

	void setBetText(){
		NewBet.text = bet.ToString ();
	}
}
