using UnityEngine;
using System.Collections;

public class levelProgress : MonoBehaviour {

	public bool begin = false;
	public GameObject ai, player, canvas, finishLight, exitDoor, camera, finishSign;
	public GameObject[] goodPlanets, badPlanets, neutralPlanets;
	public AudioClip win;
	bool everPlayed = false;
	

	// Use this for initialization
	void Start () {
		StartCoroutine(SetUpAssociations());
	//	Toggle(false);
		win = Resources.Load("win") as AudioClip;
	//	ai = GameObject.Find("AI");
		finishLight = GameObject.Find("bulb");
		finishLight.active = false;
		exitDoor = GameObject.Find("exitDoor");
	}
	
	IEnumerator SetUpAssociations(){
		yield return new WaitForSeconds(.2f);
		//ai = GameObject.Find("AI");
		player = GameObject.Find("Player");
		camera = player.transform.GetChild(0).gameObject;
		goodPlanets = GameObject.FindGameObjectsWithTag("goodPlanet");
		badPlanets = GameObject.FindGameObjectsWithTag("badPlanet");
		neutralPlanets = GameObject.FindGameObjectsWithTag("planet");
		Toggle(false);
		if (Application.loadedLevel==12){
			exitDoor.active = false;
		}
	}
	
	public void Toggle(bool toggleStatus){
		ai.active = toggleStatus;	
		if (toggleStatus==true){
			GameObject.Find("Player").transform.GetChild(0).GetComponent<multiSelect>().enabled = toggleStatus;
		}
		//canvas.SetActive(toggleStatus);// = toggleStatus;
		
		foreach (GameObject planet in goodPlanets){
			//planet.GetComponent<SporeSpawner>().enabled = toggleStatus;
			if (toggleStatus == true){ planet.GetComponent<InventoryScript>().BeginCounting();}	
		}
		foreach (GameObject planet in badPlanets){
			//planet.GetComponent<SporeSpawner>().enabled = toggleStatus;
			if (toggleStatus == true){ planet.GetComponent<InventoryScript>().BeginCounting();}	
		}
		foreach (GameObject planet in neutralPlanets){
			//planet.GetComponent<SporeSpawner>().enabled = toggleStatus;
			if (toggleStatus == true){ planet.GetComponent<InventoryScript>().BeginCounting();}	
		}
		
	}
	
	public  void FinishLevel(){
			finishLight.active = true;
		if (Application.loadedLevel==12){
			finishSign.active = true;
			exitDoor.active=true;
		}
		else{
		exitDoor.GetComponent<exitDoorScript>().OpenDoor();
		GameObject music = GameObject.FindGameObjectWithTag("music");
		music.GetComponent<MusicScript>().SetExcitingVolume(0);
		}
		if (!everPlayed){
			audio.PlayOneShot(win, 0.8f);
			everPlayed= true;
		}
	}
	
	public void NextLevel(){
		camera.GetComponent<CameraFadeOnStart>().FadeOut();
		Invoke ("LoadNext", 4.5f);
	}
	
	public void RestartLevel(){
		camera.GetComponent<CameraFadeOnStart>().FadeOut();
		Invoke ("ReloadLevel", 2.5f);
	}
	
	void ReloadLevel(){
		Application.LoadLevel(Application.loadedLevel);
	}
	
	void LoadNext(){
		Application.LoadLevel(Application.loadedLevel+1);
	}
}
