using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GymSceneController : MonoBehaviour {

	private bool playingVolleyball;
	public GameObject Player;
	public GameObject Bully;
	public GameObject Volleyball;

	public bool playerHittingBall;
	public bool bullyHittingBall;

	private Animator thePlayerAnimator;
	private Animator theBullyAnimator;
	private Animator theVolleyballAnimator;

	public GameObject Letters;
	private Animator JAnimator;
	private Animator UAnimator;
	private Animator SAnimator;
	private Animator TAnimator;
	private Animator LAnimator;

	public GameObject SwearBubble;

	private Rigidbody2D thePlayerRigidbody;

	public GameObject PenInHand;
	private bool drawLetter;



	// Use this for initialization
	void Start () {

		thePlayerRigidbody = Player.GetComponent<Rigidbody2D> ();
		thePlayerAnimator = Player.transform.GetChild (0).GetComponent<Animator> ();
		theBullyAnimator = Bully.transform.GetChild (0).GetComponent<Animator> ();
		theVolleyballAnimator = Volleyball.GetComponent<Animator> ();
		thePlayerAnimator.SetInteger ("PlayerState", 2);
		StartCoroutine (WaitForHitAnim ());

		JAnimator = Letters.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		UAnimator = Letters.transform.GetChild(0).GetChild(1).GetComponent<Animator>();
		SAnimator = Letters.transform.GetChild(0).GetChild(2).GetComponent<Animator>();
		TAnimator = Letters.transform.GetChild(0).GetChild(3).GetComponent<Animator>();
		LAnimator = Letters.transform.GetChild(0).GetChild(4).GetComponent<Animator>();
		JAnimator.SetBool ("LetterPressed", true);
		UAnimator.SetBool ("LetterPressed", true);
		SAnimator.SetBool ("LetterPressed", true);
		TAnimator.SetBool ("LetterPressed", true);
	}
	
	// Update is called once per frame
	void Update () {

		FadeDownVolume ();

		if (drawLetter) {
			PenInHand.gameObject.SetActive (true);
			PenInHand.GetComponent<Animator> ().SetInteger ("HandAndPenState", 5);
			StartCoroutine (FadeInLetter ());
			drawLetter = false;

		}

		if (playingVolleyball) {

			if (Player.gameObject.GetComponent<BoxCollider2D> ().bounds.Intersects (Volleyball.gameObject.GetComponent<CircleCollider2D> ().bounds)) {
				playerHittingBall = true;
				thePlayerAnimator.SetInteger ("PlayerState", 10);
			} else if (Bully.gameObject.GetComponent<BoxCollider2D> ().bounds.Intersects (Volleyball.gameObject.GetComponent<CircleCollider2D> ().bounds)) {
				bullyHittingBall = true;
				theBullyAnimator.SetInteger ("BullyState", 7);
			} else {
				thePlayerAnimator.SetInteger ("PlayerState", 2);
				theBullyAnimator.SetInteger ("BullyState", 0);
			}


			if (Volleyball.transform.position.x < -2) {
				
				Player.transform.position = new Vector3 (Volleyball.transform.position.x, Player.transform.position.y, Player.transform.position.z);
			}
			else if(Volleyball.transform.position.x > 2.7){
				Bully.transform.position = new Vector3(Volleyball.transform.position.x, Bully.transform.position.y, Bully.transform.position.z);
			}
		}

		else if(Letters.transform.GetChild (0).GetChild (4).gameObject.activeSelf == true){
			if (Input.GetKey ("l")) {
				LAnimator.SetBool ("LetterPressed", true);
				thePlayerAnimator.SetInteger ("PlayerState", 30);
				thePlayerRigidbody.velocity = new Vector3 (1f, thePlayerRigidbody.velocity.y, 0f);

			}
			else 
			{
				LAnimator.SetBool ("LetterPressed", false);
				thePlayerAnimator.SetInteger ("PlayerState", 2);
				thePlayerRigidbody.velocity = new Vector3 (0f, thePlayerRigidbody.velocity.y, 0f);
			}
		}
	}


	public IEnumerator WaitForHitAnim()
	{
		theBullyAnimator.SetInteger ("BullyState", 6);
		yield return new WaitForSeconds (2f);
	
		theVolleyballAnimator.SetBool ("hasBeenServed", true);
		playingVolleyball = true;
		StartCoroutine (WaitForGameToFinish ());
		StopCoroutine (WaitForHitAnim ());
	}

	public IEnumerator WaitForGameToFinish()
	{
		yield return new WaitForSeconds (55.3f);

		playingVolleyball = false;
		SwearBubble.gameObject.SetActive (true);
		theBullyAnimator.SetInteger ("BullyState", 8);
		thePlayerAnimator.SetInteger ("PlayerState", 11);
		StartCoroutine (PlayerCelebration());
		StopCoroutine (WaitForGameToFinish ());
	}

	public IEnumerator PlayerCelebration()
	{
		yield return new WaitForSeconds (13.0f);

		thePlayerAnimator.SetInteger ("PlayerState", 2);
		//Letters.transform.GetChild (0).GetChild (4).gameObject.SetActive (true);
		drawLetter = true;

		StopCoroutine (PlayerCelebration());
	}

	IEnumerator FadeInLetter()
	{
		yield return new WaitForSeconds(3.0f);
		Letters.transform.GetChild (0).GetChild (4).gameObject.SetActive (true);
		StopCoroutine(FadeInLetter());
	}

	void FadeDownVolume(){
		AudioSource sadMusic = GameObject.Find ("GameMusic").GetComponent<MusicController> ().sad2;

		if (sadMusic.volume > 0) {
			sadMusic.volume -= 0.05f * Time.deltaTime;

		}

		if (sadMusic.volume <= 0) {
			GameObject.Find ("GameMusic").GetComponent<MusicController> ().desired.gameObject.SetActive (true);
		}
	}
}
