using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PhoneSceneController : MonoBehaviour {

	public GameObject insult1,insult2,insult3;
	public GameObject playerHand;

	// Use this for initialization
	void Start () {
		GameObject.Find ("SoundController").GetComponent<PhoneSceneSoundController> ().NotificationPop.Play ();
		insult1.gameObject.SetActive (true);
		StartCoroutine (InsultingOne ());
	}

	public IEnumerator InsultingOne()
	{
		yield return new WaitForSeconds (4.0f);
		StartCoroutine (InsultingTwo ());
		StopCoroutine (InsultingOne ());
	}

	public IEnumerator InsultingTwo()
	{
		GameObject.Find ("SoundController").GetComponent<PhoneSceneSoundController> ().NotificationPop.Play ();

		insult2.gameObject.SetActive (true);
		yield return new WaitForSeconds (4.0f);
		StartCoroutine (InsultingThree ());
		StopCoroutine (InsultingTwo ());
	}

	public IEnumerator InsultingThree()
	{
		GameObject.Find ("SoundController").GetComponent<PhoneSceneSoundController> ().NotificationPop.Play ();

		insult3.gameObject.SetActive (true);
		yield return new WaitForSeconds (4.0f);
		StartCoroutine (PlayerHandShake ());
		StopCoroutine (InsultingThree ());

	}

	public IEnumerator PlayerHandShake()
	{
		playerHand.GetComponent<Animator> ().Play ("PlayerHandPhoneShake");
		yield return new WaitForSeconds (3.5f);
		StopCoroutine (PlayerHandShake ());
		SceneManager.LoadScene ("ThirdBedroomScene");

	}

}
