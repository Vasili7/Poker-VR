using UnityEngine;
using System.Collections;

/* --------------------------------------------------------------------------------------------------------------------------
 * ERSTELLT VON:
 * Maurice Noll
 * --------------------------------------------------------------------------------------------------------------------------
 * BESCHREIBUNG:
 * - Skript für das erneute Starten eines "Vier gewinnt"-Spiels mit derselben Computerstärke
 * --------------------------------------------------------------------------------------------------------------------------
*/

public class ConnectFour_Restart : MonoBehaviour {

	public GameObject Player;

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.name == "bone3") {
			
			// Spielsteine löschen
			GameObject[] spielSteinGelb = GameObject.FindGameObjectsWithTag ("Gelber Spielstein");
			for (int i = 0; i < spielSteinGelb.Length; i++) {
				Destroy (spielSteinGelb [i]);
			}
			GameObject[] spielSteinRot = GameObject.FindGameObjectsWithTag ("Roter Spielstein");
			for (int i = 0; i < spielSteinRot.Length; i++) {
				Destroy (spielSteinRot [i]);
			}

			// Einstellungen für den Spielstart
	//		SpielSteinEinwurfLEAP.siegsteineHervorheben = false;
	//		SpielSteinEinwurfLEAP.neuesSpiel = true;
			Player.transform.position = new Vector3 (15.673f, 0.177f, -7.725f);
		}
	}

}
