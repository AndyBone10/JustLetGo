using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ThirdBedroomSceneController : MonoBehaviour {

	public GameObject Letters;

	private Animator JAnimator;
	private Animator UAnimator;
	private Animator SAnimator;
	private Animator TAnimator;
	private Animator LAnimator;
	private Animator EAnimator;
	private Animator secondTAnimator;
	private Animator GAnimator;
	private Animator OAnimator;


	public GameObject Player;
	private Animator thePlayerAnimator;
	private Rigidbody2D thePlayerRigidbody;
	public GameObject leftLowerArm;

	public GameObject creditsPanel;

	public GameObject coiledRope;

	public GameObject connectedRope;

	private GameObject theNooseObject;


	public bool doneCrying;
	public bool aboutToTieKnot;
	public bool movingToMiddleBed;
	public bool coroutineStarted;
	public bool tyingKnot;
	public bool nooseTie;
	public bool pushingBed;
	private bool gettingBackOnBed;
	private bool inNoose;
	private bool jumped;
	private bool noMore;

	public GameObject sideBedCollider;

	private Animator theBedAnimator;

	public GameObject theKnot;

	public GameObject knotToCurtainTrigger;

	public GameObject bedCollider;
	public GameObject underBedCollider;
	//public GameObject riseToBedCollider;
	public GameObject bed;

	public GameObject PenInHand;
	private bool drawLetter;
	private bool drawSecondLetter;

	private bool fadeTransition;
	private bool fadeDownVolume;

	public GameObject creditCanvas;

	// Use this for initialization
	void Start () {

		theNooseObject = connectedRope.transform.GetChild (14).gameObject;
		Debug.Log (theNooseObject.gameObject.name);


		theBedAnimator = bed.GetComponent<Animator> ();

		thePlayerRigidbody = Player.GetComponent<Rigidbody2D> ();
		thePlayerAnimator = Player.transform.GetChild (0).GetComponent<Animator> ();
		thePlayerAnimator.SetInteger ("PlayerState", 18);

		StartCoroutine (WaitForPhoneThrow ());


		JAnimator = Letters.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		UAnimator = Letters.transform.GetChild(0).GetChild(1).GetComponent<Animator>();
		SAnimator = Letters.transform.GetChild(0).GetChild(2).GetComponent<Animator>();
		TAnimator = Letters.transform.GetChild(0).GetChild(3).GetComponent<Animator>();
		LAnimator = Letters.transform.GetChild(0).GetChild(4).GetComponent<Animator>();
		EAnimator = Letters.transform.GetChild(0).GetChild(5).GetComponent<Animator>();
		secondTAnimator = Letters.transform.GetChild(0).GetChild(6).GetComponent<Animator>();
		GAnimator = Letters.transform.GetChild(0).GetChild(7).GetComponent<Animator>();
		OAnimator = Letters.transform.GetChild(0).GetChild(8).GetComponent<Animator>();


		JAnimator.SetBool ("LetterPressed", true);
		UAnimator.SetBool ("LetterPressed", true);
		SAnimator.SetBool ("LetterPressed", true);
		TAnimator.SetBool ("LetterPressed", true);
		LAnimator.SetBool ("LetterPressed", true);
		EAnimator.SetBool ("LetterPressed", true);
		secondTAnimator.SetBool ("LetterPressed", true);

	}

	
	// Update is called once per frame
	void Update () {

		if (fadeTransition) {
			//sadMusic.volume -= 0.05f * Time.deltaTime;
			if (GameObject.Find ("GameMusic").GetComponent<MusicController> ().whiteNoise.volume < 0.2) {
				GameObject.Find ("GameMusic").GetComponent<MusicController> ().whiteNoise.volume += 0.01f * Time.deltaTime;
			}
			GameObject.Find ("GameMusic").GetComponent<MusicController> ().sad3.volume -= 0.02f * Time.deltaTime;

			Camera.main.GetComponent<ScreenTransitionImageEffect>().maskValue += 0.02f * Time.deltaTime;
		
		}

		if (fadeDownVolume) {
			
			GameObject.Find ("GameMusic").GetComponent<MusicController> ().whiteNoise.volume -= 0.02f * Time.deltaTime;
			if (GameObject.Find ("GameMusic").GetComponent<MusicController> ().whiteNoise.volume < 0) {
				GameObject.Find ("GameMusic").GetComponent<MusicController> ().whiteNoise.gameObject.SetActive (false);
				fadeDownVolume = false;
				//creditsPanel.gameObject.SetActive (true);
			}

		}
		if (drawLetter) {
			PenInHand.gameObject.SetActive (true);
			PenInHand.GetComponent<Animator> ().SetInteger ("HandAndPenState", 8);
			StartCoroutine (FadeInLetter ());
			drawLetter = false;

		}

		if (drawSecondLetter) {
			
			PenInHand.GetComponent<Animator> ().SetInteger ("HandAndPenState", 9);
			StartCoroutine (FadeInSecondLetter ());
			drawSecondLetter = false;

		}

		if (!doneCrying) {
			Debug.Log (1);
			if (Input.GetKey ("g") && Letters.transform.GetChild (0).GetChild (7).gameObject.activeSelf == true) {
					thePlayerAnimator.SetInteger ("PlayerState", 20);
				
				bedCollider.gameObject.SetActive (false);
				underBedCollider.gameObject.SetActive (true);
				if (!coroutineStarted) {
					StartCoroutine (WaitForLookingUnderBed ());
					coroutineStarted = true;
				}
				GAnimator.SetBool ("LetterPressed", true);
				//thePlayerAnimator.SetInteger ("PlayerState", 3);
				//thePlayerRigidbody.velocity = new Vector3 (1f, thePlayerRigidbody.velocity.y, 0f);
				doneCrying = true;
			} 
		} else if (nooseTie) {
			Debug.Log (2);

			thePlayerAnimator.SetInteger ("PlayerState", 25);
			if (!coroutineStarted) {
				StartCoroutine (WaitForNooseTie ());
				coroutineStarted = true;
			}

		} else if (tyingKnot) {

			Debug.Log (3);

			thePlayerAnimator.SetInteger ("PlayerState", 24);
			if (!coroutineStarted) {
				StartCoroutine (WaitForKnotTie ());
				coroutineStarted = true;
			}

		} else if (movingToMiddleBed == true) {

			Debug.Log (4);


			if (Input.GetKey ("g")) {
				thePlayerAnimator.SetInteger ("PlayerState", 23);

				GAnimator.SetBool ("LetterPressed", true);
				thePlayerRigidbody.velocity = new Vector3 (1f, thePlayerRigidbody.velocity.y, 0f);
				//thePlayerAnimator.SetInteger ("PlayerState", 3);
				//thePlayerRigidbody.velocity = new Vector3 (1f, thePlayerRigidbody.velocity.y, 0f);
			} else {
				thePlayerAnimator.SetInteger ("PlayerState", 22);
				GAnimator.SetBool ("LetterPressed", false);
				thePlayerRigidbody.velocity = new Vector3 (0f, thePlayerRigidbody.velocity.y, 0f);

			}
		} else if (aboutToTieKnot == true && doneCrying) {

			Debug.Log (5);


			if (Input.GetKey ("g")) {
				thePlayerAnimator.SetInteger ("PlayerState", 23);

				GAnimator.SetBool ("LetterPressed", true);
				thePlayerRigidbody.velocity = new Vector3 (-1f, thePlayerRigidbody.velocity.y, 0f);
				//thePlayerAnimator.SetInteger ("PlayerState", 3);
				//thePlayerRigidbody.velocity = new Vector3 (1f, thePlayerRigidbody.velocity.y, 0f);
			} else {
				thePlayerAnimator.SetInteger ("PlayerState", 22);
				GAnimator.SetBool ("LetterPressed", false);
				thePlayerRigidbody.velocity = new Vector3 (0f, thePlayerRigidbody.velocity.y, 0f);

			}
		} else if (pushingBed) {

			Debug.Log (6);


			//if (Input.GetKey ("g")) {
		
			theBedAnimator.SetInteger ("bedPushed", 1);
			thePlayerAnimator.SetInteger ("PlayerState", 26);



			GAnimator.SetBool ("LetterPressed", true);
			//thePlayerRigidbody.velocity = new Vector3 (-0.5f, thePlayerRigidbody.velocity.y, 0f);
			Player.transform.position = new Vector3 (bed.transform.position.x + 3.0f, Player.transform.position.y, Player.transform.position.z);

			if (!coroutineStarted) {
				StartCoroutine (WaitForBedTie ());
				coroutineStarted = true;
			}
			//thePlayerAnimator.SetInteger ("PlayerState", 3);
			//thePlayerRigidbody.velocity = new Vector3 (1f, thePlayerRigidbody.velocity.y, 0f);
			//} 
		} else if (gettingBackOnBed) {
			if (Input.GetKey ("o") && !inNoose) {
				OAnimator.SetBool ("LetterPressed", true);
				theBedAnimator.SetInteger ("bedPushed", 2);
				theNooseObject.GetComponent<SpriteRenderer> ().sortingOrder = 1;

				if (inNoose == false) {
					thePlayerAnimator.SetInteger ("PlayerState", 27);
					theNooseObject.GetComponent<SpriteRenderer> ().sortingOrder = 1;

					inNoose = true;
				}

				//nooseTieAnimation
			} else if (inNoose) {
				theNooseObject.GetComponent<SpriteRenderer> ().sortingOrder = 1;

				theBedAnimator.SetInteger ("bedPushed", 3);
				Player.transform.position = new Vector3 (theNooseObject.transform.position.x - 0.67f, theNooseObject.transform.position.y - 0.65f, theNooseObject.transform.position.z);

				thePlayerAnimator.SetInteger ("PlayerState", 28);
			} 
			else {
				theNooseObject.GetComponent<SpriteRenderer> ().sortingOrder = 1;

				thePlayerAnimator.SetInteger ("PlayerState", 22);

			}

		}


	}

	public IEnumerator WaitForPhoneThrow()
	{
		yield return new WaitForSeconds (3.6f);
		thePlayerAnimator.SetInteger ("PlayerState", 19);
		StartCoroutine (WaitForCrying ());
		StopCoroutine (WaitForPhoneThrow ());
	}
	public IEnumerator WaitForCrying()
	{
		yield return new WaitForSeconds (30.0f);

		//Letters.transform.GetChild (0).GetChild (7).gameObject.SetActive (true);
		drawLetter = true;

		StopCoroutine (WaitForCrying ());
	}
	public IEnumerator WaitForLookingUnderBed(){

		Vector3 newPos = new Vector3 (1.0f, 0.0f, 0.0f);
		Player.gameObject.transform.position += newPos;
		Player.gameObject.transform.rotation = Quaternion.Euler (0, -180, 0);
		yield return new WaitForSeconds (0.3f);
		bed.GetComponent<SpriteRenderer> ().sortingOrder = 10;
		GAnimator.SetBool ("LetterPressed", true);
		yield return new WaitForSeconds (12.5f);
		coroutineStarted = false;

		StartCoroutine (GetOnBed ());
		StopCoroutine (WaitForLookingUnderBed ());

	}

	public IEnumerator GetOnBed(){
		GameObject.Find ("GameMusic").GetComponent<MusicController> ().sad3.pitch = 0.56f;

		bed.GetComponent<SpriteRenderer> ().sortingOrder = -10;

		//riseToBedCollider.gameObject.SetActive (true);

		thePlayerAnimator.SetInteger ("PlayerState", 21);
		coiledRope.gameObject.SetActive (true);
		yield return new WaitForSeconds (2.0f);
		leftLowerArm.GetComponent<SpriteRenderer> ().sortingOrder = -4;
		GAnimator.SetBool ("LetterPressed", false);
	
		aboutToTieKnot = true;
		doneCrying = true;
		StopCoroutine (GetOnBed ());
	}

	public IEnumerator WaitForKnotTie()
	{
		yield return new WaitForSeconds (5.3f);
		tyingKnot = false;
		Vector3 newPos = new Vector3 (-0.95f, 0.0f, 0.0f);
		Player.gameObject.transform.position += newPos;
		Player.gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
		theKnot.gameObject.SetActive (true);
		connectedRope.gameObject.SetActive (true);
		movingToMiddleBed = true;
		knotToCurtainTrigger.gameObject.SetActive (true);
		StopCoroutine (WaitForKnotTie ());
	}

	public IEnumerator WaitForNooseTie()
	{
		yield return new WaitForSeconds (5.5f);

		nooseTie = false;
		thePlayerAnimator.SetInteger ("PlayerState", 22);

		coiledRope.gameObject.SetActive (false);
		knotToCurtainTrigger.gameObject.SetActive (false);
		StopCoroutine (WaitForNooseTie ());
	}

	public IEnumerator WaitForBedTie()
	{
		yield return new WaitForSeconds (3.0f);
		pushingBed = false;
		coroutineStarted = false;
		thePlayerAnimator.SetInteger ("PlayerState", 21);
		Player.transform.position = new Vector3 (Player.transform.position.x - 2.0f, Player.transform.position.y, Player.transform.position.z);
		yield return new WaitForSeconds (2.0f);
		Player.gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
		gettingBackOnBed = true;
		//Letters.transform.GetChild (0).GetChild (8).gameObject.SetActive (true);
		drawSecondLetter = true;
		StartCoroutine (StartTransitionTimer ());

		StopCoroutine (WaitForBedTie ());
	}

	public IEnumerator StartTransitionTimer()
	{
		yield return new WaitForSeconds (40.0f);
		GameObject.Find ("GameMusic").GetComponent<MusicController> ().whiteNoise.gameObject.SetActive (true);
		fadeTransition = true;
		StartCoroutine (StartFadeTimer ());

		StopCoroutine (StartTransitionTimer ());
	
	}

	public IEnumerator StartFadeTimer()
	{
		
		yield return new WaitForSeconds (60.0f);
		fadeDownVolume = true;
		fadeTransition = false;
		yield return new WaitForSeconds (7.0f);
		creditCanvas.gameObject.SetActive (true);
		StopCoroutine (StartFadeTimer ());
	}

	IEnumerator FadeInLetter()
	{
		yield return new WaitForSeconds(3.0f);
		Letters.transform.GetChild (0).GetChild (7).gameObject.SetActive (true);
		StopCoroutine(FadeInLetter());
	}

	IEnumerator FadeInSecondLetter()
	{
		yield return new WaitForSeconds(3.0f);
		Letters.transform.GetChild (0).GetChild (8).gameObject.SetActive (true);
		StopCoroutine(FadeInSecondLetter());
	}



}
