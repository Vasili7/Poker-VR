/*

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using UnityEngine;

public class Deck : MonoBehaviour {

    private List<Karten> deck = new List<Karten>();
    public Deck()
    {
        for (int i = 2; i <= 14; i++)
        {
            for (int j = 1; j <= 4; j++)
            {
                deck.Add(new Karten(i, j, false));
            }
        }
    }

    public Deck(Deck otherDeck)
    {
        foreach (Karten karte in otherDeck.deck)
        {
            this.deck.Add(new Karten(karte));
        }
    }

    public void Add(Karten karte)
    {
        deck.Add(karte);
    }

    //using an online algorithm for shuffling
    public void Shuffle()
    {
        var rand = new Random();
        for (int i = CardsLeft() - 1; i > 0; i--)
        {
            int n = rand.Next(i + 1);
            Karten temp = deck[i];
            deck[i] = deck[n];
            deck[n] = temp;
        }
    }

    public int CardsLeft()
    {
        return deck.Count;
    }

    public Karten Deal()
    {
        Karten dealCard = deck.ElementAt(deck.Count - 1);
        deck.RemoveAt(deck.Count - 1);
        return dealCard;
    }

    public void Remove(int index)
    {
        if (index < 0 || index >= deck.Count)
            throw new ArgumentOutOfRangeException();
        deck.RemoveAt(index);
    }
    public void Remove(Karten karte)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            if (deck[i] == karte && deck[i].getSuit() == karte.getSuit())
            {
                deck.RemoveAt(i);
            }
        }
    }
    public Karten[] ToArray()
    {
        return deck.ToArray();
    }
    public List<Karten> ToList()
    {
        return deck;
    }
}

*/