using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public GameObject allEnemiesObject;

	float spawnDistance = 10.0f;

	private float enemyRate = 6;
	private float nextEnemy = 1;
	
	// Update is called once per frame
	void Update () {
		nextEnemy -= Time.deltaTime;

		if (nextEnemy <= 0) {
			nextEnemy = enemyRate;
			Vector3 offset = Random.onUnitSphere;

			offset.z = 0;
			offset = offset.normalized * spawnDistance;

			GameObject enemy = Instantiate (enemyPrefab, transform.position + offset, Quaternion.identity);
			enemy.transform.parent = allEnemiesObject.transform;
		}
	}
}
