using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p1 : MonoBehaviour 
{
    //public GameObject j1, j5, j25, j100;
    //private GameObject goJetons;
    public List<GameObject> myJetons = new List<GameObject>();
    public List<GameObject> myHand = new List<GameObject>();
    public int chipStack, amountInPot;
    public bool isBusted, folded;

    public neu tisch;

    // Use this for initialization
    void Start () 
    {
        isBusted = false;
        folded = false;
    }
    
    // Update is called once per frame
    void Update () {
    }
    //leave the round
    public void Fold(Pot mainPot) 
    {
        folded = true;
        mainPot.getPlayersInPot().Remove(this);
        myHand[0].transform.position = GameObject.FindGameObjectWithTag("burnedCard").transform.position; 
        myHand[0].transform.Rotate(180, 0, 0);
        myHand[1].transform.position = GameObject.FindGameObjectWithTag("burnedCard").transform.position;
        myHand[1].transform.Rotate(180, 0, 0);
        this.myHand.Clear();
    }
    //don't bet
    public void Check(Pot mainPot) { }
    //call and bet additional amount of money
    public void Raise(int raise, Pot mainPot) 
    {
        int amount = mainPot.getMaximumAmountPutIn() + raise - amountInPot;
        if (chipStack <= amount)
        {
            AllIn(mainPot);
            return;
        }
        chipStack -= amount;
        amountInPot += amount;
        mainPot.Add(amount);
        mainPot.setMaximumAmount(amountInPot);
        mainPot.AddPlayer(this);
        mainPot.minimumRaise = raise;
    }
    //bet enough to stay in the round
    public void Call(Pot mainPot) 
    {
        int amount = mainPot.getMaximumAmountPutIn() - amountInPot;
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
        this.chipStack = 0;
        this.folded = false;
        myHand[0].transform.position = GameObject.FindGameObjectWithTag("burnedCard").transform.position; 
        myHand[0].transform.Rotate(180, 0, 0);
        myHand[1].transform.position = GameObject.FindGameObjectWithTag("burnedCard").transform.position;
        myHand[1].transform.Rotate(180, 0, 0);
        this.myHand.Clear();
        this.myJetons.Clear();
    }

    //set isBusted to true if the player busted out
    public void Leave()
    {
        // do something
        isBusted = true;
    }
}
