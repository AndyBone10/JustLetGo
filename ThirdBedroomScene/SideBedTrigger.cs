using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBedTrigger : MonoBehaviour {

	public GameObject Player;
	public GameObject thirdBedroomSceneObject;
	private ThirdBedroomSceneController theThirdBedroomController;
	// Use this for initialization
	void Start () {
		theThirdBedroomController = thirdBedroomSceneObject.GetComponent<ThirdBedroomSceneController> ();
	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			Player.gameObject.transform.rotation = Quaternion.Euler (0, -180, 0);
			theThirdBedroomController.nooseTie = false;
			theThirdBedroomController.movingToMiddleBed = false;
			theThirdBedroomController.pushingBed = true;
			theThirdBedroomController.coroutineStarted = false;

		}
	}
}
