using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : MonoBehaviour {

	private GameObject thePlayer;
	private GameObject theBully;

	public GameObject LockerSceneObject;
	private LockerSceneController theLockerSceneController;

	private Rigidbody2D theBookRigidbody;


	private Vector3 handPosition;

	private Vector3 bullyHandPosition;

	private SpriteRenderer bookSprite;

	private Animator thePlayerAnimator;

	private GameObject hand;

	private bool coroutineStarted;

	public bool playerHasBook;
	private bool bookThrown;
	// Use this for initialization
	void Start () {
		theBookRigidbody = GetComponent<Rigidbody2D> ();
		theLockerSceneController = LockerSceneObject.GetComponent<LockerSceneController> ();
		playerHasBook = true;
		thePlayer = GameObject.Find ("Player");
		theBully = GameObject.Find ("Bully");

		thePlayerAnimator = thePlayer.transform.GetChild (0).GetComponent<Animator> ();

		bookSprite = GetComponent<SpriteRenderer> ();
		bullyHandPosition = theBully.transform.GetChild (0).transform.GetChild (3).transform.GetChild (0).position;
		handPosition = thePlayer.transform.GetChild (0).transform.GetChild (0).transform.GetChild (3).transform.GetChild (0).position;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Player Has Book: " + playerHasBook);
		if (this.gameObject.activeSelf == true && playerHasBook == true) {
			Debug.Log (5);
			handPosition = new Vector3 (thePlayer.transform.GetChild (0).transform.GetChild (0).transform.GetChild (2).transform.GetChild (0).position.x+0.2f, thePlayer.transform.GetChild (0).transform.GetChild (0).transform.GetChild (2).transform.GetChild (0).position.y-0.25f, thePlayer.transform.GetChild (0).transform.GetChild (0).transform.GetChild (2).transform.GetChild (0).position.z);
			this.gameObject.transform.position = handPosition;
			//this.gameObject.transform.rotation = Quaternion.Euler (thePlayer.transform.GetChild (0).transform.GetChild (2).transform.GetChild (0).rotation.x, thePlayer.transform.GetChild (0).transform.GetChild (2).transform.GetChild (0).rotation.y, thePlayer.transform.GetChild (0).transform.GetChild (2).transform.GetChild (0).rotation.z);

		}
		else if(this.gameObject.activeSelf == true && bookThrown == true){
			theBookRigidbody.velocity = new Vector3 (-8f, theBookRigidbody.velocity.y, 0f);
			this.gameObject.transform.Rotate (0f, 0f, 2f);
			Debug.Log (6);

		}
		else if(this.gameObject.activeSelf == true && playerHasBook == false && theLockerSceneController.bullyGone == false){
			bookSprite.sortingLayerName = "Bully";
			bookSprite.sortingOrder = 4;
			bullyHandPosition = new Vector3 (theBully.transform.GetChild (0).transform.GetChild (1).transform.GetChild (0).position.x, theBully.transform.GetChild (0).transform.GetChild (1).transform.GetChild (0).position.y, theBully.transform.GetChild (0).transform.GetChild (1).transform.GetChild (0).position.z);
			thePlayer.transform.GetChild (0).GetComponent<Animator> ().SetInteger ("PlayerState", 6);
			this.gameObject.transform.position = bullyHandPosition;
			if (!coroutineStarted) {
				StartCoroutine (WaitForThrow ());
				coroutineStarted = true;
			}
			Debug.Log (6);

		}

		if (theLockerSceneController.bullyGone) {
			theBully.GetComponent<Animator> ().SetInteger ("BullyState", 2);
			Debug.Log (7);

		}

	}

	public IEnumerator WaitForThrow()
	{
		yield return new WaitForSeconds(8.5f);
		bookThrown = true;
		thePlayerAnimator.SetInteger ("PlayerState", 4);
		theBully.GetComponent<Animator> ().SetInteger ("BullyState", 2);
		theLockerSceneController.stopMovement = true;
		GameObject.Find ("GameMusic").GetComponent<MusicController> ().sad2.gameObject.SetActive (true);
		GameObject.Find ("GameMusic").GetComponent<MusicController> ().sad2.volume += 0.05f;
		GameObject.Find ("GameMusic").GetComponent<MusicController> ().sad1.volume -= 0.05f;
		yield return new WaitForSeconds(2.0f);

		GameObject.Find ("GameMusic").GetComponent<MusicController> ().sad2.volume += 0.05f;
		GameObject.Find ("GameMusic").GetComponent<MusicController> ().sad1.volume -= 0.05f;
		yield return new WaitForSeconds(2.0f);
		GameObject.Find ("GameMusic").GetComponent<MusicController> ().sad2.volume += 0.05f;
		GameObject.Find ("GameMusic").GetComponent<MusicController> ().sad1.volume -= 0.05f;
		yield return new WaitForSeconds(2.0f);
		GameObject.Find ("GameMusic").GetComponent<MusicController> ().sad2.volume += 0.05f;
		GameObject.Find ("GameMusic").GetComponent<MusicController> ().sad1.volume -= 0.05f;
		yield return new WaitForSeconds(2.0f);
		StopCoroutine(WaitForThrow());
		StartCoroutine (WaitForSad ());

	}

	public IEnumerator WaitForSad()
	{
		yield return new WaitForSeconds(3.7f);
		thePlayerAnimator.SetInteger ("PlayerState", 2);
		theLockerSceneController.stopMovement = false;
		theLockerSceneController.bullyGone = true;
		GameObject.Find ("GameMusic").GetComponent<MusicController> ().sad1.gameObject.SetActive (false);

		StopCoroutine(WaitForSad());

	}


}
