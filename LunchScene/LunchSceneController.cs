using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunchSceneController : MonoBehaviour {

	public GameObject Player;

	public GameObject NPC1, NPC2, NPC3;

	public GameObject seat;

	private Animator thePlayerAnimator;

	private Animator theNPC1Animator;
	private Animator theNPC2Animator;
	private Animator theNPC3Animator;


	public GameObject Letters;
	private Animator JAnimator;
	private Animator UAnimator;
	private Animator SAnimator;
	private Animator TAnimator;
	private Animator LAnimator;
	private Animator EAnimator;


	private Rigidbody2D thePlayerRigidbody;

	public bool playerAtTable;

	private bool timeToGoPal;

	private bool coroutineStarted;

	public GameObject PenInHand;
	private bool drawLetter;

	private AudioSource desiredSong;
	public AudioSource classLaugh;
	private bool fadeDownLaugh;


	// Use this for initialization
	void Start () {

		desiredSong = GameObject.Find ("GameMusic").GetComponent<MusicController> ().desired;

		thePlayerRigidbody = Player.GetComponent<Rigidbody2D> ();
		thePlayerAnimator = Player.transform.GetChild (0).GetComponent<Animator> ();
		thePlayerAnimator.SetInteger ("PlayerState", 2);

		theNPC1Animator = NPC1.transform.GetChild (0).GetComponent<Animator> ();
		theNPC2Animator = NPC2.transform.GetChild (0).GetComponent<Animator> ();
		theNPC3Animator = NPC3.transform.GetChild (0).GetComponent<Animator> ();


		JAnimator = Letters.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		UAnimator = Letters.transform.GetChild(0).GetChild(1).GetComponent<Animator>();
		SAnimator = Letters.transform.GetChild(0).GetChild(2).GetComponent<Animator>();
		TAnimator = Letters.transform.GetChild(0).GetChild(3).GetComponent<Animator>();
		LAnimator = Letters.transform.GetChild(0).GetChild(4).GetComponent<Animator>();
		EAnimator = Letters.transform.GetChild(0).GetChild(5).GetComponent<Animator>();

		JAnimator.SetBool ("LetterPressed", true);
		UAnimator.SetBool ("LetterPressed", true);
		SAnimator.SetBool ("LetterPressed", true);
		TAnimator.SetBool ("LetterPressed", true);
	}
	
	// Update is called once per frame
	void Update () {

		if (fadeDownLaugh) {
			if (classLaugh.volume > 0) {
				classLaugh.volume -= 0.05f * Time.deltaTime;

			}
		}

		if (drawLetter) {
			PenInHand.gameObject.SetActive (true);
			PenInHand.GetComponent<Animator> ().SetInteger ("HandAndPenState", 6);
			StartCoroutine (FadeInLetter ());
			drawLetter = false;

		}

		if (timeToGoPal) {

			if (Input.GetKey ("e")) {
				seat.gameObject.SetActive (false);
				EAnimator.SetBool ("LetterPressed", true);
				thePlayerAnimator.SetInteger ("PlayerState", 3);
				thePlayerRigidbody.velocity = new Vector3 (1f, thePlayerRigidbody.velocity.y, 0f);

			} else if(seat.gameObject.activeSelf == false) {
				EAnimator.SetBool ("LetterPressed", false);
				thePlayerAnimator.SetInteger ("PlayerState", 2);
				thePlayerRigidbody.velocity = new Vector3 (0f, thePlayerRigidbody.velocity.y, 0f);
			}
		}

		else if (!playerAtTable) {
			if (Input.GetKey ("l")) {
				
				LAnimator.SetBool ("LetterPressed", true);
				thePlayerAnimator.SetInteger ("PlayerState", 30);
				thePlayerRigidbody.velocity = new Vector3 (1f, thePlayerRigidbody.velocity.y, 0f);

			} else {
				LAnimator.SetBool ("LetterPressed", false);
				thePlayerAnimator.SetInteger ("PlayerState", 2);
				thePlayerRigidbody.velocity = new Vector3 (0f, thePlayerRigidbody.velocity.y, 0f);
			}
		}
		else if (playerAtTable) {
			if (!coroutineStarted) {
				StartCoroutine (WaitForTableClimb ());
				coroutineStarted = true;
			}
		}
	}

	public IEnumerator WaitForTableClimb()
	{
		seat.gameObject.SetActive (true);
		thePlayerAnimator.SetInteger ("PlayerState", 11);
		yield return new WaitForSeconds (1.0f);
		StartCoroutine (SittingAtTable ());
		//playerAtTable = false;
		StopCoroutine (WaitForTableClimb ());
	}

	public IEnumerator SittingAtTable()
	{
		classLaugh.Play ();
		fadeDownLaugh = true;
		thePlayerAnimator.SetInteger ("PlayerState", 12);
		yield return new WaitForSeconds (2.0f);

		NPC2.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

		theNPC1Animator.SetInteger ("NPCState", 1);
		theNPC2Animator.SetInteger ("NPCState", 1);
		theNPC3Animator.SetInteger ("NPCState", 1);
		StartCoroutine (SadAtTable ());
		StopCoroutine (SittingAtTable ());
	}

	public IEnumerator SadAtTable()
	{
		thePlayerAnimator.SetInteger ("PlayerState", 13);
		yield return new WaitForSeconds (4.0f);
		desiredSong.pitch = .8f;
		yield return new WaitForSeconds (8.0f);
		desiredSong.pitch = .6f;


		timeToGoPal = true;
		LAnimator.SetBool ("LetterPressed", true);

		drawLetter = true;
		StopCoroutine (SadAtTable ());
	}

	IEnumerator FadeInLetter()
	{
		yield return new WaitForSeconds(3.0f);
		Letters.transform.GetChild (0).GetChild (5).gameObject.SetActive (true);
		StopCoroutine(FadeInLetter());
	}


}
