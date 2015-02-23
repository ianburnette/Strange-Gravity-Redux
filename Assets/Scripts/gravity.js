#pragma strict
/* 
var rocks : GameObject[];
var gravityAmount : float;

function Start () {
	rocks = GameObject.FindGameObjectsWithTag("planet");
}

function Update () {
	for (var rock : GameObject in rocks){
		var direction = transform.position - rock.transform.position;
		rock.rigidbody.AddForce(direction * Time.deltaTime * gravityAmount);
		Debug.DrawRay(rock.transform.position, transform.TransformDirection(Vector3.down), Color.red);
	}
} */

var planet : GameObject;
var gravityAmount : float = 5;
var hoverAmount : float = 5;
var mask : LayerMask;
var orbit : float;

function Start(){
	//planets = GameObject.FindGameObjectsWithTag("planet");
	//planet = GameObject.Find("planet1");
}

function SetPlanet(newPlanet : Transform){
	planet = newPlanet.gameObject;
	
	//planet.SendMessage("newSpore", gameObject);
}

function Die(){
	this.enabled = false;
		transform.parent.SendMessage("RemoveSpore", gameObject, SendMessageOptions.DontRequireReceiver);
}

function Attacked(){
	transform.parent.SendMessage("RemoveSpore", gameObject, SendMessageOptions.DontRequireReceiver);
	this.enabled = false;
}

function Update(){
	var direction : Vector3;
	direction = planet.transform.position - transform.position;
	rigidbody.AddForce(direction * Time.deltaTime * gravityAmount);
	Debug.DrawRay(transform.position, direction, Color.red);
	//	print (Vector3.Distance(planet.transform.position, transform.position));
	if (Vector3.Distance(planet.transform.position, transform.position) < orbit){
		rigidbody.AddForce(direction * Time.deltaTime * hoverAmount);
	
	}
	
	/* var distance : RaycastHit; 
	distance = Physics.Raycast(transform.position, direction, Mathf.infinity, mask);
	if (distance. */
		
		/* 
		for (var planet : GameObject in planets){
			
		} */
	
	
}