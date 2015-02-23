using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class scoreManagement : MonoBehaviour {

	public int nextLevel;
	public bool tutorial = false;
	public float timeBeforeEnd = 2f;
	public int goodCount, badACount, neutralCount;
	public GameObject[] goodPlanets;
	public GameObject[] badAPlanets;
	public GameObject[] neutralPlanets;
	endGameContinue endScript;
	public GameObject endGameUI;
	public Text endGameText;
	public string[] winTexts;
	bool listen = false;
	public GameObject[] signs;
	public tutorialLogic[] logicScripts;

	// Use this for initialization
	void Start () {
		endScript = GetComponent<endGameContinue>();
		Invoke("SetupLists", 0.1f);
		if (tutorial){
			//signs = GameObject.FindGameObjectsWithTag("sign");
			/* for (int i = 0; i<signs.Length; i++){
				logicScripts[i] = signs[i].GetComponent<tutorialLogic>();
			} */
		}
	}
	
	void SetupLists(){
		goodPlanets = new GameObject[50];
		badAPlanets = new GameObject[50];
		neutralPlanets = new GameObject[50];
		goodPlanets = GameObject.FindGameObjectsWithTag("goodPlanet");
		badAPlanets = GameObject.FindGameObjectsWithTag("badAPlanet");
		neutralPlanets = GameObject.FindGameObjectsWithTag("planet");
		
		goodCount = goodPlanets.Length;
		badACount = badAPlanets.Length;
		neutralCount = neutralPlanets.Length;
	}
	
	public void Change(int changedFrom, int changedTo) {
		if (changedFrom == 0) goodCount--;
		if (changedFrom == 1) badACount--;
		//add here for more players
		if (changedFrom == 4) neutralCount--;
		
		if (changedTo == 0) goodCount++;
		if (changedTo == 1) badACount++;
		if (changedTo == 4) neutralCount++;
		CheckScore();
	}
	
	void CheckScore(){
		if (goodCount <= 0 && badACount > 0)	StartCoroutine("EndGame", 1);
		if (badACount <= 0 && goodCount > 0)	StartCoroutine("EndGame", 0);
		else if (badACount <= 0 && goodCount <= 0)	StartCoroutine("EndGame", 4);
	}
	
	IEnumerator EndGame(int whoWon){
		if (!tutorial){
			GetComponent<playerSounds>().StopMusic();
			yield return new WaitForSeconds (timeBeforeEnd);
			endGameUI.SetActive(true);
			endGameText.text = winTexts[whoWon];
			listen = true;
			GetComponent<playerSounds>().WinLevel();
		}
		else{
//print ("in end loop");
			GetComponent<playerSounds>().StopMusic();
			yield return new WaitForSeconds (timeBeforeEnd);
			endGameUI.SetActive(true);
			endGameText.text = "Tutorial Complete";
			listen = true;
			GetComponent<playerSounds>().WinLevel();
		}	
	}
	
	void Update(){
		if (listen){
			ListenForReturn();
		}
		if (tutorial){
			if (logicScripts[0].ready && logicScripts[1].ready){
				//print ("both ready");
				StartCoroutine("EndGame", 0);
			}
		}
	}
	
	void ListenForReturn(){
		
		if (Input.GetButton("Fire1P1") && Input.GetButton("Fire2P1") && Input.GetButton("Fire1P2") && Input.GetButton("Fire2P2")){
			if (!tutorial){
				Application.LoadLevel(0);
			}else{
				Application.LoadLevel(Application.loadedLevel+1);
			}
		}
	}
}
