using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems; 

public class multiplayerMenuControls : MonoBehaviour {

	public GameObject[] screens;
	public GameObject[] firstButtons;
	public EventSystem eventSystem;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void ShowScreen (int screenToShow){
		for (int i = 0; i<screens.Length; i++){
			if (i == screenToShow){
				screens[i].SetActive(true);
				//print ("screen " + i + " selected, with child " + screens[i].transform.GetChild(0).gameObject);
				eventSystem.SetSelectedGameObject(screens[i].transform.GetChild(0).gameObject);
			}else{
				screens[i].SetActive(false);
				//eventSystem.SetSelectedGameObject(screens[i].transform.GetChild(0).gameObject);
			}
		}			
	}
	
	public void StartLevel(int levelToStart){
		Application.LoadLevel(Application.loadedLevel+levelToStart);
	}
	
	
}
