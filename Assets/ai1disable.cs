using UnityEngine;
using System.Collections;

public class ai1disable : MonoBehaviour {

	public GameObject planet1, planet2, AI;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerExit (Collider col){
		if (col.transform.tag == "Player"){
			planet1.tag = "planet";
			planet2.tag = "planet";
			AI.active = false;//planet1.tag = "planet";
		}
	}
}
