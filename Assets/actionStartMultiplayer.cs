using UnityEngine;
using System.Collections;

public class actionStartMultiplayer : MonoBehaviour {

	//public Transform button1, button2;
	public bool startImmediately = false;
	public GameObject[] players;
	newSelector[] selectorScripts;
	public GameObject[] planets;
	public GameObject button1, button2;
	public bool button1Pressed, button2Pressed;
	bool started, starting = false;
	int countdown;
	public int baseCountdown = 3;

	// Use this for initialization
	void Start () {
		for (int i = 0; i<players.Length; i++){
			players[i].GetComponent<newSelector>().enabled = false;
		}
		planets = GameObject.FindGameObjectsWithTag("planetParent");
		countdown = baseCountdown;
		if (startImmediately){
			StartAction();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (button1Pressed && button2Pressed && !started && !starting){
			ShowCountdown();
			starting = true;			
			InvokeRepeating("DecrementCount", 1, 1);
	
		}
		if ((!button1Pressed || !button2Pressed) && starting){
			CancelInvoke("DecrementCount");
			HideCountdown();
			countdown = baseCountdown;
			starting = false;
		}
	}
	
	void StartAction(){
		foreach (GameObject planet in planets){
			planet.GetComponent<newSpawnSpores>().BeginSpawning();
		}
		RemoveButtons();
		ActivateControlScripts();
		GetComponent<playerSounds>().StartMusic();
	}
	
	void ActivateControlScripts(){
		for (int i = 0; i<players.Length; i++){
			players[i].GetComponent<newSelector>().enabled = true;
		}
	}
	
	void DecrementCount(){
		countdown--;
		if (countdown < 1){
			StartAction();
			CancelInvoke("DecrementCount");
		}else{
			button1.GetComponent<buttonUI>().NewCountdown(countdown);
			button2.GetComponent<buttonUI>().NewCountdown(countdown);
		}
	}	
	
	void RemoveButtons(){
		button1.GetComponent<newButtonPress>().Sink();
		button2.GetComponent<newButtonPress>().Sink();
		button1.GetComponent<buttonUI>().HideAll();
			button2.GetComponent<buttonUI>().HideAll();
		Invoke("DestroyButtons", 3f);
	}
	
	void DestroyButtons(){
		Destroy(button1);
		Destroy(button2);
	}
	
	void HideCountdown(){
		button1.GetComponent<buttonUI>().HideCountdown();
		button2.GetComponent<buttonUI>().HideCountdown();
	}
	
	void ShowCountdown(){
		button1.GetComponent<buttonUI>().ShowCountdown();
		button2.GetComponent<buttonUI>().ShowCountdown();
	}
}
