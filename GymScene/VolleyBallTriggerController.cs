using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolleyBallTriggerController : MonoBehaviour {

	public GameObject GymSceneControllerObject;
	private GymSceneController theGymSceneController;

	// Use this for initialization
	void Start () {
		theGymSceneController = GymSceneControllerObject.GetComponent<GymSceneController> ();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		
		Debug.Log ("other gameObject tag: " + other.gameObject.tag);
		if (other.gameObject.tag == "Player" ) { //8 == Player
			theGymSceneController.playerHittingBall = true;
			//theGymSceneController.StartCoroutine (theGymSceneController.WaitForHitAnim ());
		} 
		else if (other.gameObject.tag == "Bully") { //9 == Bully
			theGymSceneController.bullyHittingBall = true;
			//theGymSceneController.StartCoroutine (theGymSceneController.WaitForHitAnim ());

		}
	}

	void OnTriggerExit2D(Collider2D other){
		theGymSceneController.playerHittingBall = false;
		theGymSceneController.bullyHittingBall = false;

	}
}
