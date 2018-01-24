using UnityEngine;
using System.Collections;

/* --------------------------------------------------------------------------------------------------------------------------
 * ERSTELLT VON:
 * Daniel Greibich
 * --------------------------------------------------------------------------------------------------------------------------
 * BESCHREIBUNG:
 * - Skript für die Spielerentscheidung das Spiel zu beenden
 * --------------------------------------------------------------------------------------------------------------------------
*/

public class ConnectFour_Quit_Yes : MonoBehaviour {

	public GameObject buttonEnd;
	public GameObject buttonEndYes;
	public GameObject buttonEndNo;
	public GameObject buttonRestart;
	public GameObject Player;
	public TextMesh schwierigkeit_txt, werIstDran_txt, siege_txt, niederlage_txt;


	void OnTriggerEnter(Collider col) {
		if(col.gameObject.name == "bone3") {

//			SpielSteinEinwurfLEAP.siegsteineHervorheben = false;
			GameObject[] spielSteinGelb = GameObject.FindGameObjectsWithTag ("Gelber Spielstein");
			for(int i=0; i < spielSteinGelb.Length; i++) {
				Destroy (spielSteinGelb [i]);
			}
			GameObject[] spielSteinRot = GameObject.FindGameObjectsWithTag ("Roter Spielstein");
			for(int i=0; i < spielSteinRot.Length; i++) {
				Destroy (spielSteinRot [i]);
			}
			schwierigkeit_txt.text = "";
			werIstDran_txt.text = "";
			siege_txt.text = "";
			niederlage_txt.text = "";
			Bewegung.geschwindigkeit = 2;	//Referenziert die Klasse "Bewegung"
			Bewegung.spielstart = false;
//			SpielSteinEinwurfLEAP.neuesSpiel = false;
//			SpielSteinEinwurfLEAP.spielstart = false;
			Player.transform.position = new Vector3 (6F,-0.5F,6F);
			buttonEnd.SetActive (false);
			buttonEndYes.SetActive (false);
			buttonEndNo.SetActive (false);
			buttonRestart.SetActive (false);
			Debug.Log ("YES BUTTON PRESSED");
		}
	}

}
