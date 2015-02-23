using UnityEngine;
using System.Collections;

public class newGrowSpore : MonoBehaviour {

public float growTime, scale, scaleSpeed;
public Vector3 parentScale, targetScale, baseTargetScale;
public float scaleDivisor;
RotateAndOrbit orbitScript;

	void Start () {
		orbitScript = GetComponent<RotateAndOrbit>();
		parentScale = transform.parent.parent.localScale;
		targetScale = new Vector3(baseTargetScale.x / parentScale.x,baseTargetScale.y/ parentScale.y,baseTargetScale.z/ parentScale.z);
		//targetScale = new Vector3(parentScale.x * scaleDivisor,parentScale.y * scaleDivisor,parentScale.z * scaleDivisor);
		growTime = Random.Range (5,20);
		//scale = Random.Range (transform.parent.localScale.x/11, transform.parent.localScale.x/9);
		scaleSpeed = Random.Range (30, 70); 
		transform.localScale = Vector3.zero;
	}

	void FixedUpdate () {
		if (transform.localScale.x < targetScale.x){
			transform.localScale += new Vector3(1,1,1) * (growTime/scaleSpeed) * Time.deltaTime;
		}
		else if (transform.localScale.x >= targetScale.x){
			orbitScript.enabled=true;
			this.enabled = false;
		}
		else if (scale == 0){
			transform.localScale -= new Vector3(1,1,1) * (growTime/scaleSpeed) * Time.deltaTime/2;
			if (transform.localScale.x <= 0){
				Destroy(gameObject);
			}
		/* 	var gravityScript : gravity;
			gravityScript = GetComponent("gravity");
			gravityScript.enabled = true; */
		}
	}
	
	public void FinishScaling(){
		transform.localScale = targetScale;
		this.enabled=false;
	}
}
