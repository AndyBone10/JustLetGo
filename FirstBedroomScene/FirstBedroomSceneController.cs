using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FirstBedroomSceneController : MonoBehaviour {

	public GameObject Letters;
	private Animator JAnimator;
	private Animator UAnimator;
	private Animator SAnimator;
	private Animator TAnimator;
	private Animator LAnimator;
	private Animator EAnimator;

	public GameObject Player;
	private Animator thePlayerAnimator;
	private Rigidbody2D thePlayerRigidbody;

	public bool playerAtController;


	// Use this for initialization
	void Start () {
		thePlayerRigidbody = Player.GetComponent<Rigidbody2D> ();
		thePlayerAnimator = Player.transform.GetChild (0).GetComponent<Animator> ();
		thePlayerAnimator.SetInteger ("PlayerState", 2);

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
		LAnimator.SetBool ("LetterPressed", true);
	}
	
	// Update is called once per frame
	void Update () {
		if (playerAtController == false) {
			
			if (Input.GetKey ("e")) {
				EAnimator.SetBool ("LetterPressed", true);
				thePlayerAnimator.SetInteger ("PlayerState", 3);
				thePlayerRigidbody.velocity = new Vector3 (-1f, thePlayerRigidbody.velocity.y, 0f);

			} else {
				EAnimator.SetBool ("LetterPressed", false);
				thePlayerAnimator.SetInteger ("PlayerState", 2);
				thePlayerRigidbody.velocity = new Vector3 (0f, thePlayerRigidbody.velocity.y, 0f);
			}
		} else {
			thePlayerAnimator.SetInteger ("PlayerState", 14);
			GameObject.Find ("Controller").GetComponent<Animator> ().SetBool ("controllerInHands", true);
			StartCoroutine (WaitForControllerAnim ());
		}
	}

	public IEnumerator WaitForControllerAnim()
	{

		yield return new WaitForSeconds (1.0f);
		SceneManager.LoadScene ("GameWithinGame");

	}

}


