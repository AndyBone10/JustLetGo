using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManMovement : MonoBehaviour {

	public float rotSpeed;
	public float timer = 2f;
	// Use this for initialization
	void Start () {
		gameObject.layer = 16;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if (timer <= 0) {
			gameObject.layer = 0;
		}


		Quaternion rot = transform.rotation;

		float z = rot.eulerAngles.z;

		z -= rotSpeed * Time.deltaTime;

		rot = Quaternion.Euler (0, 0, z);

		transform.rotation = rot;
	}
}
