using UnityEngine;

public class Jetons : MonoBehaviour {

    public int wert;

	// Use this for initialization
	void Start () {
        SetAmountOfJetons();
	}
	
    // 1 5 25 100
    public void SetAmountOfJetons()
    {
        if(this.name == "v1(Clone)")
        {
            wert = 1;
        }         
        if(this.name == "v5(Clone)")
        {
            wert = 5;
        }
        if(this.name == "v25(Clone)")
        {
            wert = 25;
        } 
        if(this.name == "v100(Clone)")
        {
            wert = 100;
        } 
    }
}
