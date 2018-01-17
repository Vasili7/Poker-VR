using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour {

    public List<p1> playersInPot = new List<p1>();
    public int amountInPot;
    public int minimumRaise;
    public int maximumAmountPutIn;
    public int smallBlind, bigBlind;

	public TextMesh pot_amount_txt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Pot(int amount, List<p1> playersInPot)
    {
        this.amountInPot = amount;
        this.playersInPot = playersInPot;
	
    }

    //add money to pot
    public void Add(int amount)
    {
        if (amount < 0)
            return;
        amountInPot += amount;

		pot_amount_txt.text = amountInPot.ToString (); // set pot ???
    }

    //add player to pot
    public void AddPlayer(p1 player)
    {
        if (!playersInPot.Contains(player))
            playersInPot.Add(player);
    }

    //get maximum amount in pot
    public int getMaximumAmountPutIn()
    {
        return maximumAmountPutIn;
    }
    //set maximum amount in pot
    public void setMaximumAmount(int amount)
    {
        maximumAmountPutIn = amount;
    }

    //getter
    public List<p1> getPlayersInPot()
    {
        return playersInPot;
    }

    public void Reset()
    {
        amountInPot = 0;
        minimumRaise = 0;
        maximumAmountPutIn = 0;
        smallBlind = 0;
        bigBlind = 0;
    }
}
