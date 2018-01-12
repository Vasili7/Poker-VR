using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour {

    public int suit, rank;
 //   public GameObject thisCard = new GameObject();

	// Klasse benutzt um den Wert einer einzelnen Karte zu definieren
	void Start () {
        SetAmountOfThisCard();
	}

    public void SetAmountOfThisCard()
    {
      /*  
		switch(thisCard.name)
        {
            case "CA":
                suit = 2;
                rank = 14;
                return;
            case "C2":
                suit = 2;
                rank = 2;
                return;
      
        }
        */
    }
}

/*
            case 1:
                return "Diamonds";
            case 2:
                return "Clubs";
            case 3:
                return "Hearts";
            default:
                return "Spades";
*/
