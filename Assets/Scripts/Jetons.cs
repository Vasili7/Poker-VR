using UnityEngine;

/* --------------------------------------------------------------------------------------------------------------------------
 * ERSTELLT VON:
 * Vasilios Solkidis
 * --------------------------------------------------------------------------------------------------------------------------
 * BESCHREIBUNG:
 * - Dieses Skript initialisiert den Wert jeder erzeugten Jetons
 * - Mögliche Jeton Werte: 1, 5, 25, 100
 * --------------------------------------------------------------------------------------------------------------------------
*/

public class Jetons : MonoBehaviour {

    public int wert;

	// Use this for initialization
	void Start () {
        SetAmountOfJetons();
	}
	
    // Hier wird der Wert des erzeugten Jetons gesetzt
    public void SetAmountOfJetons()
    {
        if(this.name == "v1(Clone)")
        {
            wert = 1;
        }         
        if(this.name == "v5(Clone)")
        {
            wert = 5;
        }
        if(this.name == "v25(Clone)")
        {
            wert = 25;
        } 
        if(this.name == "v100(Clone)")
        {
            wert = 100;
        } 
    }
}
