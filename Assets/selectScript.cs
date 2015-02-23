using UnityEngine;
using System.Collections;

public class selectScript : MonoBehaviour {

	public LayerMask mask;
	public float range = 500;
	private GameObject currentPlanet;
	private GameObject selectedPlanet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
Debug.DrawRay(transform.position, transform.forward * range, Color.green);
		CheckForInput();	
		CheckToHighlight();
		if (currentPlanet!=null){
			HighlightPlanet();
		}
		if (selectedPlanet!=null){
			HighlightSelected();
//print ("planet selected");
		}
	}
	
	void CheckForInput(){
		if (Input.GetButtonDown("Fire1")){
			CheckForTarget();
		}	
	}
	
	void CheckForTarget(){
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, range, mask)){
			if (selectedPlanet!=null){
//print ("hit second planet");
				selectedPlanet.transform.GetComponent<planetInventory>().SendSpores(hit.transform);
			//	hit.GetComponent<highlighter>().Halo();
				selectedPlanet = null;
			}
			else{
				selectedPlanet = hit.transform.gameObject;
			}
			return;
		}
		else {
			selectedPlanet.GetComponent<highlighter>().DeactivateHighlight();
			selectedPlanet = null;
			return;
		}
	}
	
	void CheckToHighlight(){
		RaycastHit highlightHit;
		if (Physics.Raycast (transform.position, transform.forward, out highlightHit, range, mask)){
			currentPlanet = highlightHit.transform.gameObject;
			return;
		}
		else{
			currentPlanet = null;
			return;
		}
	}
	
	void HighlightPlanet(){
		currentPlanet.GetComponent<highlighter>().SetLit(1);
	}
	
	void HighlightSelected(){
		selectedPlanet.GetComponent<highlighter>().SetLit(2);
	}
}
