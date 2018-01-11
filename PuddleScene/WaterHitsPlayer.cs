using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterHitsPlayer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "BeemerWanker") {
			this.gameObject.GetComponent<Animator> ().Play ("PuddleSplash");
			GameObject.Find ("Player").transform.GetChild(0).GetComponent<Animator> ().SetInteger ("PlayerState", 4);
			GameObject.Find ("SoundController").GetComponent<PuddleSoundController> ().waterSplash.Play ();

			StartCoroutine (WaitingWhileWet ());
		}
	}

	IEnumerator WaitingWhileWet()
	{
		GameObject.Find ("PuddleController").GetComponent<PuddleController> ().stopMovement = true;
		yield return new WaitForSeconds(4.0f);
		GameObject.Find ("PuddleController").GetComponent<PuddleController> ().beenSplashed = true;
		GameObject.Find ("PuddleController").GetComponent<PuddleController> ().drawLetter = true;
		StopCoroutine(WaitingWhileWet());
	}
}
