using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerSceneController : MonoBehaviour {

	public GameObject Letters;
	private Animator JAnimator;
	private Animator UAnimator;
	private Animator SAnimator;
	private Animator PlayerAnimator;
	public GameObject Player;
	public bool lookingThroughLocker;
	public GameObject book;
	public bool stopMovement;
	private Rigidbody2D playerRigidbody;
	public bool bullyGone;

	public GameObject PenInHand;
	private bool drawLetter;



	// Use this for initialization
	void Start () {
		book.SetActive (false);
		playerRigidbody = Player.GetComponent<Rigidbody2D> ();

		JAnimator = Letters.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		UAnimator = Letters.transform.GetChild(0).GetChild(1).GetComponent<Animator>();
		SAnimator = Letters.transform.GetChild(0).GetChild(2).GetComponent<Animator>();

		PlayerAnimator = Player.transform.GetChild(0).GetComponent<Animator> ();
		PlayerAnimator.SetInteger ("PlayerState", 2);

	}

	// Update is called once per frame
	void Update(){
		

		if (drawLetter) {
			PenInHand.gameObject.SetActive (true);
			PenInHand.GetComponent<Animator> ().SetInteger ("HandAndPenState", 3);
			StartCoroutine (FadeInLetter ());
			drawLetter = false;

		}

		if (bullyGone) {
			//Debug.Log (1);
			//Letters.transform.GetChild (0).GetChild (2).gameObject.SetActive (true);
			drawLetter = true;
		}

		if (lookingThroughLocker && stopMovement == false) {
			Debug.Log (2);

			StartCoroutine (WaitForLocker ());
			PlayerAnimator.SetInteger ("PlayerState", 5);

		}

		else if (Letters.transform.GetChild (0).GetChild (0).gameObject.activeSelf == true 
			&& Letters.transform.GetChild (0).GetChild (1).gameObject.activeSelf == true
			&& Letters.transform.GetChild (0).GetChild (2).gameObject.activeSelf == false && stopMovement == false && bullyGone == false) {

			Debug.Log (3);


			JAnimator.SetBool ("LetterPressed", true);
		

			if (Input.GetKey ("u")) {
				UAnimator.SetBool ("LetterPressed", true);
				PlayerAnimator.SetInteger ("PlayerState", 3);
				playerRigidbody.velocity = new Vector3 (1f, playerRigidbody.velocity.y, 0f);
			} else {
				UAnimator.SetBool ("LetterPressed", false);
				PlayerAnimator.SetInteger ("PlayerState", 2);
				playerRigidbody.velocity = new Vector3 (0f, playerRigidbody.velocity.y, 0f);
			}

		}

		else if (Letters.transform.GetChild (0).GetChild (0).gameObject.activeSelf == true 
			&& Letters.transform.GetChild (0).GetChild (1).gameObject.activeSelf == true
			&& Letters.transform.GetChild (0).GetChild (2).gameObject.activeSelf == true && stopMovement == false && bullyGone == true) {

			JAnimator.SetBool ("LetterPressed", true);
			UAnimator.SetBool ("LetterPressed", true);

			Debug.Log (4);


			if (Input.GetKey ("s")) {
				SAnimator.SetBool ("LetterPressed", true);
				PlayerAnimator.SetInteger ("PlayerState", 3);
				playerRigidbody.velocity = new Vector3 (1f, playerRigidbody.velocity.y, 0f);

			} else {
				SAnimator.SetBool ("LetterPressed", false);
				PlayerAnimator.SetInteger ("PlayerState", 2);
				playerRigidbody.velocity = new Vector3 (0f, playerRigidbody.velocity.y, 0f);
			}

		}
	}

	public IEnumerator WaitForLocker()
	{
		yield return new WaitForSeconds(2.0f);
		lookingThroughLocker = false;
		GameObject.Find("Lockers").GetComponent<Animator> ().Play ("LockerClose");
		book.SetActive (true);
		StopCoroutine(WaitForLocker());
	}

	IEnumerator FadeInLetter()
	{
		yield return new WaitForSeconds(3.0f);
		Letters.transform.GetChild (0).GetChild (2).gameObject.SetActive (true);
		StopCoroutine(FadeInLetter());
	}
}
