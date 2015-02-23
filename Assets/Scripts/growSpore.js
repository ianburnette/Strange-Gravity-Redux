#pragma strict

var growTime : float; 
var scale : float;
var scaleSpeed : float;

function Start () {
	growTime = Random.Range (5,20);
	scale = Random.Range (transform.parent.localScale.x/11, transform.parent.localScale.x/9);
	scaleSpeed = Random.Range (30, 70); 
	transform.localScale = Vector3.zero;
}

function Update () {
	if (transform.localScale.x < scale){
		transform.localScale += new Vector3(1,1,1) * (growTime/scaleSpeed) * Time.deltaTime;
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

function Die(){
	scale = 0;
}

function Attacked(){
	scale = 0;
}