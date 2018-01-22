﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Tisch : MonoBehaviour
{
    public GameObject ca, c2, c3, c4, c5, c6, c7, c8, c9, c10, cj, cq, ck;
    public GameObject ha, h2, h3, h4, h5, h6, h7, h8, h9, h10, hj, hq, hk;
    public GameObject da, d2, d3, d4, d5, d6, d7, d8, d9, d10, dj, dq, dk;
    public GameObject sa, s2, s3, s4, s5, s6, s7, s8, s9, s10, sj, sq, sk;

	public GameObject j1, j5, j25, j100;
    public GameObject bigBlindBtn, smallBlindBtn, DealerBtn;

    private GameObject goJetons;

    public List<GameObject> deck = new List<GameObject>();
    public List<GameObject> tableHand = new List<GameObject>();
    public int amountInPot;

    public bool spielEnde, neuesSpiel, spielStart;

    // Positions-Tags für die HoleCards der Spieler
    string p11 = "p11", p12 = "p12", p21 = "p21", p22 = "p22", p31 = "p31", 
    p32 = "p32", p41 = "p41", p42 = "p42", p51 = "p51", p52 = "p52";

    public p1 player1, player2, player3, player4, player5 = new p1();
    // Liste von allen Spielern, von der Klasse p1 
    public List<p1> pList = new List<p1>();
    public List<p1> playersInMainpot = new List<p1>();

    public Pot mainPot;

    public int roundCount;

	public GameObject pp31, pp32;

    public void Start(){}

    public void Update()
    {
        //amountInPot = mainPot.amountInPot;
        //player3.pot_amount.text = amountInPot.ToString ();   //???show pot in HUD
        //playersInMainpot = mainPot.playersInPot;
    }

	// Position-Tag is needed to instatiate jetons
    public void AddFirstJetons()
    {
        // Player 1
        for (int i = 0; i< 10; i++)
        {
            goJetons = (GameObject)Instantiate(j1, GameObject.FindGameObjectWithTag("1j1").transform.position, j1.transform.rotation);
            player1.chipStack++;
            player1.myJetons.Add(j1);
        }
        for (int i = 0; i< 30; i++)
        {
            goJetons = (GameObject)Instantiate(j5, GameObject.FindGameObjectWithTag("1j5").transform.position, j5.transform.rotation);
            player1.chipStack = player1.chipStack + 5;
            player1.myJetons.Add(j5);
        }
        for (int i = 0; i< 30; i++)
        {
            goJetons = (GameObject)Instantiate(j25, GameObject.FindGameObjectWithTag("1j25").transform.position, j25.transform.rotation);
            player1.chipStack = player1.chipStack + 25;
            player1.myJetons.Add(j25);
        }
        for (int i = 0; i< 10; i++)
        {
            goJetons = (GameObject)Instantiate(j100, GameObject.FindGameObjectWithTag("1j100").transform.position, j100.transform.rotation);
            player1.chipStack = player1.chipStack + 100;
            player1.myJetons.Add(j100);
        }   
        // Player 2
        for (int i = 0; i< 10; i++)
        {
            goJetons = (GameObject)Instantiate(j1, GameObject.FindGameObjectWithTag("2j1").transform.position, j1.transform.rotation);
            player2.chipStack++;
            player2.myJetons.Add(j1);
        }
        for (int i = 0; i< 30; i++)
        {
            goJetons = (GameObject)Instantiate(j5, GameObject.FindGameObjectWithTag("2j5").transform.position, j5.transform.rotation);
            player2.chipStack = player2.chipStack + 5;
            player2.myJetons.Add(j5);
        }
        for (int i = 0; i< 30; i++)
        {
            goJetons = (GameObject)Instantiate(j25, GameObject.FindGameObjectWithTag("2j25").transform.position, j25.transform.rotation);
            player2.chipStack = player2.chipStack + 25;
            player2.myJetons.Add(j25);
        }
        for (int i = 0; i< 10; i++)
        {
            goJetons = (GameObject)Instantiate(j100, GameObject.FindGameObjectWithTag("2j100").transform.position, j100.transform.rotation);
            player2.chipStack = player2.chipStack + 100;
            player2.myJetons.Add(j100);
        } 
        // Player 3
        for (int i = 0; i < 10; i++)
        {
            goJetons = (GameObject)Instantiate(j1, GameObject.FindGameObjectWithTag("3j1").transform.position, j1.transform.rotation);
            player3.chipStack++;
            player3.myJetons.Add(j1);
        }
        for (int i = 0; i < 30; i++)
        {
            goJetons = (GameObject)Instantiate(j5, GameObject.FindGameObjectWithTag("3j5").transform.position, j5.transform.rotation);
            player3.chipStack = player3.chipStack + 5;
            player3.myJetons.Add(j5);
        }
        for (int i = 0; i < 30; i++)
        {
            goJetons = (GameObject)Instantiate(j25, GameObject.FindGameObjectWithTag("3j25").transform.position, j25.transform.rotation);
            player3.chipStack = player3.chipStack + 25;
            player3.myJetons.Add(j25);
        }
        for (int i = 0; i < 10; i++)
        {
            goJetons = (GameObject)Instantiate(j100, GameObject.FindGameObjectWithTag("3j100").transform.position, j100.transform.rotation);
            player3.chipStack = player3.chipStack + 100;
            player3.myJetons.Add(j100);
        }
        // Player 4
        for (int i = 0; i < 10; i++)
        {
            goJetons = (GameObject)Instantiate(j1, GameObject.FindGameObjectWithTag("4j1").transform.position, j1.transform.rotation);
            player4.chipStack++;
            player4.myJetons.Add(j1);
        }
        for (int i = 0; i < 30; i++)
        {
            goJetons = (GameObject)Instantiate(j5, GameObject.FindGameObjectWithTag("4j5").transform.position, j5.transform.rotation);
            player4.chipStack = player4.chipStack + 5;
            player4.myJetons.Add(j5);
        }
        for (int i = 0; i < 30; i++)
        {
            goJetons = (GameObject)Instantiate(j25, GameObject.FindGameObjectWithTag("4j25").transform.position, j25.transform.rotation);
            player4.chipStack = player4.chipStack + 25;
            player4.myJetons.Add(j25);
        }
        for (int i = 0; i < 10; i++)
        {
            goJetons = (GameObject)Instantiate(j100, GameObject.FindGameObjectWithTag("4j100").transform.position, j100.transform.rotation);
            player4.chipStack = player4.chipStack + 100;
            player4.myJetons.Add(j100);
        }
        // Player 5
        for (int i = 0; i < 10; i++)
        {
            goJetons = (GameObject)Instantiate(j1, GameObject.FindGameObjectWithTag("5j1").transform.position, j1.transform.rotation);
            player5.chipStack++;
            player5.myJetons.Add(j1);
        }
        for (int i = 0; i < 30; i++)
        {
            goJetons = (GameObject)Instantiate(j5, GameObject.FindGameObjectWithTag("5j5").transform.position, j5.transform.rotation);
            player5.chipStack = player5.chipStack + 5;
            player5.myJetons.Add(j5);
        }
        for (int i = 0; i < 30; i++)
        {
            goJetons = (GameObject)Instantiate(j25, GameObject.FindGameObjectWithTag("5j25").transform.position, j25.transform.rotation);
            player5.chipStack = player5.chipStack + 25;
            player5.myJetons.Add(j25);
        }
        for (int i = 0; i < 10; i++)
        {
            goJetons = (GameObject)Instantiate(j100, GameObject.FindGameObjectWithTag("5j100").transform.position, j100.transform.rotation);
            player5.chipStack = player5.chipStack + 100;
            player5.myJetons.Add(j100);
        }
    }

    public void RemoveJetons()
    {
        // ??
    }

    // cleans the table to begin a new round
    public void StartNewMatch()
    {
        tableHand.Clear();
        AddAllCardsToDeck();
        AddAllPlayers();
		DealAllHoleCards();
    }

	// Deal Hole Cards to all 5 Players
	public void DealAllHoleCards() 
	{
        DealHoleCards(p11, p12, player1);
		DealHoleCards(p21, p22, player2);
		DealHoleCards(p31, p32, player3);
		DealHoleCards(p41, p42, player4);
		DealHoleCards(p51, p52, player5);
	}

    public void AddPlayer(p1 player)
    {
       
        if (!pList.Contains(player) && player.isBusted == false)
        {
            pList.Add(player);
        }
    }
    public void RemovePlayer(p1 player)
    {
        if (pList.Contains(player))
        {
            pList.Remove(player);
        }
    }
    public void AddAllPlayers()
    {
        // Alle Spieler hinzufügen
        AddPlayer(player1);
        AddPlayer(player2);
        AddPlayer(player3);
        AddPlayer(player4);
        AddPlayer(player5);
        mainPot.AddPlayer(player1);
        mainPot.AddPlayer(player2);
        mainPot.AddPlayer(player3);
        mainPot.AddPlayer(player4);
        mainPot.AddPlayer(player5);
    }
    public void AddAllCardsToDeck()
    {
        // Alle Spielkarten hinzufügen
        AddCardToDeck(ca);
        AddCardToDeck(c2);
        AddCardToDeck(c3);
        AddCardToDeck(c4);
        AddCardToDeck(c5);
        AddCardToDeck(c6);
        AddCardToDeck(c7);
        AddCardToDeck(c8);
        AddCardToDeck(c9);
        AddCardToDeck(c10);
        AddCardToDeck(cj);
        AddCardToDeck(cq);
        AddCardToDeck(ck);
        AddCardToDeck(ha);
        AddCardToDeck(h2);
        AddCardToDeck(h3);
        AddCardToDeck(h4);
        AddCardToDeck(h5);
        AddCardToDeck(h6);
        AddCardToDeck(h7);
        AddCardToDeck(h8);
        AddCardToDeck(h9);
        AddCardToDeck(h10);
        AddCardToDeck(hj);
        AddCardToDeck(hq);
        AddCardToDeck(hk);
        AddCardToDeck(da);
        AddCardToDeck(d2);
        AddCardToDeck(d3);
        AddCardToDeck(d4);
        AddCardToDeck(d5);
        AddCardToDeck(d6);
        AddCardToDeck(d7);
        AddCardToDeck(d8);
        AddCardToDeck(d9);
        AddCardToDeck(d10);
        AddCardToDeck(dj);
        AddCardToDeck(dq);
        AddCardToDeck(dk);
        AddCardToDeck(sa);
        AddCardToDeck(s2);
        AddCardToDeck(s3);
        AddCardToDeck(s4);
        AddCardToDeck(s5);
        AddCardToDeck(s6);
        AddCardToDeck(s7);
        AddCardToDeck(s8);
        AddCardToDeck(s9);
        AddCardToDeck(s10);
        AddCardToDeck(sj);
        AddCardToDeck(sq);
        AddCardToDeck(sk);

        Shuffel();
 
    }

    public void Shuffel()
    {
        for (int i = 0; i < deck.Count(); i++)
        {
            var temp = deck[i];
            int randomIndex = UnityEngine.Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
        float abstand = 0.003f;
        for (int i = 0; i < deck.Count(); i++)
        {
            deck[i].transform.localPosition = new Vector3(0, abstand, 0);
            deck[i].transform.Rotate(0, 0, 0);
            abstand = abstand + 0.001f;
        }
    }

    public void DealHoleCards(string a, string b, p1 p)
    {
        if (pList.Contains(p))
        {
            for (int i = 0; i < deck.Count(); i++)
            {
                if (i == 0)
                {
                    deck[i].transform.position = GameObject.FindGameObjectWithTag(a).transform.position;
					if (a == "p31") 
					//	deck [i].transform.Rotate (-90, 0, 0);
						pp31 = deck [i];

					deck[i].transform.Rotate(0, 0, 0);
                    //myHand.Add(deck[i]);
                    //player1.myHand.Add(deck[i]);
                    p.myHand.Add(deck[i]);
                    deck.RemoveAt(i);

                    deck[i].transform.position = GameObject.FindGameObjectWithTag(b).transform.position;
					if (b == "p32") 
					//	deck [i].transform.Rotate (-90, 0, 0);
						pp32 = deck [i];

					deck[i].transform.Rotate(0, 0, 0);
                    //myHand.Add(deck[i]);
                    //player1.myHand.Add(deck[i]);
                    p.myHand.Add(deck[i]);
                    deck.RemoveAt(i);

                }
            }
        }
    }
    public void DealFlop()
    { 
        BurnCard();
        mainPot.maximumAmountPutIn = 0;
        DealBoardCard("bc1");
        DealBoardCard("bc2");
        DealBoardCard("bc3");
    }
    public void DealTurn()
    {
        BurnCard();
        mainPot.maximumAmountPutIn = 0;
        DealBoardCard("bc4");
    }
    public void DealRiver()
    {
        BurnCard();
        mainPot.maximumAmountPutIn = 0;
        DealBoardCard("bc5");
    }

    public void DealBoardCard(string bc)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            if (i == 0)
            {
                deck[i].transform.position = GameObject.FindGameObjectWithTag(bc).transform.position;
                deck[i].transform.Rotate(180, 0, 0);
                tableHand.Add(deck[i]);
                // bc werden den Spieler zugeordnet, die noch in der Liste sind
                for (int a = 0; a < mainPot.playersInPot.Count(); a++)
                {
                    mainPot.playersInPot[a].myHand.Add(deck[i]);
                }
                deck.RemoveAt(i);
            }
        }
    }
    public void BurnCard()
    {
        deck[0].transform.position = GameObject.FindGameObjectWithTag("burnedCard").transform.position;
        deck[0].transform.Rotate(0, 0, 0);
        deck.RemoveAt(0);
    }

    public void AddCardToDeck(GameObject card)
    {
        //card.transform.localPosition = new Vector3(0,0,0);
        if (!deck.Contains(card))
        {
            card.transform.Rotate(0, 0, 0);
            deck.Add(card);
        }
    }

    // Showdown
    public void ShowDown()
    {
		
/*		// HUD for gewonnere und verlorene Runden und bank
		int bank = player3.bank_amount; 
		// wenn gewonnen
		player3.gewonnenRunden++;
		player3.siege_txt.text = "Siege:" + player3.gewonnenRunden;

		bank += amountInPot;
		player3.bank_amount.text = bank.ToString ();


		//wenn verloren
		player3.verlorenenRunden++;
		player3.Niederlagen_txt.text = "Niederlage:" + player3.verlorenenRunden;

		bank -= amountInPot;
		player3.bank_amount.text = bank.ToString ();
*/
      }



    // Random method for AI
    public void RandomChoose(p1 player)
    {
	    // fold, allin + check bringt die Reihenfolge durcheinander, deswegen deaktiviert
        int randomIndex = UnityEngine.Random.Range(3, 5);
        switch (randomIndex)
        {
            case 1:
                player.Fold(mainPot);
                break;
            case 2:
                player.Check(mainPot);
                break;		
            case 3:
                player.Raise(100, mainPot);
                break;
            case 4:
                player.Call(mainPot);
                break;
            case 5:
                player.Bet(150, mainPot);
                break;
            case 6:
                player.AllIn(mainPot);
                break;
        }
    }


    public void Reset()
    {
        AddAllCardsToDeck();
        tableHand.Clear();
        for (int i = 0; i < pList.Count(); i++)
        {
            pList[i].Reset();
        }
        mainPot.Reset();
    }
}

