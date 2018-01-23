using UnityEngine;
using System.Collections;

/* --------------------------------------------------------------------------------------------------------------------------
 * ERSTELLT VON:
 * Katarina Vrdoljak
 * --------------------------------------------------------------------------------------------------------------------------
 * BESCHREIBUNG:
 * - Beendet die Applikation bei Berührung mit dem Ausgang
 * --------------------------------------------------------------------------------------------------------------------------
*/

public class Exit : MonoBehaviour {

	public GameObject mycamera;
	// Application wird beendet sobald der Controller den Ausgang berührt
	void OnTriggerEnter (Collider other) {
<<<<<<< HEAD
		if (other.gameObject.CompareTag ("MainCamera")
			|| other.gameObject.name == "Dive_Camera") {
=======
		if (other.gameObject.CompareTag ("MainCamera") ||
			other.gameObject.name == "Dive_Camera") {
>>>>>>> 181605d7ad585397ff954eb4e3f9eb9c0752a480
			Application.Quit();
		}
	}
}
