using UnityEngine;
using System.Collections;

/* --------------------------------------------------------------------------------------------------------------------------
 * ERSTELLT VON:
 * Daniel Greibich
 * --------------------------------------------------------------------------------------------------------------------------
 * BESCHREIBUNG:
 * - Skript zum Starten des "Vier gewinnt"-Spiels
 * - das Skript wird ausgeführt, sobald einer der drei zur Verfügung stehenden Spielstärken aktiviert wurde
 * --------------------------------------------------------------------------------------------------------------------------
*/

public class ConnectFour_Start : MonoBehaviour {



	public int schwierigkeit;
	public GameObject buttonEnd;
	public GameObject buttonRestart;
	public GameObject buttonLeicht;
	public GameObject buttonMittel;
	public GameObject buttonSchwer;
	public GameObject Player;
	public TextMesh schwierigkeit_txt;


	void OnTriggerEnter(Collider col) {
		if(col.gameObject.name == "bone3") {
			Debug.Log ("START BUTTON PRESSED");
			switch(schwierigkeit){
			case 1:
				schwierigkeit_txt.text = "Spielstärke: Einfach";
				SpielSteinEinwurfLEAP.schwierigkeit = "Einfach";
				schwierigkeit_txt.transform.localPosition = new Vector3 (16.092f, 0.355f, -9.414f);
				break;
			case 2:
				schwierigkeit_txt.text = "Spielstärke: Mittel";
				SpielSteinEinwurfLEAP.schwierigkeit = "Mittel";
				schwierigkeit_txt.transform.localPosition = new Vector3 (16.092f, 0.355f, -9.414f);
				break;
			case 3:
				schwierigkeit_txt.text = "Spielstärke: Schwer";
				SpielSteinEinwurfLEAP.schwierigkeit = "Schwer";
				schwierigkeit_txt.transform.localPosition = new Vector3 (16.092f, 0.355f, -9.414f);
				break;
			}
			Bewegung.spielstart = true;
			Bewegung.geschwindigkeit = 0;
			Player.transform.position = new Vector3 (15.673f, 0.177f, -7.725f);
			Player.transform.rotation = Quaternion.Euler (15.673f, 0.177f, -7.725f);
			SpielSteinEinwurfLEAP.neuesSpiel = true;
			buttonEnd.SetActive (true);
			buttonRestart.SetActive (true);
			buttonLeicht.SetActive (false);
			buttonMittel.SetActive (false);
			buttonSchwer.SetActive (false);
		}

	}



}
