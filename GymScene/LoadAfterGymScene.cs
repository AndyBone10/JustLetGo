using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAfterGymScene : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			SceneManager.LoadScene ("LunchScene");
		}
	}
}
