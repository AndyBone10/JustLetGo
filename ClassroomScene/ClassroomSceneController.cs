using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomSceneController : MonoBehaviour {

	public GameObject Player;
	public GameObject Bully;
	public GameObject Teacher;
	private Animator theBullyAnimator;
	private Animator thePlayerAnimator;
	private Animator theTeacherAnimator;

	public GameObject Letters;
	private Animator JAnimator;
	private Animator UAnimator;
	private Animator SAnimator;
	private Animator TAnimator;

	public GameObject teacherQuestion;
	public GameObject playerAnswer;

	public GameObject chair;

	private Rigidbody2D playerRigidbody;

	public GameObject PenInHand;
	private bool drawLetter;

	private ClassroomSoundController theSoundController;
	private bool startFadingLaugh;

	// Use this for initialization
	void Start () {

		theSoundController = GameObject.Find ("SoundController").GetComponent<ClassroomSoundController> ();

		playerRigidbody = Player.GetComponent<Rigidbody2D> ();
		thePlayerAnimator = Player.transform.GetChild(0).GetComponent<Animator> ();
		thePlayerAnimator.SetInteger ("PlayerState", 7);

		theBullyAnimator = Bully.GetComponent<Animator> ();
		theBullyAnimator.SetInteger ("BullyState", 3);

		theTeacherAnimator = Teacher.GetComponent<Animator> ();


		JAnimator = Letters.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		UAnimator = Letters.transform.GetChild(0).GetChild(1).GetComponent<Animator>();
		SAnimator = Letters.transform.GetChild(0).GetChild(2).GetComponent<Animator>();
		TAnimator = Letters.transform.GetChild(0).GetChild(3).GetComponent<Animator>();

		StartCoroutine (WaitForQuestion ());
	}
	
	// Update is called once per frame
	void Update () {

		if (startFadingLaugh) {
			FadeDownVolume ();
		}

		if (drawLetter) {
			PenInHand.gameObject.SetActive (true);
			PenInHand.GetComponent<Animator> ().SetInteger ("HandAndPenState", 4);
			StartCoroutine (FadeInLetter ());
			drawLetter = false;

		}

		if (Letters.transform.GetChild (0).GetChild (0).gameObject.activeSelf == true 
			&& Letters.transform.GetChild (0).GetChild (1).gameObject.activeSelf == true
			&& Letters.transform.GetChild (0).GetChild (2).gameObject.activeSelf == true
			&& Letters.transform.GetChild (0).GetChild (3).gameObject.activeSelf == false) {

			JAnimator.SetBool ("LetterPressed", true);
			UAnimator.SetBool ("LetterPressed", true);
			SAnimator.SetBool ("LetterPressed", true);

		}

		else if (Letters.transform.GetChild (0).GetChild (0).gameObject.activeSelf == true 
			&& Letters.transform.GetChild (0).GetChild (1).gameObject.activeSelf == true
			&& Letters.transform.GetChild (0).GetChild (2).gameObject.activeSelf == true
			&& Letters.transform.GetChild (0).GetChild (3).gameObject.activeSelf == true) {

			JAnimator.SetBool ("LetterPressed", true);
			UAnimator.SetBool ("LetterPressed", true);
			SAnimator.SetBool ("LetterPressed", true);



			if (Input.GetKey ("t")) {
				TAnimator.SetBool ("LetterPressed", true);
				chair.GetComponent<BoxCollider2D> ().enabled = false;
				thePlayerAnimator.SetInteger ("PlayerState", 3);
				playerRigidbody.velocity = new Vector3 (1f, playerRigidbody.velocity.y, 0f);

			} else if(chair.GetComponent<BoxCollider2D> ().enabled == false) {
				TAnimator.SetBool ("LetterPressed", false);
				thePlayerAnimator.SetInteger ("PlayerState", 2);
				playerRigidbody.velocity = new Vector3 (0f, playerRigidbody.velocity.y, 0f);
			}

		}
		
	}

	public IEnumerator WaitForQuestion()
	{
		yield return new WaitForSeconds(6.0f);
		teacherQuestion.SetActive (true);
		yield return new WaitForSeconds(6.0f);
		StartCoroutine (PlayerPutsHandUp());
		StopCoroutine(WaitForQuestion());
	}

	public IEnumerator PlayerPutsHandUp()
	{
		thePlayerAnimator.SetInteger ("PlayerState", 8);
		teacherQuestion.SetActive (false);
		yield return new WaitForSeconds(7.0f);
		thePlayerAnimator.SetInteger ("PlayerState", 7);
		StopCoroutine (PlayerPutsHandUp());
		StartCoroutine (TeacherPointsAtPlayer ());
	}

	public IEnumerator TeacherPointsAtPlayer()
	{
		theTeacherAnimator.SetInteger ("TeacherState", 1);

		yield return new WaitForSeconds(3.0f);

		theTeacherAnimator.SetInteger ("TeacherState", 2);
		StopCoroutine (TeacherPointsAtPlayer());
		StartCoroutine (PlayerAnswersQuestion ());
	}

	public IEnumerator PlayerAnswersQuestion()
	{
		playerAnswer.SetActive (true);
		yield return new WaitForSeconds(3.0f);	
		playerAnswer.SetActive (false);
		theTeacherAnimator.SetInteger ("TeacherState", 3);
		theBullyAnimator.SetInteger ("BullyState", 4);
		thePlayerAnimator.SetInteger ("PlayerState", 9);
		StartCoroutine (EverybodyLaughs ());
		StopCoroutine (PlayerAnswersQuestion());

	}
	public IEnumerator EverybodyLaughs()
	{
		theSoundController.classLaugh.Play ();

		yield return new WaitForSeconds(9.0f);	
		theTeacherAnimator.SetInteger ("TeacherState", 2);
		theBullyAnimator.SetInteger ("BullyState", 3);
		StartCoroutine (BellRings ());
		StopCoroutine (EverybodyLaughs ());

	}
		

	public IEnumerator BellRings()
	{
		//theSoundController.classLaugh.volume = 0;
		startFadingLaugh = true;
		yield return new WaitForSeconds(8.0f);
		startFadingLaugh = false;
		theSoundController.schoolBell.Play ();
		yield return new WaitForSeconds(1.0f);
		theTeacherAnimator.SetInteger ("TeacherState", 4);
		theBullyAnimator.SetInteger ("BullyState", 5);
		StartCoroutine (WaitForPlayerToRise ());
		theSoundController.classLaugh.volume = .32f;
		StopCoroutine (BellRings ());
	}

	public IEnumerator WaitForPlayerToRise()
	{
		theSoundController.classLaugh.volume = .32f;

		yield return new WaitForSeconds(5.0f);	

		theSoundController.classLaugh.pitch = 0.8f;

		yield return new WaitForSeconds(5.0f);	

		theSoundController.classLaugh.pitch = 0.6f;

		yield return new WaitForSeconds(5.0f);	

		theSoundController.classLaugh.pitch = 0.4f;

		yield return new WaitForSeconds(5.0f);	

		theSoundController.classLaugh.pitch = 0.2f;

		yield return new WaitForSeconds(5.0f);	

		theSoundController.classLaugh.pitch = -0.04f;

		yield return new WaitForSeconds(12.0f);	

		//Letters.transform.GetChild (0).GetChild (3).gameObject.SetActive (true);
		drawLetter = true;
		StopCoroutine (WaitForPlayerToRise ());
	}

	IEnumerator FadeInLetter()
	{
		yield return new WaitForSeconds(3.0f);
		Letters.transform.GetChild (0).GetChild (3).gameObject.SetActive (true);
		StopCoroutine(FadeInLetter());
	}

	void FadeDownVolume(){
		

		if (theSoundController.classLaugh.volume > 0) {
			theSoundController.classLaugh.volume -= 0.05f * Time.deltaTime;

		}
			
	}

}
