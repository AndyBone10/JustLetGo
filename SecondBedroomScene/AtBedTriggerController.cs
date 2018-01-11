using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtBedTriggerController : MonoBehaviour {

	public GameObject secondBedroomSceneObject;
	private SecondBedroomSceneController theSecondBedroomController;
	// Use this for initialization
	void Start () {
		theSecondBedroomController = secondBedroomSceneObject.GetComponent<SecondBedroomSceneController> ();
	}
	

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			theSecondBedroomController.moveToBed = false;
			theSecondBedroomController.playerAtBed = true;
		}
	}
}
