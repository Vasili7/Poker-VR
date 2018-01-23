using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class p1 : MonoBehaviour 
{
    //public GameObject j1, j5, j25, j100;
    //private GameObject goJetons;
    public List<GameObject> myJetons = new List<GameObject>();
    public List<GameObject> myHand = new List<GameObject>();
    public int chipStack = 0, amountInPot;
    public bool isBusted, folded;

    public Tisch tisch;
    //Pot pot;

	public int gewonnenRunden=0;
	public int verlorenenRunden = 0;
	public TextMesh siege_txt; 
	public TextMesh Niederlagen_txt;
	public TextMesh bank_amount;
	public TextMesh pot_amount;


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
    }

    //leave the round
    public void Fold(Pot mainPot) 
    {
        mainPot.getPlayersInPot().Remove(this);
        myHand[0].transform.position = GameObject.FindGameObjectWithTag("burnedCard").transform.position; 
        myHand[0].transform.Rotate(0, 0, 0);
        myHand[1].transform.position = GameObject.FindGameObjectWithTag("burnedCard").transform.position;
        myHand[1].transform.Rotate(0, 0, 0);
        this.myHand.Clear();
        folded = true;
    }
    //don't bet
    public void Check(Pot mainPot) { }
    //call and bet additional amount of money
    public void Raise(int raise, Pot mainPot) 
    {
        amountInPot = mainPot.amountInPot;
        int amount = mainPot.getMaximumAmountPutIn() + raise; // - amountInPot;
        if (chipStack <= amount)
        {
            AllIn(mainPot);
            return;
        }
        chipStack -= amount;
        amountInPot += amount;
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
        if (chipStack <= amount)
        {
            AllIn(mainPot);
            return;
        }
        chipStack -= amount;
        amountInPot += amount;
        mainPot.Add(amount);
        mainPot.AddPlayer(this);
    }
    //bet a certain amount of money
    public void Bet(int bet, Pot mainPot) 
    {
        amountInPot = mainPot.amountInPot;

        if (chipStack <= bet)
        {
            AllIn(mainPot);
            return;
        }
        chipStack -= bet;
        amountInPot += bet;
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
        if (amountInPot > mainPot.getMaximumAmountPutIn())
            mainPot.setMaximumAmount(amountInPot);
    }

    //collect the winnings if the player wins
    public void CollectMoney()
    {
        chipStack += tisch.amountInPot;
    }

    //pay the small and big blinds
    public void PaySmallBlind(int amount, Pot mainPot)
    {
        if (chipStack <= amount)
        {
            //AllIn(mainPot);
            return;
        }
        chipStack -= amount;
        amountInPot += amount;
        mainPot.Add(amount);
        mainPot.AddPlayer(this);
        mainPot.setMaximumAmount(amountInPot);
        mainPot.minimumRaise = amount;
    }

    public void PayBigBlind(int amount, Pot mainPot)
    {
        if (chipStack <= amount)
        {
            AllIn(mainPot);
            return;
        }
        chipStack -= amount;
        amountInPot += amount;
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
        //this.chipStack = 0;
        //this.folded = false;
        myHand[0].transform.position = GameObject.FindGameObjectWithTag("burnedCard").transform.position; 
        myHand[0].transform.Rotate(0, 0, 0);
        myHand[1].transform.position = GameObject.FindGameObjectWithTag("burnedCard").transform.position;
        myHand[1].transform.Rotate(0, 0, 0);
        this.myHand.Clear();
        //this.myJetons.Clear();
    }

    //set isBusted to true if the player busted out
    public void Leave()
    {
        // do something
        isBusted = true;
    }

    //public void Sorting()
    //{
    //    myJetons.OrderBy(j1 => j1.name);
    //    myHand.OrderBy(j1 => j1);
    //}
}
