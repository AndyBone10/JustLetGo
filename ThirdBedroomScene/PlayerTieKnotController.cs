using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTieKnotController : MonoBehaviour {

	public GameObject thirdBedroomSceneObject;
	private ThirdBedroomSceneController theThirdBedroomController;
	// Use this for initialization
	void Start () {
		theThirdBedroomController = thirdBedroomSceneObject.GetComponent<ThirdBedroomSceneController> ();
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			theThirdBedroomController.aboutToTieKnot = false;
			theThirdBedroomController.doneCrying = true;
			theThirdBedroomController.tyingKnot = true;
			theThirdBedroomController.coroutineStarted = false;
		}
	}
}
