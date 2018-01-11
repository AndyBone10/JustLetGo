using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	public GameObject bulletPrefab;

	public float fireDelay = 0.25f;
	private float cooldownTimer = 0;
	
	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime;

		if (Input.GetKey ("j") && cooldownTimer <= 0) {
			//Shoot
			cooldownTimer = fireDelay;
			Instantiate (bulletPrefab, transform.position, transform.rotation);
			GameObject.Find ("SoundController").GetComponent<GameWithinGameSoundController> ().playerShootSound.Play ();

		}
	}
}
