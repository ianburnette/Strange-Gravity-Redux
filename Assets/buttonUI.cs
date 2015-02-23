using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class buttonUI : MonoBehaviour {

	public int alignment;
	public GameObject text1, text2, gm, playerText;
	public Text countdownText;
	actionStartMultiplayer actionStartScript;
	bool otherPlayerinPlace = false;
	
	// Use this for initialization
	public void SetAlignment(int newAlignment) {
		actionStartScript = gm.GetComponent<actionStartMultiplayer>();
		alignment = newAlignment;
	}
	
	public void ShowCountdown(){
		countdownText.enabled = true;
		playerText.SetActive(false);
		otherPlayerinPlace = true;
		text1.SetActive(false);
	}
	
	public void HideCountdown(){
		countdownText.enabled = false;
		playerText.SetActive(true);
		otherPlayerinPlace = false;
		text1.SetActive(false);
		countdownText.text = ""+3;
	}
	
	public void HideAll(){
		countdownText.enabled = false;
	}
	
	public void NewCountdown(int countdownNum){
		countdownText.text = "" + countdownNum;
	}
	
	void OnTriggerStay (Collider coll) {
		if (alignment == 0){
			if (coll.transform.tag == "player1"){
				if (!otherPlayerinPlace)
					text1.SetActive(true);
				actionStartScript.button1Pressed = true;
			} else if (coll.transform.tag == "player2"){
				text2.SetActive(true);
			}
		}else if (alignment == 1){
			if (coll.transform.tag == "player2"){
				if (!otherPlayerinPlace)
					text1.SetActive(true);
				actionStartScript.button2Pressed = true;
			} else if (coll.transform.tag == "player1"){
				text2.SetActive(true);
			}
		}
	}
	
	void OnTriggerExit (Collider coll){
		text1.SetActive(false);
		text2.SetActive(false);
		if (alignment == 0){
			actionStartScript.button1Pressed = false;
		}else if (alignment == 1){
			actionStartScript.button2Pressed = false;
		}
	}
}
