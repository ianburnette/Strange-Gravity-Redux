using UnityEngine;
using System.Collections;

public class createMusic : MonoBehaviour {

	public GameObject manager;

	// Use this for initialization
	void Start () {
		//manager = Resources.Load("MusicManager");
		GameObject already;
		already = GameObject.FindGameObjectWithTag("music");
		if (already==null){
			manager = Resources.Load("MusicManager") as GameObject;
			Instantiate(manager, transform.position, transform.rotation);
		}
		else{
			//tag = "music";
		}
		//DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
