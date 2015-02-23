using UnityEngine;
using System.Collections;

public class platformManagement : MonoBehaviour {

	public bool onMac = false;
	public Transform player1, player2;
	public MouseLook player1Mouse, player2Mouse, camera1Mouse, camera2Mouse;
	public CameraZoom player1Zoom, player2Zoom;
	public newSelector selector1, selector2;
	public FirstPersonDrifter drifter1, drifter2;
	NewPauseMenu pauseMenu;

	// Use this for initialization
	public void SetupPlatform (bool onMacGlobal) {
		onMac = onMacGlobal;
		print ("platformSetup " + onMac);
		player1 = GameObject.FindGameObjectWithTag("player1").transform;
		player2 = GameObject.FindGameObjectWithTag("player2").transform;
		
		pauseMenu = GetComponent<NewPauseMenu>();
		player1Mouse = player1.GetComponent<MouseLook>();
		player2Mouse = player2.GetComponent<MouseLook>();	
		drifter1 = player1.GetComponent<FirstPersonDrifter>();
		drifter2 = player2.GetComponent<FirstPersonDrifter>();
		camera1Mouse = player1.GetChild(0).gameObject.GetComponent<MouseLook>();
		camera2Mouse = player2.GetChild(0).gameObject.GetComponent<MouseLook>();
		player1Zoom = player1.GetChild(0).gameObject.GetComponent<CameraZoom>();
		player2Zoom = player2.GetChild(0).gameObject.GetComponent<CameraZoom>();
		selector1 = player1.GetChild(0).gameObject.GetComponent<newSelector>();
		selector2 = player2.GetChild(0).gameObject.GetComponent<newSelector>();
	
		if (onMac == true){
			player1Mouse.onMac=true;
			player2Mouse.onMac=true;
			camera1Mouse.onMac=true;
			camera2Mouse.onMac=true;
			player1Zoom.onMac=true;
			player2Zoom.onMac=true;
			drifter1.onMac = true;
			drifter2.onMac = true;
			selector1.onMac = true;
			selector2.onMac = true;
			pauseMenu.onMac = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
