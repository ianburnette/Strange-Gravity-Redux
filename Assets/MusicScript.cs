using UnityEngine;
using System.Collections;

public class MusicScript : MonoBehaviour {

	public AudioClip strangeGravity, boring, exciting;
	public float musicVolume = 0.7f;

	// Use this for initialization
	void Start () {
		if (Application.loadedLevel!=0 && Application.loadedLevel!=1){
			PlayerPrefs.SetInt("progress", Application.loadedLevel);
			print ("progress is " + PlayerPrefs.GetInt("progress"));
		}
		GameObject already;
		already = GameObject.FindGameObjectWithTag("music");
		if (already!=null){
			Destroy(gameObject);
			return;
		}
		else{
			tag = "music";
		}
		DontDestroyOnLoad(gameObject);
		SetChildVolumes(0f);
		CheckLevel();
	}
	
	void OnLevelWasLoaded(){
		CheckLevel();
		if (Application.loadedLevel!=0 && Application.loadedLevel!=1){
			PlayerPrefs.SetInt("progress", Application.loadedLevel);
			print ("progress is " + PlayerPrefs.GetInt("progress"));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void SetChildVolumes(float vol){
		transform.GetChild(0).audio.volume = vol;
		transform.GetChild(1).audio.volume = vol;
	}
	
	void CheckLevel(){
	//	print (Application.loadedLevel);	
		if (Application.loadedLevel>1 && Application.loadedLevel<12){
			audio.Stop();
			SetChildVolumes(musicVolume);
			transform.GetChild(1).audio.volume = 0;
		}
		else if (Application.loadedLevel == 12){
			SetChildVolumes(0);
			audio.Play();
		}
	}
	
	public void SetExcitingVolume(int amt){
		if (amt == 1){
			transform.GetChild(1).audio.volume = musicVolume;
		}
		else if (amt == 0){
			transform.GetChild(1).audio.volume = 0;
		}
	}
}
