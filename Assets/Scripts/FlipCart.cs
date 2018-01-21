using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlipCart : MonoBehaviour {

	private PointerEventData pointer;
	private RaycastHit lastHit;

	private int rotation_winkel=90;

	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Vector3 forward = transform.TransformDirection (Vector3.forward) * 20;


		 
		if (Physics.Raycast (transform.position, forward, out hit)) {


		//	if (hit.collider.gameObject.tag == "p31" || hit.collider.gameObject.tag == "p32") {
				transform.Rotate(new Vector3(180,0,0) * Time.deltaTime);
		//	}
		}
	}
}
