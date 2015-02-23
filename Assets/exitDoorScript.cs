using UnityEngine;
using System.Collections;

public class exitDoorScript : MonoBehaviour {

	Animator anim;
	public GameObject gm;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
			anim.speed = 0;
		gm = GameObject.Find("_GM");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OpenDoor(){
		if (name != "exitGame"){
		
			anim.SetTrigger("open");
			anim.speed = 1;
			Invoke ("Freeze", 6);
		}
		else{
			GetComponent<BoxCollider>().enabled = true;
		}	
	}
	
	void Freeze(){
		anim.speed = 0;
	}
	
	void OnTriggerEnter(Collider col){
		if (col.tag == "Player"){
			gm.GetComponent<levelProgress>().NextLevel();
		}
	}
}
