using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WERT
{
      EINS = 1, ZWEI, FUENF, ZEHN, ZWANZIG, FUENFZIG, HUNDERT, FUENFHUNDERT, TAUSEND  
}

public class Jetons : MonoBehaviour {
    private int wert;

    //default jetons
    public Jetons()
    {
        wert = (int)WERT.EINS;
    }

    public Jetons(WERT wert)
    {
        // Listen Elemente einen Wert zuordnen
        switch(wert)
        {
            case (WERT)1:
                return;
        }
        this.wert = (int)wert;
    }

    public int GetWert()
    {
        return wert;
    }

    public void SetWert(WERT wert)
    {
        this.wert = (int)wert;
    }
}
