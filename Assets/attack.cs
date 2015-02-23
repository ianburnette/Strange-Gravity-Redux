using UnityEngine;
using System.Collections;

public class attack : MonoBehaviour {

	public GameObject target;
	public float speed = 20f;
	public float minMag = .3f;
	bool lostOne = false;

	// Use this for initialization
	void Start () {
		if (Application.loadedLevel==12){
			speed = 120f;
		}
		InvokeRepeating("CheckLost", 2, 2);
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		if (target != null){
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
		}
	
	}
	
	public void Attack (GameObject newTarget){
		target = newTarget;
	}
	
	public void  Die(){
		this.enabled = false;
	}
	
	public void  Death(){
		this.enabled = false;
	}
	
	public void Attacked(){
		this.enabled = false;
	}
	
	public void CheckLost(){
		if (rigidbody.velocity.magnitude<minMag){
			if (!lostOne){
				lostOne = true;
			}
			else {
				target = null;
				lostOne = false;
			}
		}
	}
	
}
