using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManPickup : MonoBehaviour {

	private ScoreScript theScoreController;

	void Start(){
		theScoreController = GameObject.Find ("ScoreController").GetComponent<ScoreScript> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.layer == 12) {
			GameObject.Find ("SoundController").GetComponent<GameWithinGameSoundController> ().humanPickupSound.Play ();
			theScoreController.score += 100;
			Destroy(gameObject);
		}
		if (other.gameObject.layer == 13) {
			theScoreController.score -= 25;
			Destroy (gameObject);
		}
		if (other.gameObject.layer == 14) {
			Destroy (gameObject);
		}
		if (other.gameObject.layer == 15) {
			Destroy (gameObject);
		}
	}
}
