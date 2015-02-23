/* #pragma strict

var goodSpore : GameObject;
var badSpore : GameObject;
//var alliegence 
var planetRadius = 3f;
var mask : LayerMask;

function Start () {

}

function Update () {
	if (Input.GetButtonDown("Fire1")){
	//	CreateSpore();
	}
	
}

function CreateSpore(){	
	var newSpore : GameObject = Instantiate(spore, transform.position, Quaternion.identity);
	newSpore.transform.rotation = Random.rotation;
	//newSpore.transform.position = Vector3.up * planetRadius;
	
	var direction : Vector3;
	direction = transform.position - newSpore.transform.position;
	
	var distance : RaycastHit;
	Physics.Raycast(newSpore.transform.position, direction, distance, Mathf.Infinity , mask);
	newSpore.transform.position = distance.point; 
}	 */