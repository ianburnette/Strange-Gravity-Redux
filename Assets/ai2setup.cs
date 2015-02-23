using UnityEngine;
using System.Collections;

public class ai2setup : MonoBehaviour {


	public GameObject planet1, planet2, planet3, planet4, AI;
	public bool entered = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	/* void LateUpdate () {
		if (entered){
			planet1.tag = "Untagged";
			planet2.tag = "Untagged";
			planet3.tag = "Untagged";
			planet4.tag = "Untagged";
		}
	} */
	
	void OnTriggerEnter (Collider col) {
		if (col.transform.tag == "Player" && AI.active == false){
			planet1.GetComponent<InventoryScript>().enabled = false;
			planet1.GetComponent<SporeSpawner>().enabled = false;
			planet1.GetComponent<planetProperties>().enabled = false;
			planet1.tag = "Untagged";
			planet1.active=false;
			planet2.GetComponent<InventoryScript>().enabled = false;
			planet2.GetComponent<SporeSpawner>().enabled = false;
			planet2.GetComponent<planetProperties>().enabled = false;
			planet2.tag = "Untagged";
			planet2.active=false;
			planet3.GetComponent<InventoryScript>().enabled = false;
			planet3.GetComponent<SporeSpawner>().enabled = false;
			planet3.GetComponent<planetProperties>().enabled = false;
			planet3.tag = "Untagged";
			planet3.active=false;
			planet4.GetComponent<InventoryScript>().enabled = false;
			planet4.GetComponent<SporeSpawner>().enabled = false;
			planet4.GetComponent<planetProperties>().enabled = false;
			planet4.tag = "Untagged";
			planet4.active=false;
			entered = true;
			col.transform.GetChild(0).GetComponent<multiSelect>().enabled = false;
		}
	}
}
