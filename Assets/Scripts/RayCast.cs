using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

/* --------------------------------------------------------------------------------------------------------------------------
 * ERSTELLT VON:
 * Imran Mirza
 * Maurice Noll
 * --------------------------------------------------------------------------------------------------------------------------
 * BESCHREIBUNG:
 * - Skript f�r das Raycasting mit allen interagierbaren Elementen
 * - Betrachtete Objekte (Buttons, Spalten von "Vier gewinnt") leuchten bei Betrachtung auf
 * - Der Spieler kann sich die Spielst�rke des Computers vor Spielbeginn und die Spalte f�r den Steineinwurf ausw�hlen
 * - Zudem kann das Spiel auf demselben Schwierigkeitsgrad erneut gespielt oder vorzeitig beendet werden
 * --------------------------------------------------------------------------------------------------------------------------
*/

public class RayCast : MonoBehaviour {

	// F�r das Aufleuchten der Buttons und Spalten durch Betrachten relevante Variablen
	private PointerEventData pointer;
	private RaycastHit lastHit;

	// F�r den reibungslosen Spielablauf sowie f�r die Spiellogik von "Vier gewinnt" relevante Variablen
	public GameObject steinSpieler1, steinSpieler2;
	public GameObject buttonLeicht, buttonMittel, buttonSchwierig;
	public GameObject buttonEnd, buttonEndJa, buttonEndNein;
	public GameObject HUD_Check, HUD_Fold, HUD_Raise, HUD_Raise_plus, HUD_Raise_minus;
	public GameObject buttonRestart;
	public GameObject buttonStart;
	public GameObject Player;
	public GameObject cardOne;
	public GameObject cardTwo;
	public TextMesh schwierigkeit_txt, werIstDran_txt, siege_txt, niederlage_txt;
	public static bool neuesSpiel = false;
	private float timer = 0f;
	private string fokusiertesObjekt = null;
	private bool ausgewaehlt = false;
	private bool spielstart = false;
	private int niederlagen = 0;
	private int siege = 0;
	private int zeilen = 6;								// Anzahl der Zeilen des Spielbretts
	private int spalten = 7;							// Anzahl der Spalten des Spielbretts
	private static bool[] gueltigeSpalten;				// Spalten, in die ein Stein gesetzt werden kann
	private static int[,] feld; 						// Spielfeld / 0 = leer, 1 = blau, 2 = rot
	private bool zugSpieler = false;					// Gibt an, welcher Spieler am Zug ist
	private bool spielende = true;						// Pr�ft das Spielende
	private int unentschieden = 0;						// Merker f�r unentschieden
	private int sieger = 0;								// Speichert den Sieger / 1 = Spieler, 2 = Computer
	private string schwierigkeit;						// Gibt die Schwierigkeit des Computers an
	private int merk_s = 0;								// Merkspalte, in die ein Stein fallen gelassen wird
	private string guteFelderSpieler = null;			// Auswahl an Spalten, die f�r den Spieler von Vorteil sind
	private string guteFelderComputer = null;			// Auswahl an Spalten, die f�r den Computer von Vorteil sind
	private string schlechteFelderComputer = null;		// Auswahl an Spalten, die f�r den Computer von Nachteil sind
	private int bewertungComputer = 0;					// Bewertung des Computerzugs im hohen Schwierigkeitsgrad
	private int bewertungSpieler = 0;					// Bewertung des Spielerzugs im hohen Schwierigkeitsgrad
	private bool aktiviert = false;
	private List<GameObject> angeseheneObjekte;

	// F�r das Aufleuchten der Steine die zum Sieg f�hrten relevante Variablen
	private static GameObject[,] gesetzteSteine;
	private static List<GameObject> gewonneneSteine;
	public static bool siegsteineHervorheben = false;
	public Material gelb;
	public Material rot;
	public Material blau;
	private bool wechsel = true;
	private float timer2 = 0f;

	private int Bet_Change=1;
	public Text Bet;
	private int Set_Bet=0;

    KartenBewegungZumSpieler kbzs = new KartenBewegungZumSpieler();
    public Tisch t = new Tisch();


	void Start(){
		angeseheneObjekte = new List<GameObject> ();
		pointer = new PointerEventData (EventSystem.current);

		HUD_Check.SetActive (false);
		HUD_Fold.SetActive (false);
		HUD_Raise.SetActive (false);
		HUD_Raise_minus.SetActive (false);
		HUD_Raise_plus.SetActive (false);

		buttonEnd.SetActive (false);
		buttonEndJa.SetActive (false);
		buttonEndNein.SetActive (false);
		buttonRestart.SetActive (false);

		buttonStart.SetActive (true);

		Bet.text = Set_Bet.ToString ();
	}


	// ##########################################################################################################################
	// --------------------------------------------------------- UPDATE ---------------------------------------------------------
	// ##########################################################################################################################
	// + Permanente �berwachung, ob ein Button oder eine Spalte von "Vier gewinnt" angesehen wurde, um eine Aktion auszuf�hren
	// --------------------------------------------------------------------------------------------------------------------------
	void Update () {
		RaycastHit hit;
		Vector3 forward = transform.TransformDirection (Vector3.forward) * 20;
		Debug.DrawRay (transform.position, forward, Color.red);

		buttonLeicht.SetActive (false);
		buttonMittel.SetActive (false);
		buttonSchwierig.SetActive (false);

		// ------------------------------------------------------------
		// Spielstart, Spielabbruch, Spielerzug & Gegnerzug
		// ------------------------------------------------------------
		if (Physics.Raycast (transform.position, forward, out hit)) {
			
			// SPIELSTART
			if (hit.collider.gameObject.tag == "Spiel starten") 
            {
				Debug.Log ("Spiel starten!");

				aktiviert = true;

				timer = timer + Time.deltaTime;
				this.lastHit = hit;
				angeseheneObjekte.Add (lastHit.transform.gameObject);
				ExecuteEvents.Execute (hit.transform.gameObject, pointer, ExecuteEvents.pointerEnterHandler);


				if (timer >= 2f) {
					timer = 0f;
					siegsteineHervorheben = false;

					Bewegung.spielstart = true;
					Bewegung.geschwindigkeit = 0;

					// Spielsteine l�schen


					// Einstellungen f�r den Spielstart
					neuesSpiel = true;
					spielende = false;
					Player.transform.position = new Vector3 (15.673f, 0.177f, -7.725f);

					ExecuteEvents.Execute (lastHit.transform.gameObject, pointer, ExecuteEvents.pointerExitHandler);

					HUD_Check.SetActive (true);
					HUD_Fold.SetActive (true);
					HUD_Raise.SetActive (true);
					HUD_Raise_minus.SetActive (true);
					HUD_Raise_plus.SetActive (true);
					buttonStart.SetActive (false);

					buttonEnd.SetActive (true);
					buttonEndJa.SetActive (true);
					buttonEndNein.SetActive (true);
					buttonRestart.SetActive (true);

					t.AddFirstJetons();
					t.StartNewMatch();
					t.DealFlop();
					t.DealTurn();
					t.DealRiver();
				}
			// NEUSTART
			} else if (hit.collider.gameObject.tag == "Neustart") {
				aktiviert = true;

				timer = timer + Time.deltaTime;
				this.lastHit = hit;
				angeseheneObjekte.Add (lastHit.transform.gameObject);
				ExecuteEvents.Execute (hit.transform.gameObject, pointer, ExecuteEvents.pointerEnterHandler);


				if (timer >= 2f) {
					timer = 0f;
					siegsteineHervorheben = false;

					// Spielsteine l�schen
					GameObject[] spielSteinGelb = GameObject.FindGameObjectsWithTag ("Gelber Spielstein");
					for (int i = 0; i < spielSteinGelb.Length; i++) {
						Destroy (spielSteinGelb [i]);
					}
					GameObject[] spielSteinRot = GameObject.FindGameObjectsWithTag ("Roter Spielstein");
					for (int i = 0; i < spielSteinRot.Length; i++) {
						Destroy (spielSteinRot [i]);
					}

					// Einstellungen f�r den Spielstart
					neuesSpiel = true;
					spielende = false;
					Player.transform.position = new Vector3 (15.673f, 0.177f, -7.725f);

					ExecuteEvents.Execute(lastHit.transform.gameObject, pointer, ExecuteEvents.pointerExitHandler);

                    buttonLeicht.SetActive(false); 
                    buttonMittel.SetActive(false); 
                    buttonSchwierig.SetActive(false);

					HUD_Check.SetActive (true);
					HUD_Fold.SetActive (true);
					HUD_Check.SetActive (true);
					HUD_Fold.SetActive (true);
					HUD_Raise.SetActive (true);
					HUD_Raise_minus.SetActive (true);
					HUD_Raise_plus.SetActive (true);
					buttonStart.SetActive (false);

					buttonEnd.SetActive (true);
					buttonEndJa.SetActive (true);
					buttonEndNein.SetActive (true);
					buttonRestart.SetActive (true);


                    t.AddFirstJetons();
                    t.StartNewMatch();
                    t.DealFlop();
                    t.DealTurn();
                    t.DealRiver();
		
				}
					
			// SPIELABBRUCH
			} else if (hit.collider.gameObject.tag == "Spiel abbrechen") {
				aktiviert = true;
				if (hit.collider.gameObject.name == fokusiertesObjekt) {
					timer = timer + Time.deltaTime;
					this.lastHit = hit;
					angeseheneObjekte.Add (lastHit.transform.gameObject);
					ExecuteEvents.Execute(hit.transform.gameObject, pointer, ExecuteEvents.pointerEnterHandler);

					if (timer >= 2f) {
						timer = 0f;

						switch (hit.collider.gameObject.name) {
						case "Spiel abbrechen":
							buttonEnd.SetActive (false);
							buttonRestart.SetActive (false);
							buttonEndJa.SetActive (true);
							buttonEndNein.SetActive (true);
							break;
						case "Spiel abbrechen (JA)":
							siegsteineHervorheben = false;
							Bewegung.spielstart = false;
							Bewegung.geschwindigkeit = 2;

							// Angaben f�r das HUD
							schwierigkeit_txt.text = "";
							werIstDran_txt.text = "";
							siege_txt.text = "";
							niederlage_txt.text = "";

							// Buttons ein- und ausblenden
							buttonStart.SetActive (true);
							buttonEnd.SetActive (false);
							buttonEndJa.SetActive (false);
							buttonEndNein.SetActive (false);
							buttonRestart.SetActive (false);

							// Spielsteine l�schen
							GameObject[] spielSteinGelb = GameObject.FindGameObjectsWithTag ("Gelber Spielstein");
							for (int i = 0; i < spielSteinGelb.Length; i++) {
								Destroy (spielSteinGelb [i]);
							}
							GameObject[] spielSteinRot = GameObject.FindGameObjectsWithTag ("Roter Spielstein");
							for (int i = 0; i < spielSteinRot.Length; i++) {
								Destroy (spielSteinRot [i]);
							}

							// Einstellungen f�r das Spielende
							Player.transform.position = new Vector3 (6F, -0.5F, 6F);
							spielstart = false;
							break;
						case "Spiel abbrechen (NEIN)":
							buttonEnd.SetActive (true);
							buttonEndJa.SetActive (false);
							buttonEndNein.SetActive (false);
							buttonRestart.SetActive (true);
							break;
						}
							
					}
				} else {
					//ExecuteEvents.Execute(lastHit.transform.gameObject, pointer, ExecuteEvents.pointerExitHandler);
					fokusiertesObjekt = hit.collider.gameObject.name;
					timer = 0f;
				}

				//CHECK
			}else if(hit.collider.gameObject.tag=="Check"){
				aktiviert = true;

				timer = timer + Time.deltaTime;
				this.lastHit = hit;
				angeseheneObjekte.Add (lastHit.transform.gameObject);
				ExecuteEvents.Execute (hit.transform.gameObject, pointer, ExecuteEvents.pointerEnterHandler);


				if (timer >= 2f) {
					timer = 0f;

					HUD_Check.SetActive (false);
				}
				//FOLD
			}else if(hit.collider.gameObject.tag=="Fold"){
				aktiviert = true;

				timer = timer + Time.deltaTime;
				this.lastHit = hit;
				angeseheneObjekte.Add (lastHit.transform.gameObject);
				ExecuteEvents.Execute (hit.transform.gameObject, pointer, ExecuteEvents.pointerEnterHandler);


				if (timer >= 2f) {
					timer = 0f;

					HUD_Fold.SetActive (false);
				}

				//RAISE
			}else if(hit.collider.gameObject.tag=="raise"){
				aktiviert = true;

				timer = timer + Time.deltaTime;
				this.lastHit = hit;
				angeseheneObjekte.Add (lastHit.transform.gameObject);
				ExecuteEvents.Execute (hit.transform.gameObject, pointer, ExecuteEvents.pointerEnterHandler);


				if (timer >= 2f) {
					timer = 0f;

					HUD_Raise.SetActive (false);
				}
				//RAISE MINUS
			}else if(hit.collider.gameObject.tag=="Raise minus"){
				aktiviert = true;

				timer = timer + Time.deltaTime;
				this.lastHit = hit;
				angeseheneObjekte.Add (lastHit.transform.gameObject);
				ExecuteEvents.Execute (hit.transform.gameObject, pointer, ExecuteEvents.pointerEnterHandler);


				if (timer >= 2f) {
					timer = 0f;

					if (int.Parse (Bet.text) > 0)
						Set_Bet -= Bet_Change;

					Bet.text = Set_Bet.ToString ();
				}
				//RAISE PLUS
			}else if(hit.collider.gameObject.tag=="Raise plus"){
				aktiviert = true;

				timer = timer + Time.deltaTime;
				this.lastHit = hit;
				angeseheneObjekte.Add (lastHit.transform.gameObject);
				ExecuteEvents.Execute (hit.transform.gameObject, pointer, ExecuteEvents.pointerEnterHandler);


				if (timer >= 2f) {
					timer = 0f;

					Set_Bet += Bet_Change;

					Bet.text = Set_Bet.ToString ();
				}
			// SPIELERZUG
			} else if (hit.collider.gameObject.tag == "Spalte") {
				if (spielstart && !spielende) {
					aktiviert = true;
					this.lastHit = hit;
					angeseheneObjekte.Add (lastHit.transform.gameObject);
					ExecuteEvents.Execute (hit.transform.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
					if (hit.collider.gameObject.name == fokusiertesObjekt) {
						if (zugSpieler) {
							if (ausgewaehlt)
								return;
							timer = timer + Time.deltaTime;
							if (timer >= 2f) {
								ausgewaehlt = true;
								timer = 0f;

								string name = hit.collider.gameObject.name;
								int s = int.Parse (name.Substring (name.Length - 1, 1));
								if (gueltigeSpalten [s - 1]) {
									fuegeSteinHinzu (s - 1);
								} else {
									ausgewaehlt = false;
								}
							}
						}
					} else {
						if (aktiviert) {
							ExecuteEvents.Execute (lastHit.transform.gameObject, pointer, ExecuteEvents.pointerExitHandler);
							aktiviert = false;
						}
						fokusiertesObjekt = hit.collider.gameObject.name;
						timer = 0f;
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
			

		// ------------------------------------------------------------
		// Einstellungen f�r ein neues Spiel
		// ------------------------------------------------------------
		if (neuesSpiel) {
			neuesSpiel = false;
			spielende = false;
			spielstart = true;
			sieger = 0;
			unentschieden = 0;

			// Spielfeld auf Werkeinstellungen zur�cksetzen
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
				print ("Start Spieler");
				werIstDran_txt.text = "Sie sind dran!";
				werIstDran_txt.transform.localPosition = new Vector3 (15.673f, 0.177f, -7.725f);
			} else {
				zugSpieler = false;
				print ("Start Computer");
				werIstDran_txt.text = "Gegenspieler �berlegt...";
				werIstDran_txt.transform.localPosition = new Vector3 (-1.25f, 5.35f, 8f);
			}
				
		}


		// ------------------------------------------------------------
		// Steine, die zum Sieg f�hrten, leuchten auf
		// ------------------------------------------------------------
		if (siegsteineHervorheben) {
			timer2 = timer2 + Time.deltaTime;
			if (timer2 >= 0.5f) {
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
				timer2 = 0f;
			}
		}


		// ------------------------------------------------------------
		// Spielende wurde erreicht
		// ------------------------------------------------------------
		if (spielende)
			return;
		if (sieger == 2) {
			spielende = true;
			siegsteineHervorheben = true;
			print ("Spieler 2 hat gewonnen!");
			niederlagen++;
			niederlage_txt.text = "Niederlagen: " + niederlagen;
			werIstDran_txt.text = "Computer siegt!";
			werIstDran_txt.transform.localPosition = new Vector3 (-1.95f, 5.35f, 8f);
			zugSpieler = false;
			return;
		}
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


		// GEGNERZUG
		if (!zugSpieler && spielstart && !ausgewaehlt && !spielende) {
			ausgewaehlt = true;
			StartCoroutine ("Warten");
		}
	}



	// ##########################################################################################################################
	// -------------------------------------------------------- WARTEZEIT -------------------------------------------------------
	// ##########################################################################################################################
	// + Wartet eine bestimmte Zeit, bis der Zug des Computers ausgef�hrt wird
	// --------------------------------------------------------------------------------------------------------------------------
	IEnumerator Warten() {
		yield return new WaitForSeconds(2f);
		gegner ();
	}



	// ##########################################################################################################################
	// ------------------------------------------------------ ZUG COMPUTER ------------------------------------------------------
	// ##########################################################################################################################
	// + Computer f�hrt einen Zug basierend auf dem zuvor gew�hlten Schwierigkeitsgrad aus
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
		ausgewaehlt = false;
	}



	// ##########################################################################################################################
	// ----------------------------------------------- SCHWIERIGKEITSGRAD EINFACH -----------------------------------------------
	// ##########################################################################################################################
	// + Computer setzt einen Stein in eine zuf�llig ausgew�hlte Spalte
	// --------------------------------------------------------------------------------------------------------------------------
	public void einfach(){
		// Pr�ft Gewinnm�glichkeit des Computers
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
			return;
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
	// + Pr�fung auf Gewinnm�glichkeiten beider Spieler (vorzeitiger Abbruch falls vorhanden)
	// + Computer setzt zuf�llig einen Stein mit Pr�ferenz zur Mitte
	// + Felder, die durch einen Stein des Computers zum Sieg des Spielers f�hren, werden ignoriert
	// --------------------------------------------------------------------------------------------------------------------------
	public void mittel(){

		// �berpr�ft Gewinnm�glichkeiten beider Spieler und bricht gegebenenfalls die Methode vorzeitig ab
		bool vorzeitigerAbbruch = gewinnmoeglichkeitenPruefen ();
		if (vorzeitigerAbbruch) return;

		// W�hlt eine zuf�llige Spalte aus mit Pr�ferenz in die Mitte
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
	// + Pr�fung auf Gewinnm�glichkeiten beider Spieler (vorzeitiger Abbruch falls vorhanden)
	// + Computer simuliert die n�chsten Z�ge (seinen Zug und den des Spielers) und setzt entsprechend eines Bewertungssystems
	// 		- Jeder m�gliche n�chste Zug des Computers wird gewichtet (7 Zugm�glichkeiten)
	// 		- Jeder m�gliche n�chste Zug des Spielers wird gewichtet (7 Zugm�glichkeiten)
	// 		- Der Zug mit der h�chsten Gewichtung wird ausgef�hrt
	// 		- Hat der Zug des Spielers die gleiche Gewichtung wie die des Computers, wird der Spielerzug ausgef�hrt
	// 		- Gibt es mehrere gleichrangige Z�ge, wird zuf�llig einer von ihnen ausgew�hlt
	// 		- Schlechte Z�ge, die zum Verlust des Computers f�hren, werden ausgef�hrt, wenn keine andere M�glichkeit besteht
	// + Felder, die durch einen Stein des Computers zum Sieg des Spielers f�hren, werden ignoriert
	// --------------------------------------------------------------------------------------------------------------------------
	public void schwierig(){

		// �berpr�ft Gewinnm�glichkeiten beider Spieler und bricht gegebenenfalls die Methode vorzeitig ab
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
	// ----------------------------------------------- GEWINNM�GLICHKEITEN PR�FEN -----------------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: MITTEL, SCHWIERIG
	// + Computer setzt seinen ersten Zug immer in die Mitte oder auf eines der angerenzenden Felder
	// + Pr�ft eigenen Sieg im n�chsten Zug und setzt entsprechenden Stein
	// + Anschlie�end wird auf Sieg des Gegners im n�chsten Zug gepr�ft und dieser verhindert
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

		// Pr�ft Gewinnm�glichkeit des Computers
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

		// Pr�ft Gewinnm�glichkeit des Spielers um sie zu verhindern
		pruefeSiegNaechstenZug (1);
		if (sieger == 1) {
			fuegeSteinHinzu (merk_s);
			return true;
		}
		return false;
	}



	// ##########################################################################################################################
	// ------------------------------------------- GUTE / SCHLECHTE SPIELZ�GE SUCHEN --------------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: MITTEL, SCHWIERIG
	// + Pr�ft f�r den Computer gute und schlechte Spielz�ge
	// + Simuliert den n�chsten Spielzug
	// 		- Setzt der Reihe nach in jede nicht volle Spalte einen Stein des Computers
	// 		- Geht anschlie�end alle M�glichkeiten f�r den Spieler durch und pr�ft, ob dieser gewinnen kann
	// 		- Kann der Spieler gewinnen, ist es ein schlechter Zug f�r den Computer, anderenfalls gut
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
	// -------------------------------------------------- SIEG IM N�CHSTEN ZUG --------------------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: MITTEL, SCHWIERIG
	// + Pr�fung, ob ein bestimmter Spieler im n�chsten Zug gewinnen kann
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
	// -------------------------------------------------- SPIELERZ�GE BEWERTEN --------------------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: SCHWIERIG
	// + Computer simuliert den n�chsten Zug des Spielers
	// + Alle Z�ge (7 M�glichkeiten) werden der Reihe nach gewichtet
	// + Nur die m�glichen Z�ge mit den h�chsten Bewertungen sind von Bedeutung
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
	// -------------------------------------------------- COMPUTERZ�GE BEWERTEN -------------------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: SCHWIERIG
	// + Computer simuliert seinen n�chsten Zug
	// + Alle Z�ge (7 M�glichkeiten) werden der Reihe nach gewichtet
	// + Nur die m�glichen Z�ge mit den h�chsten Bewertungen sind von Bedeutung
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
	// + Erstellt Zeichenketten f�r diagonal, waagrecht und senkrecht
	// + Die Zeichenketten enthalten die Koordinaten aller Punkte, die f�r den gesetzten Stein von Bedeutung sein k�nnen
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
	// + Die �bergebene Zeichenketten mit den Koordinaten wird durchsucht
	// + Dabei sind alle m�glichen Kombinationen zur Anordnung der Spielsteine relevant
	//		- 2 Steine in einer Reihe mit 2 leeren Feldern dazwischen	-> 	1 Punkt
	//		- 2 Steine in einer Reihe mit 1 leeren Feld dazwischen 		-> 	2 Punkte
	//		- 2 Steine in einer Reihe mit 0 leeren Feldern dazwischen 	-> 	3 Punkte
	//		- 3 Steine in einer Reihe mit 1 leeren Feld dazwischen 		-> 	4 Punkte
	//		- 3 Steine in einer Reihe mit 0 leeren Feldern dazwischen 	-> 	5 Punkte
	// + Treffen verschiedene Kombinationen zu, werden diese aufaddiert
	// + Der verwendete Faktor "Typ" gibt die Art und somit auch die Wichtigkeit der zu bildenden Reihe an
	// 		- 1 = Senkrecht 	(niedrigste Wertung -> 	am einfachsten vom Gegner zu verhindern)
	// 		- 2 = Waagrecht 	(mittlere Wertung	->	es k�nnen Felder erstellt werden, auf die der Gegner nicht setzen darf)
	// 		- 3 = Diagonal 		(h�chste Wertung	-> 	sind am schwierigsten zu verhindern)
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
	// ---------------------------------------------------- STEIN HINZUF�GEN ----------------------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: EINFACH, MITTEL, SCHWIERIG
	// + Erstellt ein Spielstein-Objekt auf dem Spielfeld in der entsprechenden Spalte
	// + Aktualisiert das Array des Spielfeldes
	// + Pr�ft, ob ein Spieler gewonnen hat
	// + Wechselt den Spieler
	// + Sucht alle nicht vollen Spalten
	// --------------------------------------------------------------------------------------------------------------------------
	public void fuegeSteinHinzu(int spalte){

		// F�gt den Stein auf das Spielbrett hinzu
		GameObject stein;
		if (zugSpieler) {
			stein = Instantiate (steinSpieler1, new Vector3 (20.6f, 0.71f, -3.925f - (spalte) * 0.175f), Quaternion.Euler(0,180,90)) as GameObject;
		} else {
			stein = Instantiate (steinSpieler2, new Vector3 (20.6f, 0.71f, -3.925f - (spalte) * 0.175f), Quaternion.Euler(0,180,90)) as GameObject;
		}


		// F�gt den Stein dem Array "Feld" hinzu
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

		ausgewaehlt = false;
		zugSpieler = !zugSpieler;
		if (zugSpieler) {
			werIstDran_txt.text = "Sie sind dran!";
			werIstDran_txt.transform.localPosition = new Vector3 (-2.2f, 5.35f, 8f);
		} else {
			werIstDran_txt.text = "Gegenspieler �berlegt...";
			werIstDran_txt.transform.localPosition = new Vector3 (-1.25f, 5.35f, 8f);
		}
	}



	// ##########################################################################################################################
	// ---------------------------------------------------- G�LTIGE SPALTEN -----------------------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: EINFACH, MITTEL, SCHWIERIG
	// + �berpr�ft nach jedem Zug, in welche Spalten noch Steine geworfen werden k�nnen
	// --------------------------------------------------------------------------------------------------------------------------
	public void getGueltigeSpalten(){
		for (int i = 0; i < spalten; i++) {
			if (feld [zeilen-1, i] == 0){
				gueltigeSpalten [i] = true;		// G�ltig, Steine k�nnen hinzugef�gt werden
			}else{
				gueltigeSpalten [i] = false;	// Spalte ist voll
			}
		}
	}



	// ##########################################################################################################################
	// ------------------------------------------------------- SPIELENDE --------------------------------------------------------
	// ##########################################################################################################################
	// SCHWIERIGKEITSGRADE: EINFACH, MITTEL, SCHWIERIG
	// + �berpr�ft nach jedem Zug, ob sich 4 Steine waagrecht, senkrecht oder diagonal in einer Reihe befinden
	// + Pr�ft, ob das komplette Spielbrett mit Steinen besetzt ist (unentschieden)
	// --------------------------------------------------------------------------------------------------------------------------
	public void pruefeSpielende() {

		int produkt = 0;
		sieger = 0;

		// Waagrechte Pr�fung
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

		// Senkrechte Pr�fung
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

		// Diagonale Pr�fung nach rechts oben
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

		// Diagonale Pr�fung nach links oben
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

		// Pr�fung der obersten Zeile, ob noch weitere Steine hinzugef�gt werden k�nnen (unentschieden)
		for (int s = 0; s < spalten; s++) {
			unentschieden = unentschieden * feld [zeilen - 1, s];
		}
	}
}