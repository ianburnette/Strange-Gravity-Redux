using UnityEngine;
using System.Collections;

public class tutButtonPress : MonoBehaviour {

	public GameObject player;
	public GameObject gm;
	private RaycastHit hit;
	private bool depressed = false;
	public Transform button;
	public Shader highlightShader;
	public Shader originalShader;
	public float range = 4;
	public LayerMask mask;
	private Animator anim;

	// Use this for initialization
	void Start () {
		originalShader = button.renderer.material.shader;
		player = GameObject.Find("Player");
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, range, mask)){
		//	print ("hitting anything");
			if (hit.transform.tag == "button" && !depressed){
			//	print ("hitting button");
				Highlight();
				if (Input.GetButtonDown("Fire1")){
					Press();
				}
			}
		}
		else{
			UnHighlight();
		}
	}
	
	void Press(){
		depressed = true;
		anim.SetTrigger("pressed");
		gm.GetComponent<levelProgress>().Toggle(true);// = true;
		player.transform.GetChild(0).GetComponent<multiSelect>().enabled=true;
	}
	
	void Highlight(){
		button.renderer.material.shader = highlightShader;
	}
	
	void UnHighlight(){
		button.renderer.material.shader = originalShader;
	}
}
