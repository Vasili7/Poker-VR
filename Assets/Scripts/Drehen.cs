using UnityEngine;
using System.Collections;

/* --------------------------------------------------------------------------------------------------------------------------
 * ERSTELLT VON:
 * Katarina Vrdoljak
 * --------------------------------------------------------------------------------------------------------------------------
 * BESCHREIBUNG:
 * - Rotation für die Würfel, die dem Spieler zeigen, wo es etwas für das Spiel Relevantes zu sehen gibt
 * --------------------------------------------------------------------------------------------------------------------------
*/

public class Drehen : MonoBehaviour {
	private float Geschwindigkeit = 2;
	void Update () {
		transform.Rotate (new Vector3 (0, 45, 0) * Geschwindigkeit * Time.deltaTime);
	}
}
