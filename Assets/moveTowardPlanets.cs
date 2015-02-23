using UnityEngine;
using System.Collections;

public class moveTowardPlanets : MonoBehaviour {

	public Rigidbody cog;
	public Transform gravityTarget;
	public float gravityAmount, yOffset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		cog.transform.position = new Vector3 (transform.position.x, transform.position.y - yOffset, transform.position.z);
		Vector3 direction = gravityTarget.transform.position - cog.transform.position;
		Debug.DrawRay(transform.position, direction, Color.blue);
		cog.rigidbody.AddForce (direction*Time.deltaTime * gravityAmount);
	}
}
