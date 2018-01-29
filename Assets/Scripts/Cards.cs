using UnityEngine;

/* --------------------------------------------------------------------------------------------------------------------------
 * ERSTELLT VON:
 * Vasilios Solkidis
 * --------------------------------------------------------------------------------------------------------------------------
 * BESCHREIBUNG:
 * - Dieses Skript initialisiert den Wert jeder Karte
 * - Rank: 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 = J = Bube, 12 = Q = Dame, 13 = K = König, 14 = A = Ass
 * - SUIT: 1 = Diamonds(Karo), 2 = Clubs(Kreuz), 3 = Hearts(Herz), 4 = Spades(Pik)
 * --------------------------------------------------------------------------------------------------------------------------
*/

public class Cards : MonoBehaviour {

    public int suit, rank;

	void Start () {
        SetAmountOfThisCard();
	}

    // Sortierung nach RANK
    public int SortByRank() { return this.rank; }

    // Sortierung nach SUIT
    public int SortBySuit() { return this.suit; }

    // Den Wert einer einzelnen Karte zu definieren
    public void SetAmountOfThisCard()
    {
        switch(this.name)
        {
            // DIAMONDS
            case "DA":
                suit = 1;
                rank = 14;
                return;
            case "D2":
                suit = 1;
                rank = 2;
                return;
            case "D3":
                suit = 1;
                rank = 3;
                return;
            case "D4":
                suit = 1;
                rank = 4;
                return;
            case "D5":
                suit = 1;
                rank = 5;
                return;
            case "D6":
                suit = 1;
                rank = 6;
                return;
            case "D7":
                suit = 1;
                rank = 7;
                return;
            case "D8":
                suit = 1;
                rank = 8;
                return;
            case "D9":
                suit = 1;
                rank = 9;
                return;
            case "D10":
                suit = 1;
                rank = 10;
                return;
            case "DJ":
                suit = 1;
                rank = 11;
                return;
            case "DQ":
                suit = 1;
                rank = 12;
                return;
            case "DK":
                suit = 1;
                rank = 13;
                return;
            // CLUBS
            case "CA":
                suit = 2;
                rank = 14;
                return;
            case "C2":
                suit = 2;
                rank = 2;
                return;
            case "C3":
                suit = 2;
                rank = 3;
                return;
            case "C4":
                suit = 2;
                rank = 4;
                return;
            case "C5":
                suit = 2;
                rank = 5;
                return;
            case "C6":
                suit = 2;
                rank = 6;
                return;
            case "C7":
                suit = 2;
                rank = 7;
                return;
            case "C8":
                suit = 2;
                rank = 8;
                return;
            case "C9":
                suit = 2;
                rank = 9;
                return;
            case "C10":
                suit = 2;
                rank = 10;
                return;
            case "CJ":
                suit = 2;
                rank = 11;
                return;
            case "CQ":
                suit = 2;
                rank = 12;
                return;
            case "CK":
                suit = 2;
                rank = 13;
                return;
                // HEARTS
            case "HA":
                suit = 3;
                rank = 14;
                return;
            case "H2":
                suit = 3;
                rank = 2;
                return;
            case "H3":
                suit = 3;
                rank = 3;
                return;
            case "H4":
                suit = 3;
                rank = 4;
                return;
            case "H5":
                suit = 3;
                rank = 5;
                return;
            case "H6":
                suit = 3;
                rank = 6;
                return;
            case "H7":
                suit = 3;
                rank = 7;
                return;
            case "H8":
                suit = 3;
                rank = 8;
                return;
            case "H9":
                suit = 3;
                rank = 9;
                return;
            case "H10":
                suit = 3;
                rank = 10;
                return;
            case "HJ":
                suit = 3;
                rank = 11;
                return;
            case "HQ":
                suit = 3;
                rank = 12;
                return;
            case "HK":
                suit = 3;
                rank = 13;
                return;
                // SPADES
            case "SA":
                suit = 4;
                rank = 14;
                return;
            case "S2":
                suit = 4;
                rank = 2;
                return;
            case "S3":
                suit = 4;
                rank = 3;
                return;
            case "S4":
                suit = 4;
                rank = 4;
                return;
            case "S5":
                suit = 4;
                rank = 5;
                return;
            case "S6":
                suit = 4;
                rank = 6;
                return;
            case "S7":
                suit = 4;
                rank = 7;
                return;
            case "S8":
                suit = 4;
                rank = 8;
                return;
            case "S9":
                suit = 4;
                rank = 9;
                return;
            case "S10":
                suit = 4;
                rank = 10;
                return;
            case "SJ":
                suit = 4;
                rank = 11;
                return;
            case "SQ":
                suit = 4;
                rank = 12;
                return;
            case "SK":
                suit = 4;
                rank = 13;
                return;
        }
    }
}