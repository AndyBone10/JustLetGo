using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnotToCurtainTrigger : MonoBehaviour {

	public GameObject thirdBedroomSceneObject;
	private ThirdBedroomSceneController theThirdBedroomController;

	// Use this for initialization
	void Start () {
		theThirdBedroomController = thirdBedroomSceneObject.GetComponent<ThirdBedroomSceneController> ();
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			theThirdBedroomController.nooseTie = true;
			theThirdBedroomController.sideBedCollider.gameObject.SetActive (true);
			theThirdBedroomController.tyingKnot = false;
			theThirdBedroomController.coroutineStarted = false;
			;
		}
	}
}
