using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public int progress = 0;

	// Use this for initialization
	void Start () {	
		if (PlayerPrefs.GetInt("progress") == 0){
			PlayerPrefs.SetInt("progress", 1);
		}
		progress = PlayerPrefs.GetInt("progress");
		print (transform);
		print ("progress is " + PlayerPrefs.GetInt("progress"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void NextLevel(){
		
			progress = PlayerPrefs.GetInt("progress");
			print ("loading level " + progress);
			if (progress==0){
				progress=1;
			}
		Application.LoadLevel(progress);
	}
	
	public void NewGame(){
		PlayerPrefs.SetInt("progress", 1);
		progress = PlayerPrefs.GetInt("progress");
		print ("reset progress is " + PlayerPrefs.GetInt("progress"));
	}
	
	public void Quit(){
		Application.Quit();
	}
}
