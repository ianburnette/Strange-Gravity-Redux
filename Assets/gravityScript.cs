using UnityEngine;
using System.Collections;

public class gravityScript : MonoBehaviour {

	public GameObject planet;
	public float gravityAmount = 5f;
	public LayerMask mask;
	public GameObject target;
	
	public TrailRenderer trail;
	private float vel = .15f;
	
	// Use this for initialization
	
	void Start(){
		trail = GetComponent<TrailRenderer>();//renderer.TrailRenderer;
	}
	
	public void SetPlanet (Transform newPlanet){
		if (transform.tag != "Untagged"){
			transform.parent = newPlanet;
			planet = newPlanet.gameObject;
		}
		/* if (planet.transform.tag == "goodPlanet" && transform.tag == "badSpore"){
			target = planet.GetComponent<InventoryScript>().GetTarget(1);
		} */
	}
	
	// Update is called once per frame
	void Update () {
		TrailHandle();
		Vector3 direction;
		if (planet != null){
			direction = planet.transform.position - transform.position;
			rigidbody.AddForce (direction*Time.deltaTime * gravityAmount);
//Debug.DrawRay(transform.position, direction, Color.red);
		}
	}
	
	public void Attacked(){
		this.enabled=false;
	}
	
	public void Die(){
		this.enabled=false;
	}
	
	void TrailHandle(){
//print (rigidbody.velocity.magnitude);
		if (rigidbody.velocity.magnitude >= vel){
			trail.enabled = true;
		}
		else{
			trail.enabled = false;
		}
	}
}
