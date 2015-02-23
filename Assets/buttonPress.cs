using UnityEngine;
using System.Collections;

public class buttonPress : MonoBehaviour {

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
	
	public GameObject sun, smallLight, dirLight;
	public Shader sunMat, diffuseShader;
	
	bool triggerOnce = false;

	// Use this for initialization
	void Start () {
		originalShader = button.renderer.material.shader;
		player = GameObject.Find("Player");
		anim = GetComponent<Animator>();
		sunMat = sun.renderer.material.shader;
		dirLight.active = false;
		diffuseShader = Shader.Find("Diffuse");
		highlightShader = Shader.Find("Unlit/Texture");
		sun.renderer.material.shader = diffuseShader;
		Component halo = sun.GetComponent("Halo"); 
		halo.GetType().GetProperty("enabled").SetValue(halo, false, null); 
		player.transform.GetChild(0).GetComponent<multiSelect>().enabled = false;
		//sun.GetComponent("Halo").size = 0;
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
		player.transform.GetChild(0).GetComponent<multiSelect>().enabled = true;
		dirLight.active = true;
		sun.renderer.material.shader = sunMat;
		Component halo = sun.GetComponent("Halo"); 
		halo.GetType().GetProperty("enabled").SetValue(halo, true, null); 
		smallLight.active = false;
		gm.GetComponent<levelProgress>().Toggle(true);// = true;
		print ("button thinks level is " + Application.loadedLevel);
		if (Application.loadedLevel!=1 && Application.loadedLevel != 12){
			GameObject music = GameObject.FindGameObjectWithTag("music");
			music.GetComponent<MusicScript>().SetExcitingVolume(1);
		}
	}
	
	void Highlight(){
		button.renderer.material.shader = highlightShader;
	}
	
	void UnHighlight(){
		button.renderer.material.shader = originalShader;
	}
	
	void OnTriggerEnter(Collider col){
		if (!triggerOnce){
			Press();
			triggerOnce = true;
		}
	}
}
