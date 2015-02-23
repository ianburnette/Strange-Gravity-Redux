using UnityEngine;
using System.Collections;

public class platformInitializer : MonoBehaviour {

	public bool onMac;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
	void OnLevelWasLoaded(){
		if (Application.loadedLevel!=0){
			GameObject.Find("_gm").GetComponent<platformManagement>().SetupPlatform(onMac);
		}
		else{
			GameObject[] inits = GameObject.FindGameObjectsWithTag("init");
			if (inits.Length > 1){
				Destroy(gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
