using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems; 

public class multiplayerMenuControls : MonoBehaviour {

	public GameObject[] screens;
	public GameObject[] firstButtons;
	public EventSystem eventSystem;
	public GameObject tutorialObject;
	public int levelToBegin;
	
	// Use this for initialization
	void Start () {
		eventSystem.SetSelectedGameObject(firstButtons[0]);
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
		levelToBegin = levelToStart;
		ShowScreen(2);
	}
	
	public void StartSelectedLevel(){
		Application.LoadLevel(levelToBegin);
	}
	
	public void StartTutorial(){
		GameObject newTutorialObject = (GameObject)Instantiate(tutorialObject, Vector3.zero, Quaternion.identity);
		newTutorialObject.GetComponent<NextLevel>().nextLevelInt = levelToBegin;
		DontDestroyOnLoad(newTutorialObject);
		Application.LoadLevel(1);
	}
	
	public void QuitGame(){
		Application.Quit();
	}
	
}
