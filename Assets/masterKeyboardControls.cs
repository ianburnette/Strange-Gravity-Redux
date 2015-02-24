using UnityEngine;
using System.Collections;

public class masterKeyboardControls : MonoBehaviour {

	NewPauseMenu pauseMenu;
	
	void OnLevelWasLoaded(){
		if (Application.loadedLevel!=0){
			pauseMenu = GameObject.Find("_gm").GetComponent<NewPauseMenu>();
		}
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)){ 
			pauseMenu.TogglePause();
		}
		if (Input.GetKeyDown(KeyCode.F1)){ Application.LoadLevel(0);}
		if (Input.GetKeyDown(KeyCode.F2)){ Application.LoadLevel(Application.loadedLevel);}
		if (Input.GetKeyDown(KeyCode.F3)){ Application.LoadLevel(1);}
		if (Input.GetKeyDown(KeyCode.F5)){
			Application.LoadLevel(0);
			bool platOnMac = GetComponent<platformInitializer>().onMac;
			GetComponent<platformInitializer>().onMac = !platOnMac;
		}
	}
}
