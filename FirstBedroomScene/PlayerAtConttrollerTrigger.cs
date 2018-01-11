using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtConttrollerTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			GameObject.Find("FirstBedroomSceneController").GetComponent<FirstBedroomSceneController>().playerAtController = true;
		}
	}
}
