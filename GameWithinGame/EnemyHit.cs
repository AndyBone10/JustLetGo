using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour {

	public GameObject spacemanPrefab;
	private ScoreScript theScoreController;

	void Start(){
		theScoreController = GameObject.Find ("ScoreController").GetComponent<ScoreScript> ();

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.layer == 12) {
			Destroy (other.gameObject);
		}
		if (other.gameObject.layer == 14) { // hit by player bullet
			GameObject.Find ("SoundController").GetComponent<GameWithinGameSoundController> ().enemyShipExplode.Play();
			Destroy (gameObject);

			theScoreController.score += 25;
			int rand = Random.Range (1, 9);
			if (rand == 4) {
				Instantiate (spacemanPrefab, transform.position, transform.rotation);
			}
		
		}
	}
}

