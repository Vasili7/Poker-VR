using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartenBewegungZumSpieler : MonoBehaviour
{

    public int schwierigkeit;
    public GameObject buttonEnd;
    public GameObject buttonRestart;
    public GameObject buttonLeicht;
    public GameObject buttonMittel;
    public GameObject buttonSchwer;
    public GameObject Player;
    public TextMesh schwierigkeit_txt;
    //public GameObject ca, c2, c3, c4, c5, c6, c7, c8, c9, c10, cj, cq, ha, h2, h3, h4, h5, h6, h7;
    //public Rigidbody ca, c2, c3, c4, c5, c6, c7, c8, c9, c10, cj, cq, ha, h2, h3, h4, h5, h6, h7;
    //public GameObject blueChip, redChip, blackChip, whiteChip, greenChip;
    // public Rigidbody redChip;
    //public Rigidbody ck;

    GameObject cardInstance;

	public GameObject HUD_Check;
	public GameObject HUD_Fold;
	public GameObject HUD_Raise;
	public GameObject HUD_Raise_plus;
	public GameObject HUD_Raise_minus;

    private void Start()
    {

        //BewegeKarten();
        //FuegeJetonsHinzu();

		HUD_Check.SetActive(false);
		HUD_Fold.SetActive(false);
		HUD_Raise.SetActive(false);
		HUD_Raise_plus.SetActive(false);
		HUD_Raise_minus.SetActive(false);

    }
    /*
    public void FuegeJetonsHinzu()
    {
        // Instantiate, Problem: Skalierung
        // Rigidbody redChipClone = (Rigidbody)Instantiate(redChip, GameObject.FindGameObjectWithTag("removed1").transform.position, redChip.transform.rotation);

        //GameObject cardInstance = (GameObject)Instantiate(blueChip, GameObject.FindGameObjectWithTag("blue1").transform.position, blueChip.transform.rotation);
        // Karten karte = cardInstance.GetComponent<Karten>();
        // redChipClone.transform.SetParent(this.redChip.transform, false); *.log Temp Library

        for (int i = 0; i < 15; i++)
        {
            cardInstance = (GameObject)Instantiate(blueChip, GameObject.FindGameObjectWithTag("blue1").transform.position, blueChip.transform.rotation);
            cardInstance = (GameObject)Instantiate(blackChip, GameObject.FindGameObjectWithTag("black1").transform.position, blackChip.transform.rotation);
            cardInstance = (GameObject)Instantiate(redChip, GameObject.FindGameObjectWithTag("red1").transform.position, redChip.transform.rotation);
            cardInstance = (GameObject)Instantiate(greenChip, GameObject.FindGameObjectWithTag("green1").transform.position, greenChip.transform.rotation);
            cardInstance = (GameObject)Instantiate(whiteChip, GameObject.FindGameObjectWithTag("white1").transform.position, whiteChip.transform.rotation);


            cardInstance = (GameObject)Instantiate(blueChip, GameObject.FindGameObjectWithTag("blue2").transform.position, blueChip.transform.rotation);
            cardInstance = (GameObject)Instantiate(blackChip, GameObject.FindGameObjectWithTag("black2").transform.position, blackChip.transform.rotation);
            cardInstance = (GameObject)Instantiate(redChip, GameObject.FindGameObjectWithTag("red2").transform.position, redChip.transform.rotation);
            cardInstance = (GameObject)Instantiate(greenChip, GameObject.FindGameObjectWithTag("green2").transform.position, greenChip.transform.rotation);
            cardInstance = (GameObject)Instantiate(whiteChip, GameObject.FindGameObjectWithTag("white2").transform.position, whiteChip.transform.rotation);
        }
        

    }
    */
    /*
    public void BewegeKarten()
    {
        ca.transform.position = GameObject.FindGameObjectWithTag("bc1").transform.position;
        ca.transform.Rotate(180, 0, 0);
        c2.transform.position = GameObject.FindGameObjectWithTag("bc2").transform.position;
        c2.transform.Rotate(180, 0, 0);
        c3.transform.position = GameObject.FindGameObjectWithTag("bc3").transform.position;
        c3.transform.Rotate(180, 0, 0);
        c4.transform.position = GameObject.FindGameObjectWithTag("p11").transform.position;
        c4.transform.Rotate(0, 0, 0);
        c5.transform.position = GameObject.FindGameObjectWithTag("p12").transform.position;
        c5.transform.Rotate(0, 0, 0);
        c6.transform.position = GameObject.FindGameObjectWithTag("p31").transform.position;
        c6.transform.Rotate(180, 0, 0);
        c7.transform.position = GameObject.FindGameObjectWithTag("p32").transform.position;
        c7.transform.Rotate(180, 0, 0);
        ca.useGravity = true;
        c2.useGravity = true;
        c3.useGravity = true;
        c4.useGravity = true;
        c5.useGravity = true;
        c6.useGravity = true;
        c7.useGravity = true;
    }
    */






    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "bone3")
        {
            Debug.Log("START BUTTON PRESSED");
            switch (schwierigkeit)
            {
                case 1:
                    schwierigkeit_txt.text = "Spielstärke: Einfach";
     //               SpielSteinEinwurfLEAP.schwierigkeit = "Einfach";
                    schwierigkeit_txt.transform.localPosition = new Vector3(16.092f, 0.355f, -9.414f);

                    break;
                case 2:
                    schwierigkeit_txt.text = "Spielstärke: Mittel";
   //                 SpielSteinEinwurfLEAP.schwierigkeit = "Mittel";
                    schwierigkeit_txt.transform.localPosition = new Vector3(16.092f, 0.355f, -9.414f);
                    break;
                case 3:
                    schwierigkeit_txt.text = "Spielstärke: Schwer";
 //                   SpielSteinEinwurfLEAP.schwierigkeit = "Schwer";
                    schwierigkeit_txt.transform.localPosition = new Vector3(16.092f, 0.355f, -9.414f);
                    break;
            }

            Bewegung.spielstart = true;
            Bewegung.geschwindigkeit = 0;
            Player.transform.position = new Vector3(15.673f, 0.177f, -7.725f);
            Player.transform.rotation = Quaternion.Euler(15.673f, 0.177f, -7.725f);
   //         SpielSteinEinwurfLEAP.neuesSpiel = true;
            buttonEnd.SetActive(true);
            buttonRestart.SetActive(true);
            buttonLeicht.SetActive(false);
            buttonMittel.SetActive(false);
            buttonSchwer.SetActive(false);

			HUD_Check.SetActive(true);
			HUD_Fold.SetActive(true);
			HUD_Raise.SetActive(true);
			HUD_Raise_plus.SetActive(true);
			HUD_Raise_minus.SetActive(true);


        }
    }

}
