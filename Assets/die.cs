using UnityEngine;
using System.Collections;

public class die : MonoBehaviour {

	public GameObject target;
	public Transform grave;

	// Use this for initialization
	void Start () {
		grave = GameObject.Find("grave").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Attacked(){
		//GetComponent<SmoothRandomPosition>().enabled = false;
		rigidbody.useGravity=true;
		transform.tag = "deadSpore";
		if (transform.parent != grave){
			transform.parent.GetComponent<SporeSpawner>().stunned = 200;
		}
		transform.parent = grave;
		SendMessage("Death");
	}
	
	public void Die(){
		//GetComponent<SmoothRandomPosition>().enabled = false;
		rigidbody.useGravity=true;
		transform.tag = "deadSpore";
		transform.parent = grave;
		SendMessage("Death");
		Invoke ("DestroySelf", 10);
		GetComponent<TrailRenderer>().enabled=false;
		this.enabled=false;
	}
	
	void OnCollisionEnter (Collision col){
		GameObject currentTarget = GetComponent<attack>().target;
		//print ("collision");
		if (col.gameObject == currentTarget){
			if (col.transform.tag!="deadSpore"){
				SendMessage("Die");
				currentTarget.SendMessage("Attacked");
			}
			else if (col.transform.tag == "deadSpore"){
				currentTarget = transform.parent.gameObject;
			}
			else if (col.transform.tag == "goodPlanet"){
				//col.GetComponent<planetInventory>().
			}
		}
		
		/* if ((transform.tag == "badSpore" && col.transform.tag == "goodSpore") || 
			(transform.tag == "goodSpore" && col.transform.tag == "badSpore")){
				DieNow();
				col.gameObject.SendMessage("Die");
			} */
	}
	
	void DestroySelf(){
		Destroy(gameObject);
	}
}
