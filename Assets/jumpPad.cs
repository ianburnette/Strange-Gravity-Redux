using UnityEngine;
using System.Collections;

public class jumpPad : MonoBehaviour {

	public float speed = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider col) {
		if (col.tag == "player1" || col.tag == "player2"){
			col.GetComponent<FirstPersonDrifter>().JumpHigh(speed);
		}
	}
	
	void OnTriggerExit (Collider col) {
		if (col.tag == "player1" || col.tag == "player2");
			col.GetComponent<FirstPersonDrifter>().JumpHigh(1f);
	}
}

