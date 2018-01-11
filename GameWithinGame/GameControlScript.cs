using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlScript : MonoBehaviour {

	public 	GameObject theControls;
	private bool controlsShown;
	public GameObject W,A,S,D,J;
	public GameObject LeftArm,RightArm;
	private float armSpeed;
	public GameObject titleScreen;

	// Use this for initialization
	void Start () {
		armSpeed = 0.5f;

	}
	
	// Update is called once per frame
	void Update () {

		if (!controlsShown) {
			if (Input.GetKey ("j")) {
				RightArm.transform.position = Vector3.MoveTowards (RightArm.transform.position, J.transform.position, armSpeed);

			}

			if(RightArm.transform.position == J.transform.position){
				theControls.GetComponent<Animator> ().SetBool ("controlsShown", true);
				controlsShown = true;
				StartCoroutine (WaitForAnim());
				//titleScreen.SetActive (true);

			}

			if (Input.GetKey ("w")) {
				LeftArm.transform.position = Vector3.MoveTowards (LeftArm.transform.position, W.transform.position, armSpeed);
			}
			if (Input.GetKey ("a")) {
				LeftArm.transform.position = Vector3.MoveTowards (LeftArm.transform.position, A.transform.position, armSpeed);
			}
			if (Input.GetKey ("s")) {
				LeftArm.transform.position = Vector3.MoveTowards (LeftArm.transform.position, S.transform.position, armSpeed);
			}
			if (Input.GetKey ("d")) {
				LeftArm.transform.position = Vector3.MoveTowards (LeftArm.transform.position, D.transform.position, armSpeed);
			}

		}

	}

	public IEnumerator WaitForAnim()
	{
		yield return new WaitForSeconds(1.0f);
		titleScreen.SetActive (true);
		StopCoroutine(WaitForAnim());
	}
}
