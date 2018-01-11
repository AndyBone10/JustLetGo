using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakfastController : MonoBehaviour {

	public GameObject line1,line2,line3,line4;
	public GameObject OldMan, Woman, Player;
	private Animator OldManAnimator, WomanAnimator, PlayerAnimator;
	private int manState, womanState, playerState;
	private bool movePlayer;
	public GameObject Letters;
	private Animator JAnimator;
	public GameObject Chair;
	public GameObject SoundController;
	private BreakfastSoundController theSoundController;

	private bool drawLetter;

	private Rigidbody2D playerRigidbody;

	public GameObject PenInHand;

	public float UIHandSpeed;

	// Use this for initialization
	void Start () {

		theSoundController = SoundController.GetComponent<BreakfastSoundController> ();

		playerRigidbody = Player.GetComponent<Rigidbody2D> ();

		JAnimator = Letters.transform.GetChild(0).GetChild(0).GetComponent<Animator>();

		OldManAnimator = OldMan.GetComponent<Animator> ();
		WomanAnimator = Woman.GetComponent<Animator> ();
		PlayerAnimator = Player.transform.GetChild(0).GetComponent<Animator> ();


		StartCoroutine(Starting());
	}

	void Update(){

		if (drawLetter) {
			PenInHand.gameObject.SetActive (true);
			PenInHand.GetComponent<Animator> ().SetInteger ("HandAndPenState", 1);
			StartCoroutine (FadeInLetter ());
			drawLetter = false;

		}

		if (Letters.transform.GetChild (0).GetChild (0).gameObject.activeSelf == true) {
			if (Input.GetKey ("j")) {
				JAnimator.SetBool ("LetterPressed", true);
				PlayerAnimator.SetInteger ("PlayerState", 3);
				Chair.GetComponent<BoxCollider2D> ().enabled = false;
				playerRigidbody.velocity = new Vector3 (1.8f, playerRigidbody.velocity.y, 0f);
			} else {
				JAnimator.SetBool ("LetterPressed", false);
				PlayerAnimator.SetInteger ("PlayerState", 2);
				playerRigidbody.velocity = new Vector3 (0f, playerRigidbody.velocity.y, 0f);
			}
				
			
		}
	}
	
	IEnumerator Starting()
	{
		//print(Time.time);
		yield return new WaitForSeconds(10);
		line1.SetActive (true);

		StartCoroutine(LineOne());
		StopCoroutine(Starting());

	}

	IEnumerator LineOne()
	{
		print(Time.time);
		yield return new WaitForSeconds(8);
		line1.SetActive (false);

		line2.SetActive (true);
		StopCoroutine(LineOne());
		StartCoroutine(LineTwo());
	}

	IEnumerator LineTwo()
	{
		print(Time.time);
		yield return new WaitForSeconds(8);
		line2.SetActive (false);
		line3.SetActive (true);
		StopCoroutine(LineTwo());
		StartCoroutine(LineThree());
	}

	IEnumerator LineThree()
	{
		print(Time.time);
		yield return new WaitForSeconds(8);
		line3.SetActive (false);
		line4.SetActive (true);
		OldManAnimator.SetInteger ("OldManState", 1);
		PlayerAnimator.SetInteger ("PlayerState", 1);
		StopCoroutine(LineThree());
		StartCoroutine(LineFour());

	}

	IEnumerator LineFour()
	{
		print(Time.time);
		yield return new WaitForSeconds(5);
		line4.SetActive (false);
		OldManAnimator.SetInteger ("OldManState", 2);

		StopCoroutine(LineFour());
		StartCoroutine (Fight ());
		yield return new WaitForSeconds(0.5f);
		theSoundController.mugSound.Play ();





	}

	IEnumerator Fight()
	{
		print(Time.time);
		yield return new WaitForSeconds(3.2f);
		line4.SetActive (false);
		OldManAnimator.SetInteger ("OldManState", 3);
		WomanAnimator.SetInteger ("WomanState", 1);
		StopCoroutine(Fight());
		StartCoroutine (FightTwo ());
	}

	IEnumerator FightTwo()
	{
		print(Time.time);
		yield return new WaitForSeconds(3.6f);
		WomanAnimator.SetInteger ("WomanState", 2);
		theSoundController.panSound.Play ();
		StopCoroutine(FightTwo());
		drawLetter = true;
	}

	IEnumerator FadeInLetter()
	{
		yield return new WaitForSeconds(3.0f);
		Letters.transform.GetChild (0).GetChild (0).gameObject.SetActive (true);
		StopCoroutine(FadeInLetter());
	}

		
}

