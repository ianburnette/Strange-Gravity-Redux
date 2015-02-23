using UnityEngine;
using System.Collections;

public class sporeWander : MonoBehaviour {

	public Transform planet;
	public float minLength, maxLength, minStrength, maxStrength;
	public float length, strength;

	// Use this for initialization
	void Start () {
		length = Random.Range (minLength, maxLength);
		strength = Random.Range (minStrength, maxStrength);
		//planet = transform.parent;
		InvokeRepeating("Wander", length, length);
	}
	
	void Wander(){
	//	rigidbody.AddForce(transform.up * strength);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		/* transform.position = 
							RotatePointAroundPivot(transform.position,
								transform.parent.position,
								Quaternion.Euler(0, OrbitDegrees * Time.deltaTime, 0)); */
	
		/* Vector3 diff = transform.parent.position - transform.position;
		Vector3 direction = diff.normalized;
		float gravitationForce = (strength) / diff.sqrMagnitude;
		rigidbody.AddForce(direction * gravitationForce);
		//transform.RotateAround(transform.parent.position, transform.position - transform.parent.position, strength); */
	}
	
	public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle) {
		return angle * ( point - pivot) + pivot;
	}
}
