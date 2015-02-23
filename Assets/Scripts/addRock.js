#pragma strict

var spawnLocation : Transform;
var rock : GameObject;
var rockParent : Transform;
var shootSpeed : float = 10;

function Start () {

}

function Update () {
	if (Input.GetButtonDown("Fire1")){
		var curRock = Instantiate(rock, spawnLocation.position, Quaternion.identity);
		curRock.transform.parent = rockParent;
		curRock.rigidbody.AddForce(transform.forward * shootSpeed);
	}
}