using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 using XInputDotNetPure; 

public class multiplayerManagement : MonoBehaviour {

	public float fieldOfView2P = 50f;
	public int numberOfPlayers;
	public GameObject[] players = new GameObject[0];
	public Transform crosshair;
	//public Transform[] twoPlayerCrosshairs = new Transform[2];

	// Use this for initialization
	void Start () {
		if (numberOfPlayers == 1){
			SetupPlayers(1);
		}
		else if (numberOfPlayers == 2){
			SetupPlayers(2);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)){
			//  GamePad.SetVibration(0,1,1);
		}
		if (Input.GetKeyDown(KeyCode.Return)){
		//	  GamePad.SetVibration(0,0,0);
		}
	//	Setup2PlayerCrosshairs();
	}
	
	void SetupPlayers(int num){
		//players = new GameObject[num];
		if (num == 1){
		//	players = GameObject.FindGameObjectsWithTag("Player");
			players[0].transform.GetChild(0).GetComponent<Camera>().rect = new Rect(0,0,1,1);
			/* foreach (Transform crosshair in twoPlayerCrosshairs){
				crosshair.gameObject.SetActive (false);
			} */
		}
		else if (num == 2){
		//	players = GameObject.FindGameObjectsWithTag("Player");
			players[0].transform.GetChild(0).GetComponent<Camera>().rect = new Rect(0,0.5f,1,1);
			players[1].transform.GetChild(0).GetComponent<Camera>().rect = new Rect(0,0,1,0.5f);
			players[0].transform.GetChild(0).GetComponent<Camera>().fieldOfView = fieldOfView2P;
			players[1].transform.GetChild(0).GetComponent<Camera>().fieldOfView = fieldOfView2P;
		/* 	crosshair.gameObject.SetActive(false);
			Setup2PlayerCrosshairs(); */
		}
	}
}
