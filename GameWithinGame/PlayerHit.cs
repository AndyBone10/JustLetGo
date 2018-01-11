using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour {

	private DeleteChildEnemies theChildDeleter;
	private ScoreScript theScoreController;
	void Start(){
		theChildDeleter = GameObject.Find ("AllEnemiesObject").GetComponent<DeleteChildEnemies> ();
		theScoreController = GameObject.Find ("ScoreController").GetComponent<ScoreScript> ();
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.layer == 15 || other.gameObject.layer == 13) {
			Destroy (gameObject);
			GameObject.Find ("SoundController").GetComponent<GameWithinGameSoundController> ().playerShipExplode.Play ();
			theChildDeleter.deleteAllEnemies ();
			theScoreController.score = 0;
		}
	}
}
