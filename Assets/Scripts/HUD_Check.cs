using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_Check : MonoBehaviour {

	public GameObject this_HUD;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "bone3") {
	//		this_HUD.gameObject.SetActive (false);
		}
	}
}
