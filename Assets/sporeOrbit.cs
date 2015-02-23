using UnityEngine;
using System.Collections;

public class sporeOrbit : MonoBehaviour {

	public float repulsionSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerStay (Collider col) {
		if (col.transform.tag == "goodSpore" || col.transform.tag == "badSpore"){
			col.rigidbody.AddForce(transform.position + col.transform.position * repulsionSpeed);
		}
	}
}
