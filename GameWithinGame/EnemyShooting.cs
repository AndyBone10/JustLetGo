using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

	public GameObject bulletPrefab;

	public float fireDelay = 20f;
	private float cooldownTimer = 0;

	Transform player;

	// Update is called once per frame
	void Update () {

		if (player == null)
		{
			GameObject go = GameObject.FindWithTag ("PlayerShip");

			if (go != null) 
			{
				player = go.transform;
			}
		}

		cooldownTimer -= Time.deltaTime;

		if (cooldownTimer <= 0 && player != null && Vector3.Distance(transform.position, player.position) < 4) 
		{
			//Shoot
			cooldownTimer = fireDelay;

			Instantiate (bulletPrefab, transform.position, transform.rotation);
		
		}
	}
}
