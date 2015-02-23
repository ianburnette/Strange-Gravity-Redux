using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems; 

public class NewPauseMenu : MonoBehaviour {

	public bool onMac;
	public bool paused = false;
	public GameObject pauseCamera;
	
	public Slider volSlider;
	public Slider sens1, sens2;
	public Toggle inv1, inv2;
	
	public GameObject pauseCanvas;
	public GameObject winCanvas;
	public GameObject[] menuObjects;
	public GameObject[] firstButtons;
	public EventSystem eventSystem;
	public GameObject player1Cam, player2Cam;
	public MouseLook player1MouseLook, player2MouseLook;
	public MouseLook player1PlayerLook, player2PlayerLook;
	public float sensitivityModifier = 16f;

	void SetupReferences(){
		pauseCamera = GameObject.Find("EndCamera");	
		pauseCanvas = pauseCamera.transform.GetChild(1).gameObject;
		winCanvas = pauseCamera.transform.GetChild(0).gameObject;
		eventSystem = pauseCamera.transform.GetChild(2).GetComponent<EventSystem>();
		for (int i = 0; i<4; i++){
			menuObjects[i] = pauseCanvas.transform.GetChild(i+1).gameObject;
			firstButtons[i] = menuObjects[i].transform.GetChild(0).gameObject;
		}
		player1Cam = GameObject.FindGameObjectWithTag("player1").transform.GetChild(0).gameObject;
		player2Cam = GameObject.FindGameObjectWithTag("player2").transform.GetChild(0).gameObject;
		volSlider = menuObjects[1].transform.GetChild(1).GetComponent<Slider>();
		sens1 = menuObjects[1].transform.GetChild(2).GetComponent<Slider>();
		sens2 = menuObjects[1].transform.GetChild(3).GetComponent<Slider>();
		pauseCamera.SetActive(false);
	}
	
	// Use this for initialization
	void Start () {
		SetupReferences();
		volSlider.value = 0.5f;
		volSlider.onValueChanged.AddListener(VolumeChange);
		sens1.onValueChanged.AddListener(SetSensitivity1);
		sens2.onValueChanged.AddListener(SetSensitivity2);
		//inv1.onValueChanged.AddListener(
		player1MouseLook = player1Cam.GetComponent<MouseLook>();
		player2MouseLook = player2Cam.GetComponent<MouseLook>();
		player1PlayerLook = player1Cam.transform.parent.GetComponent<MouseLook>();
		player2PlayerLook = player2Cam.transform.parent.GetComponent<MouseLook>();
	}
	
	void Update(){
		if (onMac){
			if (Input.GetButtonUp("PauseMAC")){
				print ("paused");
				TogglePause();
			}
		}else{
			if (Input.GetButtonUp("Pause")){
				print ("paused");
				TogglePause();
			}

		}
	}
	
	// Update is called once per frame
	public void ChangeScreen (int newScreen) {
		for (int i = 0; i<menuObjects.Length; i++){
			if (i == newScreen){
				menuObjects[i].SetActive(true);
				eventSystem.SetSelectedGameObject(firstButtons[i]);
			}
			else{
				menuObjects[i].SetActive(false);
			}
		}
	}
	
	public void TogglePause(){
		if (paused){
			paused = false;
			pauseCamera.SetActive(false);
			pauseCanvas.SetActive(false);
			winCanvas.SetActive(true);
			Time.timeScale = 1;
		}else{
			paused = true;
			pauseCamera.SetActive(true);
			pauseCanvas.SetActive(true);
			winCanvas.SetActive(false);
			ChangeScreen(0);
			eventSystem.SetSelectedGameObject(firstButtons[0]);
			Time.timeScale = 0;
		}
	}
	
	public void SetSensitivity1(float newSensitivity){
			player1PlayerLook.sensitivityX = newSensitivity * sensitivityModifier;
			player1PlayerLook.sensitivityY = newSensitivity * sensitivityModifier;
			player1MouseLook.sensitivityX = newSensitivity * sensitivityModifier;
			player1MouseLook.sensitivityY = newSensitivity * sensitivityModifier;
	}
	
		
	public void SetSensitivity2(float newSensitivity){
			player2PlayerLook.sensitivityX = newSensitivity * sensitivityModifier;
			player2PlayerLook.sensitivityY = newSensitivity * sensitivityModifier;
			player2MouseLook.sensitivityX = newSensitivity * sensitivityModifier;
			player2MouseLook.sensitivityY = newSensitivity * sensitivityModifier;
	}
	
	public void ResetLevel(){
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void ResetGame(){
		Application.LoadLevel(0);
	}
	
	public void ToggleInvert(int playerNum){
		if (playerNum == 0)
			player1MouseLook.invertY = !player1MouseLook.invertY;
		else if (playerNum == 1){
			player2MouseLook.invertY = !player2MouseLook.invertY;
		}
	}
	
	public void VolumeChange(float volume){
		AudioListener.volume = volSlider.value;
	}
}
