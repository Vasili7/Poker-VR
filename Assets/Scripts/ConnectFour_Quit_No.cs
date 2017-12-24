using UnityEngine;
using System.Collections;

/* --------------------------------------------------------------------------------------------------------------------------
 * ERSTELLT VON:
 * Daniel Greibich
 * --------------------------------------------------------------------------------------------------------------------------
 * BESCHREIBUNG:
 * - Skript für die Spielerentscheidung, das Spiel NICHT zu beenden
 * --------------------------------------------------------------------------------------------------------------------------
*/

public class ConnectFour_Quit_No : MonoBehaviour {

	public GameObject buttonEnd;
	public GameObject buttonEndYes;
	public GameObject buttonEndNo;

	void OnTriggerEnter(Collider col) {
		if(col.gameObject.name == "bone3") {
			Debug.Log ("NO BUTTON PRESSED");
			buttonEnd.SetActive (true);
			buttonEndYes.SetActive (false);
			buttonEndNo.SetActive (false);
		}
	}

}
