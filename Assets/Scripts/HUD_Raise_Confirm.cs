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

public class HUD_Raise_Confirm : MonoBehaviour {

	public Text bank;
	public Text bet;

	private int NewBank;
	void OnTriggerEnter(Collider col){
		if (int.Parse (bank.text) < int.Parse (bet.text)) {

			// bank < bet => can't bet
		} else {

			// bet

	//		NewBank -= int.Parse(bet.text);
	//		bank.text == NewBank.ToString ();
		}
	}
}
