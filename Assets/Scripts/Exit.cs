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

public class Exit : MonoBehaviour
{

    public GameObject mycamera;
    // Application wird beendet sobald der Controller den Ausgang berührt
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera")
            || other.gameObject.name == "Dive_Camera")
        {
            
                Application.Quit();
            
        }
    }
}
