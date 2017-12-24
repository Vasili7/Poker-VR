using UnityEngine;
using System.Collections;

/* --------------------------------------------------------------------------------------------------------------------------
 * ERSTELLT VON:
 * Katarina Vrdoljak
 * --------------------------------------------------------------------------------------------------------------------------
 * BESCHREIBUNG:
 * - Der Spieler bewegt sich mit gleichbleibender Geschwindigkeit durch den Raum
 * - Kommt er dem "Vier gewinnt"-Spieltisch näher, verringert sich seine Geschwindigkeit, damit in Ruhe eine Spielstärke ausgewählt werden kann
 * --------------------------------------------------------------------------------------------------------------------------
*/

public class Bewegung : MonoBehaviour {

	public GameObject buttonSchwer;
	public GameObject buttonLeicht;
	public GameObject buttonMittel;
	public GameObject buttonJa;
	public GameObject buttonNein;
	public GameObject buttonEnd;
	public GameObject buttonRestart;
	public TextMesh schwierigkeit_txt, werIstDran_txt, siege_txt, niederlage_txt;
	public static float geschwindigkeit = 2.0F;
	public static bool spielstart = false;
	private Vector3 forward;


	void Start () {
		schwierigkeit_txt.text = "";
		werIstDran_txt.text = "";
		siege_txt.text = "";
		niederlage_txt.text = "";
		buttonLeicht.gameObject.SetActive(false);
		buttonMittel.gameObject.SetActive(false);
		buttonSchwer.gameObject.SetActive(false);
		buttonJa.gameObject.SetActive(false);
		buttonNein.gameObject.SetActive(false);
		buttonEnd.gameObject.SetActive(false);
		buttonRestart.gameObject.SetActive(false);
	}
	

	void Update () {
		CharacterController gamer = GetComponent<CharacterController>();
		forward = transform.TransformDirection (Vector3.forward)*geschwindigkeit;
		gamer.SimpleMove (forward);
	}


	void OnTriggerEnter (Collider other) {

		// Geschwindigkeit wird verringert wenn er sich dem Spiel nähert
		if (other.gameObject.CompareTag ("Collider um den Tisch")) {
			if (!spielstart) {
				geschwindigkeit = 0.5F;
				forward.x = 0.0f;
				forward.y = 0.0f;
				forward.z = 0.0f;
				buttonLeicht.SetActive (true);
				buttonMittel.SetActive (true);
				buttonSchwer.SetActive (true);
			}
		}




		// Geschwindigkeit wird erhöht wenn er sich vom Spiel entfernt
		if (other.gameObject.CompareTag ("Collider außerhalb Tisch")  ) {
			geschwindigkeit = 2.0F;
			forward.x = 0.0f;
			forward.y = 0.0f;
			forward.z = 0.0f;
			buttonLeicht.SetActive (false);
			buttonMittel.SetActive (false);
			buttonSchwer.SetActive (false);
		}

	}
}
