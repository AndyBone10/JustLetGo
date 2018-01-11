using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashPlayer : MonoBehaviour {

	public GameObject BeemerWanker;


	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			BeemerWanker.gameObject.GetComponent<Animator> ().Play ("CarDrivePast");
			GameObject.Find ("SoundController").GetComponent<PuddleSoundController> ().carHorn.Play ();
			GameObject.Find ("GameMusic").GetComponent<MusicController> ().sad1.gameObject.SetActive (true);
			GameObject.Find ("GameMusic").GetComponent<MusicController> ().baseSound.gameObject.SetActive (false);


		}
	}
}
