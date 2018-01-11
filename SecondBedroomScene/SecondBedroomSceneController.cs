using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondBedroomSceneController : MonoBehaviour {

	public GameObject Letters;
	private Animator JAnimator;
	private Animator UAnimator;
	private Animator SAnimator;
	private Animator TAnimator;
	private Animator LAnimator;
	private Animator EAnimator;
	private Animator secondTAnimator;

	public GameObject Player;
	private Animator thePlayerAnimator;
	private Rigidbody2D thePlayerRigidbody;

	public bool moveToBed;
	public bool playerAtBed;
	private bool playerFlipped;
	private bool firstTPress;

	public GameObject Woman;
	private Animator theWomanAnimator;

	public GameObject diagonalToBed;
	public GameObject bedCollider;

	public GameObject PenInHand;
	private bool drawLetter;

	private bool coroutineStarted;
	//public GameObject SoundController;
	//private SecondBedroomSoundController theSoundController;

	// Use this for initialization
	void Start () {

		//theSoundController = SoundController.GetComponent<SecondBedroomSoundController> ();
		//playFBSound ();
		GameObject.Find ("GameMusic").GetComponent<MusicController> ().sad3.gameObject.SetActive (true);

		thePlayerRigidbody = Player.GetComponent<Rigidbody2D> ();
		thePlayerAnimator = Player.transform.GetChild (0).GetComponent<Animator> ();
		thePlayerAnimator.SetInteger ("PlayerState", 15);

		theWomanAnimator = Woman.GetComponent<Animator> ();
		theWomanAnimator.SetInteger ("WomanState", 3);

		JAnimator = Letters.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		UAnimator = Letters.transform.GetChild(0).GetChild(1).GetComponent<Animator>();
		SAnimator = Letters.transform.GetChild(0).GetChild(2).GetComponent<Animator>();
		TAnimator = Letters.transform.GetChild(0).GetChild(3).GetComponent<Animator>();
		LAnimator = Letters.transform.GetChild(0).GetChild(4).GetComponent<Animator>();
		EAnimator = Letters.transform.GetChild(0).GetChild(5).GetComponent<Animator>();
		secondTAnimator = Letters.transform.GetChild(0).GetChild(6).GetComponent<Animator>();

		JAnimator.SetBool ("LetterPressed", true);
		UAnimator.SetBool ("LetterPressed", true);
		SAnimator.SetBool ("LetterPressed", true);
		TAnimator.SetBool ("LetterPressed", true);
		LAnimator.SetBool ("LetterPressed", true);
		EAnimator.SetBool ("LetterPressed", true);

		StartCoroutine (SadAfterRoasting ());

	}

	// Update is called once per frame
	void Update () {

		if (drawLetter) {
			PenInHand.gameObject.SetActive (true);
			PenInHand.GetComponent<Animator> ().SetInteger ("HandAndPenState", 7);
			StartCoroutine (FadeInLetter ());
			drawLetter = false;

		}

		if (moveToBed == true) {
			if (Input.GetKey ("t")) {
				if (!playerFlipped) {
					Player.gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
					playerFlipped = true;
				}
				firstTPress = true;
				secondTAnimator.SetBool ("LetterPressed", true);
				thePlayerAnimator.SetInteger ("PlayerState", 3);
				thePlayerRigidbody.velocity = new Vector3 (1f, thePlayerRigidbody.velocity.y, 0f);

			} else if (firstTPress == true) {
				secondTAnimator.SetBool ("LetterPressed", false);
				thePlayerAnimator.SetInteger ("PlayerState", 16);
				thePlayerRigidbody.velocity = new Vector3 (0f, thePlayerRigidbody.velocity.y, 0f);
			}
		} 
		else if (playerAtBed) {
			secondTAnimator.SetBool ("LetterPressed", true);
			if (!coroutineStarted) {
				StartCoroutine (ClimbOntoBed ());
				coroutineStarted = true;
			}
		}
	}

	public IEnumerator SadAfterRoasting()
	{
		yield return new WaitForSeconds (30.0f);
		diagonalToBed.gameObject.SetActive (true);
		//Letters.transform.GetChild (0).GetChild (6).gameObject.SetActive (true);
		drawLetter = true;
		moveToBed = true;
		StopCoroutine (SadAfterRoasting ());
	}

	public IEnumerator ClimbOntoBed()
	{


		diagonalToBed.gameObject.SetActive(false);
		bedCollider.gameObject.SetActive(true);

		thePlayerAnimator.SetInteger ("PlayerState", 13);
	
		yield return new WaitForSeconds (20.0f);

		StartCoroutine (LookAtPhone ());
		StopCoroutine (ClimbOntoBed ());
	}

	IEnumerator LookAtPhone()
	{
		

		GameObject.Find ("SoundController").GetComponent<PhoneSceneSoundController> ().NotificationPop.Play ();
		yield return new WaitForSeconds (1.0f);
		thePlayerAnimator.SetInteger ("PlayerState", 17);
		yield return new WaitForSeconds (3.0f);
		SceneManager.LoadScene ("PhoneScene");
		StopCoroutine (LookAtPhone ());
	}

	IEnumerator FadeInLetter()
	{

		yield return new WaitForSeconds(3.0f);
		Letters.transform.GetChild (0).GetChild (6).gameObject.SetActive (true);
		StopCoroutine(FadeInLetter());
	}

	private void playFBSound(){
		//theSoundController.facebookMessage.Play ();

	}
}
