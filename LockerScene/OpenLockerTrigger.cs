using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLockerTrigger : MonoBehaviour {

	public GameObject theLockers;
	public GameObject thePlayer;
	public GameObject lockerSceneObject;
	private LockerSceneController theLockerSceneController;

	void Start(){
		theLockerSceneController = lockerSceneObject.GetComponent<LockerSceneController> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			theLockerSceneController.lookingThroughLocker = true;
			theLockers.gameObject.GetComponent<Animator> ().Play ("LockerOpen");
		}
	}
		
}
