using UnityEngine;
using System.Collections;

public class multiplayerProperties : MonoBehaviour {

	public int playerNum;
	newHighlightScript highlightScript;
	newSelector selectorScript;
	newSelectedPlanets selectedPlanetsScript;
	FirstPersonDrifter drifterControls;
	buttonDetector buttonScript;
	MouseLook playerLook;
	MouseLook cameraLook;
	CameraZoom zoomScript;
	
	// Use this for initialization
	void Start () {
	
		selectorScript = transform.GetChild(0).GetComponent<newSelector>();
		buttonScript = transform.GetChild(0).GetComponent<buttonDetector>();
		highlightScript = transform.GetChild(0).GetComponent<newHighlightScript>();
		selectedPlanetsScript = transform.GetChild(0).GetComponent<newSelectedPlanets>();
		zoomScript = transform.GetChild(0).GetComponent<CameraZoom>();
		drifterControls = GetComponent<FirstPersonDrifter>();
		playerLook = GetComponent<MouseLook>();
		cameraLook = transform.GetChild(0).GetComponent<MouseLook>();
		selectorScript.alignment = playerNum;
		buttonScript.SetAlignment(playerNum);
		selectedPlanetsScript.myPlayerAlignment = playerNum;
		drifterControls.playerNumber = playerNum;
		playerLook.playerNumber = playerNum;
		cameraLook.playerNumber = playerNum;
		zoomScript.playerNum = playerNum;
		//playerNumber = playerNum;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
