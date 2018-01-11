using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnimateText : MonoBehaviour {

	private string str;
	public Text theTextToAnimate;
	public GameObject twitterButton;
	
	// Update is called once per frame
	void Update () {
		theTextToAnimate.text = str;
	}


	void Start(){
		str = "A Game By Andy Bone";
		StartCoroutine( Animate(str) );
		StartCoroutine( DelayButtonAppearence() );
	}


	IEnumerator Animate(string strComplete){
		
		int i = 0;

		str = "";
		while( i < strComplete.Length ){
			str += strComplete[i++];
			yield return new WaitForSeconds(0.3f);
		}
	}

	IEnumerator DelayButtonAppearence(){

	
			yield return new WaitForSeconds(6.3f);
		twitterButton.gameObject.SetActive (true);
	}
}
