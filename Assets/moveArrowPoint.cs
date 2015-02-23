using UnityEngine;
using System.Collections;

public class moveArrowPoint : MonoBehaviour {

	public float moveSpeed, margin;
	public Transform destination;
	

	void Update () {
		
	
		//transform.Translate(Vector3.left * moveSpeed);
		if (destination!=null){
			transform.position = Vector3.MoveTowards(transform.position, destination.position, moveSpeed);
			if (Vector3.Distance(transform.position, destination.position) <= margin){
				Destroy(gameObject);
			}
			transform.LookAt(destination.position);
			transform.Rotate(Vector3.left);
		}
	}
	
	void OnTriggerEnter(Collider coll){
		print (coll.transform.tag);
		if (coll.transform.tag == "goodPlanet" || 
			coll.transform.tag == "badPlanet" || 
			coll.transform.tag == "planet"){
				Destroy(gameObject);
			}
	}
}
