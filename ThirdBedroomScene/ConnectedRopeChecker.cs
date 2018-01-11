using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedRopeChecker : MonoBehaviour {

	public GameObject ropeCoil;
	Vector3 ropeSpawnPoint;
	// Use this for initialization

	void Start () {
		ropeSpawnPoint = new Vector3 (ropeCoil.transform.position.x + -0.3f, ropeCoil.transform.position.y, ropeCoil.transform.position.z);
		//Debug.Log ("ROPE SPAWN POINT X: " + ropeSpawnPoint.x);
	}
	
	// Update is called once per frame
	void Update () {
		ropeSpawnPoint = new Vector3 (ropeCoil.transform.position.x + -0.3f, ropeCoil.transform.position.y, ropeCoil.transform.position.z);

		foreach(Transform child in transform)
		{
			//Debug.Log ("CHILD X: " + child.transform.position.x);

			if (child.transform.position.x < ropeSpawnPoint.x) {
				child.gameObject.SetActive (true);
			}
		}
	}
}
