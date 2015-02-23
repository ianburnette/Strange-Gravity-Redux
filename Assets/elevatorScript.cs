using UnityEngine;
using System.Collections;

public class elevatorScript : MonoBehaviour {

	Animator anim;
	public bool arriving, departing, doorOpen, arriveElevator;
	GameObject player, gm;
	public GameObject elevatorItself;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find("_GM");
		anim = GetComponent<Animator>();
		player = GameObject.Find ("Player");
		anim.SetBool("arriving", arriving);
		anim.SetBool("departing", departing);
		anim.SetBool("doorOpen", doorOpen);
		if (arriveElevator){
			player.transform.parent = elevatorItself.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider col){
		
		if (col.transform.gameObject == player){
			
			if (!arriveElevator){
				//print ("succesfully entered trigger");
				departing = true;
				doorOpen = false;
				anim.SetBool("depart", true);
				anim.SetBool("doorOpen", doorOpen);
				//player.transform.parent = elevatorItself.transform;
				gm.GetComponent<levelProgress>().NextLevel();
			}
			else{
				Invoke("KeepDoorOpen", 3);
			}
		}	
	}
	
	void KeepDoorOpen(){
		doorOpen = true;
		anim.SetBool("doorOpen", doorOpen);
		//player.transform.parent = null;
	}
}
