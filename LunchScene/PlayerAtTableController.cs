using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtTableController : MonoBehaviour {



	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			GameObject.Find ("LunchSceneController").GetComponent<LunchSceneController> ().playerAtTable = true;
		}
	}
}
