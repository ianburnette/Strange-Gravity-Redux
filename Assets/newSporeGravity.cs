using UnityEngine;
using System.Collections;
//using UnityEditor;

public class newSporeGravity : MonoBehaviour {

	public float minDist = 2f;
	public bool detached = false;
	public int alignment;
	public bool attacking;
	public bool orbiting;
	public Transform gravityTarget, attackTarget;
	public float gravityAmount, attackSpeed, waitTime;
	private float baseAttackSpeed;
	newShrinkSpore shrinkScript;
	newGrowSpore growScript;
	RotateAndOrbit orbitScript;

	void Start(){
		baseAttackSpeed = attackSpeed;
		shrinkScript = GetComponent<newShrinkSpore>();
		growScript = GetComponent<newGrowSpore>();
		orbitScript = GetComponent<RotateAndOrbit>();
	}
	
	void FixedUpdate(){
transform.localScale = transform.localScale;
		if (attacking && attackTarget == null && shrinkScript.enabled == false){
			//gravityTarget.parent.GetComponent<newSporeLists>().Add(transform, alignment);
			shrinkScript.enabled=true;
		}
		else if (attackTarget == null && !attacking && gravityTarget != null){
			Vector3 direction = gravityTarget.transform.position - transform.position;
			//Debug.DrawRay(transform.position, direction, Color.blue);
			rigidbody.AddForce (direction*Time.deltaTime * gravityAmount);
		}
		else{
			if (!detached){
				detached = true;
				Detach();
			}
			if (!attacking){
				attacking=true;
			}
			if (attackTarget == null){
				return;
			}
			CheckProximity();
			if (orbitScript.enabled){
				ToggleOrbit(false);
			}
			Vector3 direction = attackTarget.transform.position - transform.position;
			Debug.DrawRay(transform.position, direction, Color.red);
			//rigidbody.AddForce (direction*Time.deltaTime * attackSpeed);
		//	transform.parent = null;
			transform.position = Vector3.MoveTowards(transform.position, attackTarget.position, attackSpeed * Time.deltaTime);
			//Destroy (gameObject);
		}
		
	}
	
	void CheckProximity(){
		if (Vector3.Distance(transform.position, attackTarget.transform.position)<minDist){
			StartGravity(attackTarget.rigidbody);
			if (attackSpeed==0)
				attackSpeed = baseAttackSpeed;
		}
	}	
	
	public void Detach(){
		//transform.parent.parent.GetComponent<newSporeLists>().Remove(transform);
	//	print ("removed");
		//EditorApplication.isPaused = true;
	}
	
	void OnCollisionEnter(Collision coll){
		if (transform == attackTarget){
			StartGravity(coll.rigidbody);
			if (attackSpeed==0)
				attackSpeed = baseAttackSpeed;
			print ("collided with target");
		}
		
		
		if (transform.tag == "goodSpore"/*  && attackTarget == null */){
			if (coll.transform.tag == "badASpore" || coll.transform.tag == "badBSpore" || coll.transform.tag == "badCSpore"){
				//attackTarget = coll.transform;
				StartGravity(coll.rigidbody);//rigidbody.useGravity = true;
				if (attackSpeed==0)
					attackSpeed = baseAttackSpeed;
				
				//attacking = true;
				//coll.rigidbody.useGra
			}
		}
		if (transform.tag == "badASpore"/*  && attackTarget == null */){
			if (coll.transform.tag == "goodSpore" || coll.transform.tag == "badBSpore" || coll.transform.tag == "badCSpore"){
				//attackTarget = coll.transform;
				StartGravity(coll.rigidbody);
				if (attackSpeed==0)
					attackSpeed = baseAttackSpeed;
				//attacking = true;
			}
		}
		if (transform.tag == "badBSpore" /* && attackTarget == null */){
			if (coll.transform.tag == "badASpore" || coll.transform.tag == "goodSpore" || coll.transform.tag == "badCSpore"){
				//attackTarget = coll.transform;
				StartGravity(coll.rigidbody);
				if (attackSpeed==0)
					attackSpeed = baseAttackSpeed;
				//attacking = true;
			}
		}
		if (transform.tag == "badCSpore" /* && attackTarget == null */){
			if (coll.transform.tag == "badASpore" || coll.transform.tag == "badBSpore" || coll.transform.tag == "goodSpore"){
				//attackTarget = coll.transform;
				StartGravity(coll.rigidbody);
				if (attackSpeed==0)
					attackSpeed = baseAttackSpeed;
				//attacking = true;
			}
		}
		if (coll.transform == transform.parent && !orbiting){
			if (attackTarget == null){
				Invoke("StartOrbit", Random.Range(waitTime/2f, waitTime*1.5f));
				orbiting = true;
			}
		}		
	}
	
	void OnCollisionStay(Collision coll){
		if (coll.transform == transform.parent && !orbiting){
			if (attackTarget == null){
				//Invoke("StartOrbit", Random.Range(waitTime/2f, waitTime*1.5f));
				//orbiting = true;
			}
		}
	}
	
	public void Transfer(Transform newPlanet){
		ToggleOrbit(false);
		orbiting = false;
		//StartCoroutine("ToggleOrbit",Random.Range(waitTime/2f, waitTime*1.5f));
		if (growScript.enabled==true){
			growScript.FinishScaling();
		}
		if (transform.parent != newPlanet){
				transform.parent = newPlanet;
			}
		//orbitScript.enabled = false;
		gravityTarget = newPlanet;
		attackSpeed = baseAttackSpeed * transform.parent.parent.localScale.x;
	}
	
	void StartOrbit(){
		if (orbiting)
			ToggleOrbit(true);
		//orbiting = true;
	}
	
	void ToggleOrbit(bool status){
		orbitScript.enabled = status;
		/* if (orbitScript.enabled == true){
			orbitScript.enabled = false;
		}
		else {
			orbitScript.enabled = true;
		} */
	}
	
	void StartGravity(Rigidbody opponent){
		transform.GetComponent<newShrinkSpore>().enabled = true;
		transform.GetComponent<newShrinkSpore>().opponent = opponent;
		opponent.transform.GetComponent<newShrinkSpore>().enabled = true;
		shrinkScript.victim = true;
		
		/* rigidbody.useGravity = true;
		opponent.useGravity = true; */
	
	}

/* 	public RotateAndOrbit orbitScript;
	public bool gravityActive = false;
	public float gravityAmount, waitTime, attackSpeed;
	public Transform gravityTarget, attackTarget;

	void Start () {
		orbitScript = GetComponent<RotateAndOrbit>();
	}
	
	void FixedUpdate () {
		if (attackTarget!=null){
			//print ("attack target set to " + attackTarget);
			//Vector3 direction = -(transform.position - attackTarget.position);
			//rigidbody.AddForce (direction * attackSpeed);
			//Debug.DrawRay(transform.position, attackTarget.position, Color.green);
			
			Vector3 direction = attackTarget.transform.position - transform.position;
			Debug.DrawRay(transform.position, direction, Color.green);
			transform.position = Vector3.MoveTowards(transform.position, attackTarget.position, attackSpeed * Time.deltaTime);
			//rigidbody.AddForce (direction*Time.deltaTime * attackSpeed);
		}
		
		else if (gravityActive && gravityTarget!=null){
			Vector3 direction = gravityTarget.transform.position - transform.position;
			Debug.DrawRay(transform.position, direction, Color.blue);
			rigidbody.AddForce (direction*Time.deltaTime * gravityAmount);
		}
	
	}
	
	public void Transfer(Transform newPlanet){
		if (transform.parent != newPlanet){
				transform.parent = newPlanet;
			}
		orbitScript.enabled = false;
		gravityTarget = newPlanet;
		gravityActive = true;
	}
	
	public void OnCollisionEnter(Collision coll){
//print (coll.transform + " and " + gravityTarget);
		if (coll.transform.parent == gravityTarget && attackTarget == null){
			StartCoroutine("ResumeOrbiting");
		}
		else if (coll.transform == attackTarget){
			SporeCollide();
			coll.transform.GetComponent<newSporeGravity>().attackTarget = transform;
			coll.transform.GetComponent<newSporeGravity>().SporeCollide();
		}
	}
	
	public void SporeCollide(){
		orbitScript.enabled = false;
		gravityActive = false;
		rigidbody.useGravity = true;
		if (transform.parent != null){
			transform.parent.parent.GetComponent<newPlanetInventory>().SporeDie(transform);
			transform.parent = null;
		}
		renderer.material.shader = Shader.Find("Diffuse"); 
		GetComponent <newShrinkSpore>().enabled = true;
		
		// orbitScript.enabled = false;
		// rigidbody.useGravity = true;
		// gravityActive = false;
		// attackTarget = null;
		// transform.parent.parent.GetComponent<newPlanetInventory>().SporeDie(transform);
		// transform.parent = null;
		// renderer.material.shader = Shader.Find("Diffuse");
	}
	
	IEnumerator ResumeOrbiting(){
		yield return new WaitForSeconds(waitTime);
		// gravityActive = false;
		// orbitScript.target = gravityTarget;
		// orbitScript.enabled = true;
		// this.enabled = false;
	} */
}
