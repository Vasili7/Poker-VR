using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/* --------------------------------------------------------------------------------------------------------------------------
 * ERSTELLT VON:
 * Vasilios Solkidis
 * --------------------------------------------------------------------------------------------------------------------------
 * BESCHREIBUNG:
 * - Dieses Skript enthält alle notwendigen Aktionen eines Spielers
 * - Fold, Call, Check, Raise, Bet, AllIn, CollectMoney, PaySmallBlind, PayBigBlind, Reset und Leave
 * --------------------------------------------------------------------------------------------------------------------------
*/

public class p1 : MonoBehaviour 
{
    //public GameObject j1, j5, j25, j100;
    //private GameObject goJetons;
    public List<GameObject> myJetons = new List<GameObject>();
    public List<GameObject> myHand = new List<GameObject>();
	public int chipStack = 0;
	public int amountInPot;
    public bool isBusted, folded;

    public Tisch tisch;
    //Pot pot;

	public int gewonnenRunden=0;
	public int verlorenenRunden = 0;
	public TextMesh siege_txt; 
	public TextMesh Niederlagen_txt;
	public static int niederlagen = 0;
	public TextMesh bank_amount;
	public TextMesh pot_amount;
	public int bank=200;


    // Use this for initialization
    void Start () 
    {
        isBusted = false;
        folded = false;

    }
    
    // Update is called once per frame
    void Update () 
    {
        //amountInPot = pot.amountInPot;
		//bank_amount.text = bank.ToString();
//		pot_amount.text = amountInPot.ToString ();
		bank_amount.text = bank.ToString ();
    }

    //leave the round, removed from the list and cards send to burnedCards
    public void Fold(Pot mainPot) 
    {
        mainPot.getPlayersInPot().Remove(this);
        myHand[0].transform.position = GameObject.FindGameObjectWithTag("burnedCard").transform.position; 
        myHand[0].transform.Rotate(0, 0, 0);
        myHand[1].transform.position = GameObject.FindGameObjectWithTag("burnedCard").transform.position;
        myHand[1].transform.Rotate(0, 0, 0);
        this.myHand.Clear();
//		niederlagen++;
//		Niederlagen_txt.text = "Niederlagen: "+niederlagen;
        folded = true;
    }
    //don't bet
    public void Check(Pot mainPot) { }

    //call and bet additional amount of money
    public void Raise(int raise, Pot mainPot) 
    {
        amountInPot = mainPot.amountInPot;
        int amount = mainPot.getMaximumAmountPutIn() + raise; // - amountInPot;
        //if (chipStack <= amount)
        //{
        //    AllIn(mainPot);
        //    return;
        //}
        chipStack -= amount;
        amountInPot += amount;
		bank -= amount;
//		bank_amount.text = bank.ToString ();
		pot_amount.text = amountInPot.ToString ();
        mainPot.Add(amount);
        //if (mainPot.getMaximumAmountPutIn() >= amount)
        //{ }
            mainPot.setMaximumAmount(amount);
        
        mainPot.AddPlayer(this);
        mainPot.minimumRaise = raise;
    }

    //bet enough to stay in the round
    public void Call(Pot mainPot) 
    {
        amountInPot = mainPot.amountInPot;

        int amount = mainPot.getMaximumAmountPutIn(); // - amountInPot;
        //if (chipStack <= amount)
        //{
        //    AllIn(mainPot);
        //    return;
        //}
        chipStack -= amount;
        amountInPot += amount;
		bank -= amount;
//		bank_amount.text = bank.ToString ();
		pot_amount.text = amountInPot.ToString ();
        mainPot.Add(amount);
        mainPot.AddPlayer(this);
    }

    //bet a certain amount of money
    public void Bet(int bet, Pot mainPot) 
    {
        amountInPot = mainPot.amountInPot;
        mainPot.AddPlayer(this);
        //if (chipStack <= bet)
        //{
        //    AllIn(mainPot);
        //    return;
        //}
        chipStack -= bet;
        amountInPot += bet;
		pot_amount.text = amountInPot.ToString ();
        mainPot.Add(bet);
        mainPot.setMaximumAmount(amountInPot);
        mainPot.minimumRaise = bet;
    }

    //all in, bet remaining chipstack
    public void AllIn(Pot mainPot) 
    {
        if (chipStack > mainPot.minimumRaise)
            mainPot.minimumRaise = chipStack;
        mainPot.AddPlayer(this);
        mainPot.Add(chipStack);
        amountInPot += chipStack;
        chipStack = 0;
		bank = 0;
		bank_amount.text = bank.ToString ();
		pot_amount.text = amountInPot.ToString ();
        if (amountInPot > mainPot.getMaximumAmountPutIn())
            mainPot.setMaximumAmount(amountInPot);
    }

    //collect the winnings if the player wins
    public void CollectMoney()
    {
        chipStack += tisch.amountInPot;
    }

    //pay the small blinds
    public void PaySmallBlind(int amount, Pot mainPot)
    {
        if (chipStack <= amount)
        {
            //AllIn(mainPot);
            return;
        }
        chipStack -= amount;
        amountInPot += amount;
		pot_amount.text = amountInPot.ToString ();
        mainPot.Add(amount);
        mainPot.AddPlayer(this);
        mainPot.setMaximumAmount(amountInPot);
        mainPot.minimumRaise = amount;
    }

    // pay the big blind
    public void PayBigBlind(int amount, Pot mainPot)
    {
        if (chipStack <= amount)
        {
            AllIn(mainPot);
            return;
        }
        chipStack -= amount;
        amountInPot += amount;
		pot_amount.text = amountInPot.ToString ();
        mainPot.Add(amount);
        mainPot.AddPlayer(this);
        mainPot.setMaximumAmount(amountInPot);
        mainPot.minimumRaise = amount;
    }

    //reset the individual players
    public void Reset()
    {
        this.amountInPot = 0;
        folded = false;
        isBusted = false;
        this.myHand.Clear();
        //this.myJetons.Clear();
    }

    //set isBusted to true if the player busted out
    public void Leave()
    {
        // do something
        isBusted = true;
    }
}
