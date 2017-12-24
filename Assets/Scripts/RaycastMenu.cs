using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

/* --------------------------------------------------------------------------------------------------------------------------
 * ERSTELLT VON:
 * Maurice Noll
 * --------------------------------------------------------------------------------------------------------------------------
 * BESCHREIBUNG:
 * - der Spieler befindet sich beim Starten der Applikation vor einem Menü, aus dem er die Steuerungsart wählen kann (Raycasting oder Leap-Motion-Controller)
 * - nach der Auswahl einer Steuerungsart, verschwindet das Menü sowie die Credits auf der linken Seite
 * --------------------------------------------------------------------------------------------------------------------------
*/

public class RaycastMenu : MonoBehaviour {

	public GameObject player;
	public GameObject menu;
	public GameObject richtungsanzeige;
	public TextMesh schwierigkeit_txt, werIstDran_txt, siege_txt, niederlage_txt;
	private PointerEventData pointer;
	private RaycastHit hit;
	private RaycastHit lastHit;
	private float timer = 0f;
	private bool aktiviert = false;

	private List<GameObject> angeseheneObjekte;

	void Start(){
		angeseheneObjekte = new List<GameObject> ();
		pointer = new PointerEventData (EventSystem.current);
		richtungsanzeige.SetActive (false);
		schwierigkeit_txt.text = "";
		werIstDran_txt.text = "";
		siege_txt.text = "";
		niederlage_txt.text = "";
	}

	void Update () {

		Vector3 forward = transform.TransformDirection (Vector3.forward) * 10;
		Debug.DrawRay (transform.position, forward, Color.red);

		if (Physics.Raycast (transform.position, forward, out hit)) {
			if (hit.collider.gameObject.tag == "Menü") {
				aktiviert = true;
				timer = timer + Time.deltaTime;
				this.lastHit = hit;
				angeseheneObjekte.Add (lastHit.transform.gameObject);
				ExecuteEvents.Execute (hit.transform.gameObject, pointer, ExecuteEvents.pointerEnterHandler);

				if (hit.collider.gameObject.name == "Raycasting") {
					if (timer >= 2f) {
						menu.SetActive (false);
						player.GetComponent<RayCast> ().enabled = true;
						player.GetComponent<RaycastMenu> ().enabled = false;
						player.GetComponent<Bewegung> ().enabled = true;
					}
				} else 	if (hit.collider.gameObject.name == "Raycasting mit Anzeige") {
					if (timer >= 2f) {
						menu.SetActive (false);
						richtungsanzeige.SetActive (true);
						player.GetComponent<RayCast> ().enabled = true;
						player.GetComponent<RaycastMenu> ().enabled = false;
						player.GetComponent<Bewegung> ().enabled = true;
					}
				} else if (hit.collider.gameObject.name == "LeapMotion") {
					if (timer >= 2f) {
						menu.SetActive (false);
						player.GetComponent<RayCast> ().enabled = false;
						player.GetComponent<RaycastMenu> ().enabled = false;
						player.GetComponent<Bewegung> ().enabled = true;
					}
				} else if (hit.collider.gameObject.name == "Beenden") {
					if (timer >= 2f) {
						Application.Quit ();
					}
				}
			} else {
				if (aktiviert) {
					for(int i = 0; i < angeseheneObjekte.Count; i++)
						ExecuteEvents.Execute (angeseheneObjekte[i], pointer, ExecuteEvents.pointerExitHandler);
					angeseheneObjekte.Clear ();
					aktiviert = false;
				}
				timer = 0f;
			}
		}
	}
}