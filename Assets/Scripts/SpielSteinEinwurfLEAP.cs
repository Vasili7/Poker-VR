using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* --------------------------------------------------------------------------------------------------------------------------
 * ERSTELLT VON:
 * Daniel Greibich (1054042)
 * Maurice Noll (1095454)
 * --------------------------------------------------------------------------------------------------------------------------
 * BESCHREIBUNG:
 * - Skript zum Einwerfen der Spielsteine mit den LEAP-Motion-Controller
 * - Jeder Würfel auf dem Tisch besitzt ein Skript und wurde durch öffentliche Variablen am Objekt zugewiesen
 * - Der Spieler kann "Vier gewinnt" in 3 unterschiedlichen Schwierigkeitsgraden spielen
 * - Die Auswahl des Schwierigkeitsgrads erfolgt vor Spielbeginn
 * --------------------------------------------------------------------------------------------------------------------------
*/

public class SpielSteinEinwurfLEAP : MonoBehaviour {

	// Für den reibungslosen Spielablauf sowie für die Spiellogik von "Vier gewinnt" relevante Variablen
	public int columnNumber;
	public TextMesh werIstDran_txt;
	public TextMesh siege_txt;
	public TextMesh niederlage_txt;
	private static int niederlagen = 0;
	private static int siege = 0;
	public GameObject steinSpieler1;						// Stein von Spieler
	public GameObject steinSpieler2;						// Stein von Computer
	public static bool neuesSpiel;
	public static string schwierigkeit;						// Gibt die Schwierigkeit des Computers an
	public static bool spielstart = false;
	private static int zeilen = 6;							// Anzahl der Zeilen des Spielbretts
	private static int spalten = 7;							// Anzahl der Spalten des Spielbretts
	private static bool[] gueltigeSpalten;					// Spalten, in die ein Stein gesetzt werden kann
	private static int[,] feld; 							// Spielfeld / 0 = leer, 1 = blau, 2 = rot
	private static bool zugSpieler;							// Gibt an, welcher Spieler am Zug ist
	private static bool spielende = false;					// Prüft das Spielende
	private static int unentschieden = 0;					// Merker für unentschieden
	private static int sieger = 0;							// Speichert den Sieger / 1 = Spieler, 2 = Computer
	private static int merk_s = 0;							// Merkspalte, in die ein Stein fallen gelassen wird
	private static string guteFelderSpieler = null;			// Auswahl an Spalten, die für den Spieler von Vorteil sind
	private static string guteFelderComputer = null;		// Auswahl an Spalten, die für den Computer von Vorteil sind
	private static string schlechteFelderComputer = null;	// Auswahl an Spalten, die für den Computer von Nachteil sind
	private static int bewertungComputer = 0;				// Bewertung des Computerzugs im hohen Schwierigkeitsgrad
	private static int bewertungSpieler = 0;				// Bewertung des Spielerzugs im hohen Schwierigkeitsgrad

	// Für das Aufleuchten der Steine die zum Sieg führten relevante Variablen
	private static GameObject[,] gesetzteSteine;
	private static List<GameObject> gewonneneSteine;
	public static bool siegsteineHervorheben = false;
	public Material gelb;
	public Material rot;
	public Material blau;
	private bool wechsel = true;
	private float timer = 0f;

    public KartenBewegungZumSpieler kbzs = new KartenBewegungZumSpieler();


	// ##########################################################################################################################
	// ------------------------------------------------------- ZUG SPIELER ------------------------------------------------------
	// ##########################################################################################################################
	// + Überwacht die Handlungen des Spielers (Auswahl des Schwierigkeitsgrads, vorzeitiges Beenden)
	// + Prüft den aktuellen Status und sorgt für den Spielerwechsel
	// --------------------------------------------------------------------------------------------------------------------------
	void OnTriggerEnter(Collider col) {
		if(zugSpieler && gueltigeSpalten[columnNumber - 1] && spielstart && col.gameObject.name == "bone3") {
			Debug.Log ("Einwurf in: " + columnNumber);
			fuegeSteinHinzu (columnNumber - 1);
			if (!spielende) {
				zugSpieler = false;
				werIstDran_txt.text = "Gegenspieler überlegt...";
				werIstDran_txt.transform.localPosition = new Vector3 (-1.25f, 5.35f, 8f);
				StartCoroutine ("Warten");
			}
		}
	}



	// ##########################################################################################################################
	// ---------------------------------------------------------- START ---------------------------------------------------------
	// ##########################################################################################################################
	// + Allen Arrays und Variablen werden die Werte zugewiesen, um das Spiel zu starten
	// --------------------------------------------------------------------------------------------------------------------------
	void Update () {

		if (neuesSpiel) {

            /*
            kbzs.BewegeKarten();
            kbzs.fuegeJetonsHinzu();
            */

			neuesSpiel = false;
			spielende = false;
			spielstart = true;
			sieger = 0;
			unentschieden = 0;

			siege_txt.text = "Siege: " + siege;
			niederlage_txt.text = "Niederlagen: " + niederlagen;

			// Spielfeld auf Werkeinstellungen zurücksetzen
			feld = new int[zeilen, spalten];
			gesetzteSteine = new GameObject[zeilen, spalten];
			gewonneneSteine = new List<GameObject> ();
			for (int zeile = 0; zeile < zeilen; zeile++) {
				for (int spalte = 0; spalte < spalten; spalte++) {
					feld [zeile, spalte] = 0;
					gesetzteSteine [zeile, spalte] = null;
				}
			}

			gueltigeSpalten = new bool[spalten];
			getGueltigeSpalten ();

			int beginner = Random.Range (0, 2);
			if (beginner == 0) {
				zugSpieler = true;
				werIstDran_txt.text = "Sie sind dran!";
				werIstDran_txt.transform.localPosition = new Vector3 (-2.2f, 5.35f, 8f);
				print ("Start Spieler");
			} else {
				zugSpieler = false;
				print ("Start Computer");
				werIstDran_txt.text = "Gegenspieler überlegt...";
				werIstDran_txt.transform.localPosition = new Vector3 (-1.25f, 5.35f, 8f);
				StartCoroutine ("Warten");
			}
		}


		if (siegsteineHervorheben) {
			timer = timer + Time.deltaTime;
			if (timer >= 0.5f) {
				if (sieger == 1) {
					if (wechsel) {
						for (int i = 0; i < gewonneneSteine.Count; i++) {
							Renderer r = gewonneneSteine [i].GetComponent<Renderer> ();
							r.material = blau;
						}
					} else {
						for (int i = 0; i < gewonneneSteine.Count; i++) {
							Renderer r = gewonneneSteine [i].GetComponent<Renderer> ();
							r.material = gelb;
						}
					}
				} else {
					if (wechsel) {
						for (int i = 0; i < gewonneneSteine.Count; i++) {
							Renderer r = gewonneneSteine [i].GetComponent<Renderer> ();
							r.material = blau;
						}
					} else {
						for (int i = 0; i < gewonneneSteine.Count; i++) {
							Renderer r = gewonneneSteine [i].GetComponent<Renderer> ();
							r.material = rot;
						}
					}
				}
				wechsel = !wechsel;
				timer = 0f;
			}
		}
	}




	// ##########################################################################################################################
	// -------------------------------------------------------- WARTEZEIT -------------------------------------------------------
	// ##########################################################################################################################
	// + Wartet eine bestimmte Zeit, bis der Zug des Computers ausgeführt wird
	// --------------------------------------------------------------------------------------------------------------------------
	IEnumerator Warten() {
		yield return new WaitForSeconds(2f);
		gegner ();
	}



	// ##########################################################################################################################
	// ------------------------------------------------------ ZUG COMPUTER ------------------------------------------------------
	// ##########################################################################################################################
	// + Computer führt einen Zug basierend auf dem zuvor gewählten Schwierigkeitsgrad aus
	// --------------------------------------------------------------------------------------------------------------------------
	public void gegner(){
		switch (schwierigkeit) {
		case "Einfach":
			einfach ();
			break;
		case "Mittel":
			mittel ();
			break;
		case "Schwer":
			schwierig ();
			break;
		}
		if (!spielende) {
			zugSpieler = true;
			werIstDran_txt.text = "Sie sind dran!";
			werIstDran_txt.transform.localPosition = new Vector3 (-2.2f, 5.35f, 8f);
		}
	}



	// ##########################################################################################################################
	// ----------------------------------------------- SCHWIERIGKEITSGRAD EINFACH -----------------------------------------------
	// ##########################################################################################################################
	// + Computer setzt einen Stein in eine zufällig ausgewählte Spalte
	// --------------------------------------------------------------------------------------------------------------------------
	public void einfach(){

		// Prüft Gewinnmöglichkeit des Computers
		pruefeSiegNaechstenZug (2);
		if (sieger == 2) {
			fuegeSteinHinzu (merk_s);
			spielende = true;
			siegsteineHervorheben = true;
			print ("Spieler 2 hat gewonnen!");
			niederlagen++;
			niederlage_txt.text = "Niederlagen: " + niederlagen;
			werIstDran_txt.text = "Computer siegt!";
			werIstDran_txt.transform.localPosition = new Vector3 (-1.95f, 5.35f, 8f);
			return;
		} else {
			gewonneneSteine.Clear ();
		}

		int zufallsSpalte = Random.Range(0, 6);
		while (gueltigeSpalten [zufallsSpalte] == false) {
			zufallsSpalte = Random.Range (0, 6);
		}
		fuegeSteinHinzu (zufallsSpalte);
	}



	// ##########################################################################################################################
	// ----------------------------------------------- SCHWIERIGKEITSGRAD MITTEL ------------------------------------------------
	// ##########################################################################################################################
	// + Prüfung auf Gewinnmöglichkeiten beider Spieler (vorzeitiger Abbruch falls vorhanden)
	// + Computer setzt zufällig einen Stein mit Präferenz zur Mitte
	// + Felder, die durch einen Stein des Computers zum Sieg des Spielers führen, werden ignoriert
	// --------------------------------------------------------------------------------------------------------------------------
	public void mittel(){

		// Überprüft Gewinnmöglichkeiten beider Spieler und bricht gegebenenfalls die Methode vorzeitig ab
		bool vorzeitigerAbbruch = gewinnmoeglichkeitenPruefen ();
		if (vorzeitigerAbbruch) return;

		// Wählt eine zufällige Spalte aus mit Präferenz in die Mitte
		naechsterSpielzug();
		if (guteFelderComputer != null) {
			int zufall = Random.Range(1, guteFelderComputer.Length / 2);
			int z = int.Parse(guteFelderComputer.Substring (zufall * 2 - 2, 1));
			int s = int.Parse(guteFelderComputer.Substring (zufall * 2 - 1, 1));
			fuegeSteinHinzu (s);
		} else {
			fuegeSteinHinzu(int.Parse (schlechteFelderComputer.Substring (2, 1)));
			return;
		}
	}



	// ##########################################################################################################################
	// ---------------------------------------------- SCHWIERIGKEITSGRAD SCHWIERIG ----------------------------------------------
	// ##########################################################################################################################
	// + Prüfung auf Gewinnmöglichkeiten beider Spieler (vorzeitiger Abbruch falls vorhanden)
	// + Computer simuliert die nächsten Züge (seinen Zug und den des Spielers) und setzt entsprechend eines Bewertungssystems
	// 		- Jeder mögliche nächste Zug des Computers wird gewichtet (7 Zugmöglichkeiten)
	// 		- Jeder mögliche nächste Zug des Spielers wird gewichtet (7 Zugmöglichkeiten)
	// 		- Der Zug mit der höchsten Gewichtung wird ausgeführt
	// 		- Hat der Zug des Spielers die gleiche Gewichtung wie die des Computers, wird der Spielerzug ausgeführt
	// 		- Gibt es mehrere gleichrangige Züge, wird zufällig einer von ihnen ausgewählt
	// 		- Schlechte Züge, die zum Verlust des Computers führen, werden ausgeführt, wenn keine andere Möglichkeit besteht
	// + Felder, die durch einen Stein des Computers zum Sieg des Spielers führen, werden ignoriert
	// --------------------------------------------------------------------------------------------------------------------------
	public void schwierig(){

		// Überprüft Gewinnmöglichkeiten beider Spieler und bricht gegebenenfalls die Methode vorzeitig ab
		bool vorzeitigerAbbruch = gewinnmoeglichkeitenPruefen ();
		if (vorzeitigerAbbruch) return;

		int besteBewertungSpieler = zuegeBewertenSpieler ();
		int besteBewertungComputer = zuegeBewertenComputer ();

		if (besteBewertungSpieler > 0 && besteBewertungSpieler >= besteBewertungComputer)
			guteFelderComputer = guteFelderSpieler;

		if (guteFelderComputer != null){
			int zufall = Random.Range(1, guteFelderComputer.Length / 2);
			int z = int.Parse(guteFelderComputer.Substring (zufall * 2 - 2, 1));
			int s = int.Parse(guteFelderComputer.Substring (zufall * 2 - 1, 1));
			fuegeSteinHinzu (s);
		}else{
			fuegeSteinHinzu(int.Parse (schlechteFelderComputer.Substring (2, 1)));
			return;
		}

	}



	// ##########################################################################################################################
	// ----------------------------------------------- GEWINNMÖGLICHKEITEN PRÜFEN -----------------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: MITTEL, SCHWIERIG
	// + Computer setzt seinen ersten Zug immer in die Mitte oder auf eines der angerenzenden Felder
	// + Prüft eigenen Sieg im nächsten Zug und setzt entsprechenden Stein
	// + Anschließend wird auf Sieg des Gegners im nächsten Zug geprüft und dieser verhindert
	// --------------------------------------------------------------------------------------------------------------------------
	public bool gewinnmoeglichkeitenPruefen(){

		// Erster Spielzug des Computers ist entweder in der Mitte oder ein Feld daneben
		if (feld[0, 3] == 0) {
			fuegeSteinHinzu (3);
			return true;
		}
		if (feld[0,3] == 1 && feld[1,3] == 0) {
			fuegeSteinHinzu (3);
			return true;
		}

		// Prüft Gewinnmöglichkeit des Computers
		pruefeSiegNaechstenZug (2);
		if (sieger == 2) {
			fuegeSteinHinzu (merk_s);
			spielende = true;
			siegsteineHervorheben = true;
			print("Spieler 2 hat gewonnen!");
			niederlagen++;
			niederlage_txt.text = "Niederlagen: " + niederlagen;
			werIstDran_txt.text = "Computer siegt!";
			werIstDran_txt.transform.localPosition = new Vector3 (-1.95f, 5.35f, 8f);
			return true;
		}
		gewonneneSteine.Clear ();

		// Prüft Gewinnmöglichkeit des Spielers um sie zu verhindern
		pruefeSiegNaechstenZug (1);
		if (sieger == 1) {
			fuegeSteinHinzu (merk_s);
			return true;
		}
		return false;
	}



	// ##########################################################################################################################
	// ------------------------------------------- GUTE / SCHLECHTE SPIELZÜGE SUCHEN --------------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: MITTEL, SCHWIERIG
	// + Prüft für den Computer gute und schlechte Spielzüge
	// + Simuliert den nächsten Spielzug
	// 		- Setzt der Reihe nach in jede nicht volle Spalte einen Stein des Computers
	// 		- Geht anschließend alle Möglichkeiten für den Spieler durch und prüft, ob dieser gewinnen kann
	// 		- Kann der Spieler gewinnen, ist es ein schlechter Zug für den Computer, anderenfalls gut
	// --------------------------------------------------------------------------------------------------------------------------
	public void naechsterSpielzug(){
		sieger = 0;
		guteFelderComputer = null;
		schlechteFelderComputer = null;
		for (int s = 0; s < spalten; s++) {
			if (gueltigeSpalten [s]) {
				for (int z = 0; z < zeilen; z++) {
					if (feld [z, s] == 0) {
						feld [z, s] = 2;
						pruefeSiegNaechstenZug (1);
						if (sieger == 1) {
							schlechteFelderComputer = schlechteFelderComputer + z + s;
							feld [z, s] = 0;
							break;
						}
						guteFelderComputer = guteFelderComputer + z + s;
						if (s >= 2 && s <= 5) guteFelderComputer = guteFelderComputer + z + s + z + s;
						feld [z, s] = 0;
						break;
					}
				}
			}
		}
	}



	// ##########################################################################################################################
	// -------------------------------------------------- SIEG IM NÄCHSTEN ZUG --------------------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: MITTEL, SCHWIERIG
	// + Prüfung, ob ein bestimmter Spieler im nächsten Zug gewinnen kann
	// --------------------------------------------------------------------------------------------------------------------------
	public void pruefeSiegNaechstenZug(int x){
		sieger = 0;
		for (int s = 0; s < spalten; s++) {
			if (gueltigeSpalten [s]) {
				for (int z = 0; z < zeilen; z++) {
					if (feld [z, s] == 0) {
						feld [z, s] = x;
						pruefeSpielende ();
						gewonneneSteine.Clear ();
						feld [z, s] = 0;
						merk_s = s;
						if (sieger > 0)
							return;
						break;
					}
				}
			}
		}
	}



	// ##########################################################################################################################
	// -------------------------------------------------- SPIELERZÜGE BEWERTEN --------------------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: SCHWIERIG
	// + Computer simuliert den nächsten Zug des Spielers
	// + Alle Züge (7 Möglichkeiten) werden der Reihe nach gewichtet
	// + Nur die möglichen Züge mit den höchsten Bewertungen sind von Bedeutung
	// --------------------------------------------------------------------------------------------------------------------------
	public int zuegeBewertenSpieler(){
		sieger = 0;
		guteFelderSpieler = null;
		int besteBewertung = 0;

		for (int s = 0; s < 7; s++){
			if (feld[5, s] == 0){
				for (int z = 0; z < 6; z++){
					if (feld[z, s] == 0){

						feld [z, s] = 3;
						bewertungSpieler = 0;
						getDiagonalRechts (z, s, 1);
						getDiagonalLinks (z, s, 1);
						getWaagrecht (z, s, 1);
						getSenkrecht (z, s, 1);
						pruefeSiegNaechstenZug (2);
						feld[z, s] = 0;

						if (sieger == 0 && bewertungSpieler > 0){

							feld [z, s] = 3;
							pruefeSiegNaechstenZug (1);
							feld [z, s] = 0;

							bewertungSpieler = bewertungSpieler * 2;
							if (sieger == 0){
								if (bewertungSpieler == besteBewertung)
									guteFelderSpieler = guteFelderSpieler + z.ToString () + s.ToString ();

								if (bewertungSpieler > besteBewertung){
									guteFelderSpieler = z.ToString () + s.ToString ();
									besteBewertung = bewertungSpieler;
								}
							}
						}
						break;
					}
				}
			}
		}
		return besteBewertung;
	}



	// ##########################################################################################################################
	// -------------------------------------------------- COMPUTERZÜGE BEWERTEN -------------------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: SCHWIERIG
	// + Computer simuliert seinen nächsten Zug
	// + Alle Züge (7 Möglichkeiten) werden der Reihe nach gewichtet
	// + Nur die möglichen Züge mit den höchsten Bewertungen sind von Bedeutung
	// --------------------------------------------------------------------------------------------------------------------------
	public int zuegeBewertenComputer(){
		sieger = 0;
		guteFelderComputer = null;
		schlechteFelderComputer = null;
		int besteBewertung = 0;

		for (int s = 0; s < 7; s++){
			if (feld[5, s] == 0){
				for (int z = 0; z < 6; z++){
					if (feld[z, s] == 0){
						feld [z, s] = 2;
						pruefeSiegNaechstenZug (1);
						if (sieger == 1) {
							schlechteFelderComputer = schlechteFelderComputer + z.ToString () + s.ToString ();
							feld [z, s] = 0;
							break;
						}

						feld[z, s] = 3;
						bewertungComputer = 0;
						getDiagonalRechts (z, s, 2);
						getDiagonalLinks (z, s, 2);
						getWaagrecht (z, s, 2);
						getSenkrecht (z, s, 2);
						feld[z, s] = 0;

						if (bewertungComputer == besteBewertung)
							guteFelderComputer = guteFelderComputer + z.ToString () + s.ToString ();

						if (bewertungComputer > besteBewertung){
							guteFelderComputer = z.ToString () + s.ToString ();
							besteBewertung = bewertungComputer;
						}
						break;
					}
				}
			}
		}
		return besteBewertung;
	}



	// ##########################################################################################################################
	// ---------------------------------------- LISTE MIT BETROFFENEN FELDERN DURCHSUCHEN ---------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: SCHWIERIG
	// + Erstellt Zeichenketten für diagonal, waagrecht und senkrecht
	// + Die Zeichenketten enthalten die Koordinaten aller Punkte, die für den gesetzten Stein von Bedeutung sein können
	// --------------------------------------------------------------------------------------------------------------------------
	public void getDiagonalRechts(int z, int s, int spieler){
		int laufZeile;
		int laufSpalte;

		if (z < s){
			laufZeile = 0;
			laufSpalte = s - z;
		}else{
			laufZeile = z - s;
			laufSpalte = 0;
		}

		string felder = null;
		while (laufZeile < 6 && laufSpalte < 7) {
			felder = felder + feld [laufZeile, laufSpalte];
			laufZeile++;
			laufSpalte++;
		}

		if (felder.Length >= 4) {
			bewerten(felder, 3, spieler);
		}

	}

	public void getDiagonalLinks(int z, int s, int spieler){
		int laufZeile;
		int laufSpalte;

		if (z + s > 4){
			laufZeile = 5;
			laufSpalte = z + s - 5;
		}else{
			laufZeile = z + s;
			laufSpalte = 0;
		}

		string felder = null;
		while (laufZeile >= 0 && laufSpalte < 7) {
			felder = felder + feld [laufZeile, laufSpalte];
			laufZeile--;
			laufSpalte++;
		}

		if (felder.Length >= 4) {
			bewerten(felder, 3, spieler);
		}
	}

	public void getWaagrecht(int z, int s, int spieler){
		string felder = null;
		for(int laufSpalte = 0; laufSpalte < 7; laufSpalte++){
			felder = felder + feld [z, laufSpalte];
		}
		bewerten (felder, 2, spieler);
	}

	public void getSenkrecht(int z, int s, int spieler){
		string felder = null;
		for(int laufZeile = 0; laufZeile < 6; laufZeile++){
			felder = felder + feld [laufZeile, s];
		}
		bewerten (felder, 1, spieler);
	}



	// ##########################################################################################################################
	// ------------------------------------------------------ ZUG BEWERTEN ------------------------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: SCHWIERIG
	// + Die übergebene Zeichenketten mit den Koordinaten wird durchsucht
	// + Dabei sind alle möglichen Kombinationen zur Anordnung der Spielsteine relevant
	//		- 2 Steine in einer Reihe mit 2 leeren Feldern dazwischen	-> 	1 Punkt
	//		- 2 Steine in einer Reihe mit 1 leeren Feld dazwischen 		-> 	2 Punkte
	//		- 2 Steine in einer Reihe mit 0 leeren Feldern dazwischen 	-> 	3 Punkte
	//		- 3 Steine in einer Reihe mit 1 leeren Feld dazwischen 		-> 	4 Punkte
	//		- 3 Steine in einer Reihe mit 0 leeren Feldern dazwischen 	-> 	5 Punkte
	// + Treffen verschiedene Kombinationen zu, werden diese aufaddiert
	// + Der verwendete Faktor "Typ" gibt die Art und somit auch die Wichtigkeit der zu bildenden Reihe an
	// 		- 1 = Senkrecht 	(niedrigste Wertung -> 	am einfachsten vom Gegner zu verhindern)
	// 		- 2 = Waagrecht 	(mittlere Wertung	->	es können Felder erstellt werden, auf die der Gegner nicht setzen darf)
	// 		- 3 = Diagonal 		(höchste Wertung	-> 	sind am schwierigsten zu verhindern)
	// --------------------------------------------------------------------------------------------------------------------------
	public void bewerten(string felder, int typ, int spieler){
		string[] kombinationen;
		if (spieler == 1) {
			kombinationen = new string[] {"1003", "3001", "0103", "0301", "3010", "1030", "1300", "3100", "0310", "0130", "0031", "0013",
				"3101", "1301", "1103", "3011", "1031", "1013", "3110", "0311", "1310", "1130", "0131", "0113"};
		}else{
			kombinationen = new string[] {"2003", "3002", "0203", "0302", "3020", "2030", "2300", "3200", "0320", "0230", "0032", "0023",
				"3202", "2302", "2203", "3022", "2032", "2023", "3220", "0322", "2320", "2230", "0232", "0223"};
		}

		int summe = 0;
		for (int i = 0; i < 24; i++){
			int position = felder.IndexOf (kombinationen [i]);
			if (position >= 0){
				if (i == 0 || i == 1){
					summe = summe + 1;
				}else if(i > 1 && i < 6){
					summe = summe + 2;
				}else if(i >= 6 && i < 12){
					summe = summe + 3;
				}else if(i >= 12 && i < 18){
					summe = summe + 4;
				}else if(i >= 18 && i < 24){
					summe = summe + 5;
				}
			}
		}

		if (spieler == 1)
			bewertungSpieler = summe * typ + bewertungSpieler;
		else
			bewertungComputer = summe * typ + bewertungComputer;
	}



	// ##########################################################################################################################
	// ---------------------------------------------------- STEIN HINZUFÜGEN ----------------------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: EINFACH, MITTEL, SCHWIERIG
	// + Erstellt ein Spielstein-Objekt auf dem Spielfeld in der entsprechenden Spalte
	// + Aktualisiert das Array des Spielfeldes
	// + Prüft, ob ein Spieler gewonnen hat
	// + Wechselt den Spieler
	// + Sucht alle nicht vollen Spalten
	// --------------------------------------------------------------------------------------------------------------------------
	public void fuegeSteinHinzu(int spalte){

		// Fügt den Stein auf das Spielbrett hinzu
		GameObject stein;
		if (zugSpieler) {
			stein = Instantiate (steinSpieler1, new Vector3 (20.6f, 0.71f, -3.925f - (spalte) * 0.175f), Quaternion.Euler(0,180,90)) as GameObject;
		} else {
			stein = Instantiate (steinSpieler2, new Vector3 (20.6f, 0.71f, -3.925f - (spalte) * 0.175f), Quaternion.Euler(0,180,90)) as GameObject;
		}
			

		// Fügt den Stein dem Array "Feld" hinzu
		for (int i = 0; i < zeilen; i++) {
			if (feld [i, spalte] == 0) {
				if (zugSpieler) {
					feld [i, spalte] = 1;
				} else {
					feld [i, spalte] = 2;
				}
				gesetzteSteine [i, spalte] = stein;
				break;
			}
		}

		pruefeSpielende ();
		getGueltigeSpalten ();


		// ------------------------------------------------------------
		// Spielende wurde erreicht
		// ------------------------------------------------------------
		if (sieger == 1) {
			spielende = true;
			siegsteineHervorheben = true;
			print ("Spieler 1 hat gewonnen!");
			siege++;
			siege_txt.text = "Siege: " + siege;
			werIstDran_txt.text = "GEWONNEN!";
			werIstDran_txt.transform.localPosition = new Vector3 (-1.8f, 5.35f, 8f);
			zugSpieler = false;
			return;
		}
		if (unentschieden != 0) {
			spielende = true;
			print ("Unentschieden!");
			werIstDran_txt.text = "Unentschieden!";
			werIstDran_txt.transform.localPosition = new Vector3 (-2f, 5.35f, 8f);
			zugSpieler = false;
			return;
		}
	}



	// ##########################################################################################################################
	// ---------------------------------------------------- GÜLTIGE SPALTEN -----------------------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: EINFACH, MITTEL, SCHWIERIG
	// + Überprüft nach jedem Zug, in welche Spalten noch Steine geworfen werden können
	// --------------------------------------------------------------------------------------------------------------------------
	public void getGueltigeSpalten(){
		for (int i = 0; i < spalten; i++) {
			if (feld [zeilen-1, i] == 0){
				gueltigeSpalten [i] = true;		// Gültig, Steine können hinzugefügt werden
			}else{
				gueltigeSpalten [i] = false;	// Spalte ist voll
			}
		}
	}
		


	// ##########################################################################################################################
	// ------------------------------------------------------- SPIELENDE --------------------------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: EINFACH, MITTEL, SCHWIERIG
	// + Überprüft nach jedem Zug, ob sich 4 Steine waagrecht, senkrecht oder diagonal in einer Reihe befinden
	// + Prüft, ob das komplette Spielbrett mit Steinen besetzt ist (unentschieden)
	// --------------------------------------------------------------------------------------------------------------------------
	public void pruefeSpielende() {

		int produkt = 0;
		sieger = 0;

		// Waagrechte Prüfung
		for (int z = 0; z < zeilen; z++) {
			for (int s = 0; s < 4; s++) {
				produkt = feld [z, s] * feld [z, s + 1] * feld [z, s + 2] * feld [z, s + 3];
				if (produkt == 1) sieger = 1;
				if (produkt == 16) sieger = 2;
				if (produkt == 1 || produkt == 16) {
					gewonneneSteine.Add (gesetzteSteine [z, s]);
					gewonneneSteine.Add (gesetzteSteine [z, s + 1]);
					gewonneneSteine.Add (gesetzteSteine [z, s + 2]);
					gewonneneSteine.Add (gesetzteSteine [z, s + 3]);
				}
			}
		}

		// Senkrechte Prüfung
		for (int s = 0; s < spalten; s++) {
			for (int z = 0; z < 3; z++) {
				produkt = feld [z, s] * feld [z + 1, s] * feld [z + 2, s] * feld [z + 3, s];
				if (produkt == 1) sieger = 1;
				if (produkt == 16) sieger = 2;
				if (produkt == 1 || produkt == 16) {
					gewonneneSteine.Add (gesetzteSteine [z, s]);
					gewonneneSteine.Add (gesetzteSteine [z + 1, s]);
					gewonneneSteine.Add (gesetzteSteine [z + 2, s]);
					gewonneneSteine.Add (gesetzteSteine [z + 3, s]);
				}
			}
		}

		// Diagonale Prüfung nach rechts oben
		for (int z = 0; z < 3; z++) {
			for (int s = 0; s < 4; s++) {
				produkt = feld [z, s] * feld [z + 1, s + 1] * feld [z + 2, s + 2] * feld [z + 3, s + 3];
				if (produkt == 1) sieger = 1;
				if (produkt == 16) sieger = 2;
				if (produkt == 1 || produkt == 16) {
					gewonneneSteine.Add (gesetzteSteine [z, s]);
					gewonneneSteine.Add (gesetzteSteine [z + 1, s + 1]);
					gewonneneSteine.Add (gesetzteSteine [z + 2, s + 2]);
					gewonneneSteine.Add (gesetzteSteine [z + 3, s + 3]);
				}
			}
		}

		// Diagonale Prüfung nach links oben
		for (int z = 0; z < 3; z++) {
			for (int s = 3; s < spalten; s++) {
				produkt = feld [z, s] * feld [z + 1, s - 1] * feld [z + 2, s - 2] * feld [z + 3, s - 3];
				if (produkt == 1) sieger = 1;
				if (produkt == 16) sieger = 2;
				if (produkt == 1 || produkt == 16) {
					gewonneneSteine.Add (gesetzteSteine [z, s]);
					gewonneneSteine.Add (gesetzteSteine [z + 1, s - 1]);
					gewonneneSteine.Add (gesetzteSteine [z + 2, s - 2]);
					gewonneneSteine.Add (gesetzteSteine [z + 3, s - 3]);
				}
			}
		}

		// Prüfung der obersten Zeile, ob noch weitere Steine hinzugefügt werden können (unentschieden)
		for (int s = 0; s < spalten; s++) {
			unentschieden = unentschieden * feld [zeilen - 1, s];
		}
	}
}