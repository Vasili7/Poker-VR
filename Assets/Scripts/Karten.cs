using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karten : MonoBehaviour { 
    // Liste mit Werten. Beginnend mit 2 bis 14 bzw 1 bis 4
    public enum RANK
    {
        TWO = 2, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE
    }
    public enum SUIT
    {
        DIAMONDS = 1,
        CLUBS,
        HEARTS,
        SPADES
    }

    private int rank, suit;
    private bool v;


    //default two of diamonds
    public Karten()
    {
        rank = (int)RANK.TWO;
        suit = (int)SUIT.DIAMONDS;
    }

    public Karten(RANK rank, SUIT suit)
    {
        this.rank = (int)rank;
        this.suit = (int)suit;
    }

    public Karten(int rank, int suit)
    {
        if (rank < 1 || rank > 14 || suit < 1 || suit > 4)
            throw new ArgumentOutOfRangeException();
        this.rank = rank;
        this.suit = suit;
    }

    public Karten(Karten karte)
    {
        this.rank = karte.rank;
        this.suit = karte.suit;
    }

    public Karten(int rank, int suit, bool v) : this(rank, suit)
    {
        this.v = v;
    }

    public static string rankToString(int rank)
    {
        switch (rank)
        {
            case 11:
                return "Jack";
            case 12:
                return "Queen";
            case 13:
                return "King";
            case 14:
                return "Ace";
            default:
                return rank.ToString();
        }
    }
    public static string suitToString(int suit)
    {
        switch (suit)
        {
            case 1:
                return "Diamonds";
            case 2:
                return "Clubs";
            case 3:
                return "Hearts";
            default:
                return "Spades";
        }
    }
    public int getRank()
    {
        return rank;
    }
    public int getSuit()
    {
        return suit;
    }


        public void setRank(RANK rank)
    {
        this.rank = (int)rank;
    }
    public void setKarten(RANK rank, SUIT suit)
    {
        this.rank = (int)rank;
        this.suit = (int)suit;
    }
    public void setKarten(int rank, int suit)
    {
        if (rank < 1 || rank > 14 || suit < 1 || suit > 4)
            throw new ArgumentOutOfRangeException();
        this.rank = rank;
        this.suit = suit;
    }


        //compare rank of cards

    // für was macht er das ???
    public static bool operator ==(Karten a, Karten b)
    {
        if (a.rank == b.rank)
            return true;
        else
            return false;
    }
    public static bool operator !=(Karten a, Karten b)
    {
        if (a.rank != b.rank)
            return true;
        else
            return false;
    }
    public static bool operator <(Karten a, Karten b)
    {
        if (a.rank < b.rank)
            return true;
        else
            return false;
    }
    public static bool operator >(Karten a, Karten b)
    {
        if (a.rank > b.rank)
            return true;
        else
            return false;
    }
    public static bool operator <=(Karten a, Karten b)
    {
        if (a.rank <= b.rank)
            return true;
        else
            return false;
    }
    public static bool operator >=(Karten a, Karten b)
    {
        if (a.rank >= b.rank)
            return true;
        else
            return false;
    }
}
