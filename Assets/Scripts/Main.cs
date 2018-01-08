using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    string playerName = "vasili";
    int BuyInAmount = 200, playerAmount, difficulty;
 //   Tisch tisch = new Tisch();
   // PlayerList playerList = new PlayerList();
    Random rand = new Random();
    //public KartenBewegungZumSpieler k = new KartenBewegungZumSpieler();
    public Rigidbody ca;

    //Player me;

	// Use this for initialization
    void Start () {

        //k.BewegeKarten();
        // das funktioniert! wieso funktioniert es nicht im tisch? vllt in deck() versuchen
        //ca.transform.position = GameObject.FindGameObjectWithTag("bc1").transform.position;
        //ca.transform.Rotate(180, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
