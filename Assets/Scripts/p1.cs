using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p1 : MonoBehaviour 
{
    public GameObject j1, j5, j25, j100;
    private GameObject goJetons;
    public List<GameObject> myJetons = new List<GameObject>();
    public List<GameObject> myHand = new List<GameObject>();
    public int chipStack, amountInPot;
    public bool folded = false;
    public bool dealer, smallBlind, bigBlind = false;

    public GameObject thisPlayer, btn;

    // Use this for initialization
    void Start () 
    {
        
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void Fold() 
    {
        folded = true;
    }
    public void Check() { }
    public void Raise() { }
    public void Call() { }
    public void Bet() { }
    public void AllIn() { }



}
