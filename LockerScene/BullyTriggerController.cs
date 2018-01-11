using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyTriggerController : MonoBehaviour {

	public GameObject Bully;
	public GameObject Book;
	private BookController theBookController;
	private bool throwingBook,idle,walkingAway;


	void Start(){
		theBookController = Book.GetComponent<BookController> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
			throwingBook = true;
			theBookController.playerHasBook = false;
		}
	}
	void Update(){
		if (throwingBook) {
			Bully.GetComponent<Animator> ().SetInteger ("BullyState", 1);

		} else if (walkingAway) {
			Bully.GetComponent<Animator> ().SetInteger ("BullyState", 2);

		}



	}

		public IEnumerator WaitForThrow()
		{
			yield return new WaitForSeconds(9.0f);
			throwingBook = false;
			walkingAway = true;

			StopCoroutine(WaitForThrow());
		}
		
}
