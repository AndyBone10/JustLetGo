using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameTimer : MonoBehaviour {

	private float timeLeft;
	private bool startTimer;

	// Use this for initialization
	void Start () {
		timeLeft = 80.0f;
	}
	
	// Update is called once per frame
	void Update () {

		FadeDownVolume ();

		if (Input.GetKey ("j")) {
			startTimer = true;
		}
		if (startTimer) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				SceneManager.LoadScene ("SecondBedroomScene");
			}
		}
	}

	void FadeDownVolume(){
		AudioSource sadMusic = GameObject.Find ("GameMusic").GetComponent<MusicController> ().desired;

		if (sadMusic.volume > 0) {
			sadMusic.volume -= 0.01f * Time.deltaTime;

		}

		if (sadMusic.volume <= 0) {
			GameObject.Find ("GameMusic").GetComponent<MusicController> ().desired.gameObject.SetActive (true);
		}
	}
}
