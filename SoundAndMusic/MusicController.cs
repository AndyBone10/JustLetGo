using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

	public AudioSource baseSound;
	public AudioSource sad1;
	public AudioSource sad2;
	public AudioSource sad3;
	public AudioSource sad4;
	public AudioSource desired;
	public AudioSource whiteNoise;

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}

}
