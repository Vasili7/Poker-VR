using UnityEngine;
using System.Collections;

/* --------------------------------------------------------------------------------------------------------------------------
 * ERSTELLT VON:
 * Daniel Greibich
 * --------------------------------------------------------------------------------------------------------------------------
 * BESCHREIBUNG:
 * - Skript zum vorzeitigen Beenden des "Vier gewinnt"-Spiels
 * - eine Bestätigung zum Beenden wird im Skript "ConnectFour_Quit_Yes" behandelt
 * --------------------------------------------------------------------------------------------------------------------------
*/

public class ConnectFour_Quit : MonoBehaviour {

	public GameObject buttonEnd;
	public GameObject buttonEndYes;
	public GameObject buttonEndNo;

	void OnTriggerEnter(Collider col) {
		if(col.gameObject.name == "bone3") {
			Debug.Log ("QUIT BUTTON PRESSED");
			buttonEnd.SetActive (false);
			buttonEndYes.SetActive (true);
			buttonEndNo.SetActive (true);
		}
	}

}
