using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleController : MonoBehaviour {

	public GameObject Letters;
	private Animator JAnimator;
	private Animator UAnimator;
	private Animator PlayerAnimator;
	public GameObject Player;

	private Rigidbody2D playerRigidbody;
	public bool beenSplashed;
	public bool stopMovement;

	public GameObject PenInHand;
	public bool drawLetter;



	// Use this for initialization
	void Start () {
		playerRigidbody = Player.GetComponent<Rigidbody2D> ();

		JAnimator = Letters.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		UAnimator = Letters.transform.GetChild(0).GetChild(1).GetComponent<Animator>();
	
		PlayerAnimator = Player.transform.GetChild(0).GetComponent<Animator> ();
		PlayerAnimator.SetInteger ("PlayerState", 2);

	}
	
	// Update is called once per frame
	void Update(){

		if (drawLetter) {
			PenInHand.gameObject.SetActive (true);
			PenInHand.GetComponent<Animator> ().SetInteger ("HandAndPenState", 2);
			StartCoroutine (FadeInLetter ());
			drawLetter = false;

		}
			
		if (beenSplashed) {
			JAnimator.SetBool ("LetterPressed", true);

			if (Input.GetKey ("u")) {
				UAnimator.SetBool ("LetterPressed", true);
				PlayerAnimator.SetInteger ("PlayerState", 3);
				playerRigidbody.velocity = new Vector3 (1f, playerRigidbody.velocity.y, 0f);
			} else {
				UAnimator.SetBool ("LetterPressed", false);
				PlayerAnimator.SetInteger ("PlayerState", 4);
				playerRigidbody.velocity = new Vector3 (0f, playerRigidbody.velocity.y, 0f);
			}
		}
		else if (Letters.transform.GetChild (0).GetChild (0).gameObject.activeSelf == true
		    && Letters.transform.GetChild (0).GetChild (1).gameObject.activeSelf == false && stopMovement == false) {
			if (Input.GetKey ("j")) {
				JAnimator.SetBool ("LetterPressed", true);
				PlayerAnimator.SetInteger ("PlayerState", 3);
				playerRigidbody.velocity = new Vector3 (1f, playerRigidbody.velocity.y, 0f);
			} else {
				JAnimator.SetBool ("LetterPressed", false);

				PlayerAnimator.SetInteger ("PlayerState", 2);
				playerRigidbody.velocity = new Vector3 (0f, playerRigidbody.velocity.y, 0f);
			}
				
		} else if (Letters.transform.GetChild (0).GetChild (1).gameObject.activeSelf == true) {
			
			JAnimator.SetBool ("LetterPressed", true);
			
			if (Input.GetKey ("u")) {
				UAnimator.SetBool ("LetterPressed", true);
				PlayerAnimator.SetInteger ("PlayerState", 3);
				playerRigidbody.velocity = new Vector3 (1f, playerRigidbody.velocity.y, 0f);
			} else {
				UAnimator.SetBool ("LetterPressed", false);
				PlayerAnimator.SetInteger ("PlayerState", 4);
				playerRigidbody.velocity = new Vector3 (0f, playerRigidbody.velocity.y, 0f);
			}

		}
	}

	IEnumerator FadeInLetter()
	{
		yield return new WaitForSeconds(3.0f);
		Letters.transform.GetChild (0).GetChild (1).gameObject.SetActive (true);
		StopCoroutine(FadeInLetter());
	}
}
