﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAfterClassroomScene : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			SceneManager.LoadScene ("GymScene");
		}
	}
}
