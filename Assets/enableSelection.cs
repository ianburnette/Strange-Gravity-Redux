using UnityEngine;
using System.Collections;

public class enableSelection : MonoBehaviour {

	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider col) {
		if (col.transform.tag == "Player"){
			col.transform.GetChild(0).GetComponent<multiSelect>().enabled= true;
		}
	}
}
