using UnityEngine;
using System.Collections;

public class newSelector : MonoBehaviour {

	public bool onMac;
	public int alignment;
	public string[] planetTags;
	public LayerMask mask;
	public float range = 2000;
	public RaycastHit highlightHit;
	private RaycastHit hit;
	public Shader highlightShader;
	private Shader defaultShader;
	public bool dragging;
	newSelectedPlanets selectedPlanetsScript;
	newPlanetInventory inventoryScript;
	multiplayerProperties multiplayerScript;
	public TrailRenderer trailObj;
	public GameObject selectorCrosshair;
	public playerSounds soundScript;
	
	public Transform currentlyHighlightedPlanet, lastSelectedPlanet;
	
	void Start () {
		multiplayerScript = GetComponent<multiplayerProperties>();
		//alignment = multiplayerScript.playerNum;
		selectedPlanetsScript = GetComponent<newSelectedPlanets>();
		inventoryScript = GetComponent<newPlanetInventory>();
		InvokeRepeating("CheckToHighlight", .1f, .1f);
	}
	
	void Update () {
		Debug.DrawRay(transform.position, transform.forward*range, Color.green);
		if (alignment == 0 && !onMac){
			if (Input.GetButtonDown("Fire1P1")){
				CheckToSelect(1);
			}
			if (Input.GetButtonDown("Fire2P1")){
				CheckToFire(1);
			}
		}else if (alignment == 0 && onMac){
			if (Input.GetButtonDown("Fire1P1MAC")){
				CheckToSelect(1);
			}
			if (Input.GetButtonDown("Fire2P1MAC")){
				CheckToFire(1);
			}
		}
		else if (alignment == 1 && !onMac){
			if (Input.GetButtonDown("Fire1P2")){
				CheckToSelect(2);
			}
			if (Input.GetButtonDown("Fire2P2")){
				CheckToFire(2);
			}
		}else if (alignment == 1 && onMac){
			if (Input.GetButtonDown("Fire1P2MAC")){
				CheckToSelect(2);
			}
			if (Input.GetButtonDown("Fire2P2MAC")){
				CheckToFire(2);
			}
		}
	}
	
	void CheckToSelect(int player){
		RaycastHit selectHit;
		//if (player == 1){
			if (Physics.Raycast (transform.position, transform.forward, out selectHit, range, mask)){
				if (selectHit.transform.tag == planetTags[alignment]){
					print ("player " + alignment + " hit planet with tag " + selectHit.transform.tag);
					int degree = selectedPlanetsScript.ClickedOn(selectHit.transform);
					if (degree == 1){ //list 1
						selectHit.transform.parent.GetComponent<newHighlightScript>().Highlight(2,alignment);
						soundScript.PlaySelect();
					} else if (degree == 2) { //list 2
						selectHit.transform.parent.GetComponent<newHighlightScript>().Highlight(3,alignment);
						soundScript.PlaySelect();
					} else{ //deselect
						selectHit.transform.parent.GetComponent<newHighlightScript>().StopSelecting(alignment);
						selectHit.transform.parent.GetComponent<newHighlightScript>().Highlight(1,alignment);
						soundScript.PlayDeselect();
					}
				}else if (selectHit.transform.tag == "floor"){
					//print ("hit " + selectHit.transform.name);
					selectedPlanetsScript.DeselectAll();
				/* 	selectHit.transform.parent.GetComponent<newHighlightScript>().StopSelecting(alignment);
					selectHit.transform.parent.GetComponent<newHighlightScript>().Highlight(1,alignment); */
					soundScript.PlayDeselect();
				}else{
					
				}
			}
		//}
	}
	
	void DragToSelect(){
		print ("dragging");
		if (!dragging){
			dragging = true;
			trailObj.enabled = true;
			selectorCrosshair.SetActive(true);
		}
	}
	
	void StopDragging(){
		dragging = false;
		//trailObj.enabled = false;
		//selectorCrosshair.SetActive(false);
	}
	
	void CheckToFire(int player){
		RaycastHit selectHit;
		if (Physics.Raycast (transform.position, transform.forward, out selectHit, range, mask)){
			print ("firing at " + selectHit.transform.name);
			selectedPlanetsScript.SendPlanets(selectHit.transform);
			soundScript.PlaySend();
		}
	}
	
	void CheckToHighlight(){
		//AM I HITTING SOMETHING?
		if (Physics.Raycast (transform.position, transform.forward, out highlightHit, range, mask)){
			if (highlightHit.transform.tag != "floor" && highlightHit.transform.name != "ravine"){ // NOT JUST HITTING THE FLOOR
				currentlyHighlightedPlanet = highlightHit.transform; // PLANET I'M HOVERING OVER IS CURRENTHIGHLIGHTED
				if (lastSelectedPlanet!=currentlyHighlightedPlanet){ //
					if (lastSelectedPlanet!=null){
						lastSelectedPlanet.transform.parent.GetComponent<newHighlightScript>().StopHighlighting(alignment);
					}
					lastSelectedPlanet=currentlyHighlightedPlanet;
					soundScript.PlayRollover();
					
					if (currentlyHighlightedPlanet.transform.tag == planetTags[alignment] || currentlyHighlightedPlanet.transform.tag == "neutralPlanet"){
						currentlyHighlightedPlanet.transform.parent.GetComponent<newHighlightScript>().Highlight(1,alignment);
					}
					else if (currentlyHighlightedPlanet.transform.tag != planetTags[4]){
						currentlyHighlightedPlanet.transform.parent.GetComponent<newHighlightScript>().Highlight(4,alignment);
					} 
					else {
						currentlyHighlightedPlanet.transform.parent.GetComponent<newHighlightScript>().Highlight(5,alignment);
					}
					
				/* 	if (currentlyHighlightedPlanet.transform.tag == "goodPlanet" || currentlyHighlightedPlanet.transform.tag == "neutralPlanet"){
						currentlyHighlightedPlanet.transform.parent.GetComponent<newHighlightScript>().Highlight(1);
					}
					else if (currentlyHighlightedPlanet.transform.tag == "badAPlanet" || 
							currentlyHighlightedPlanet.transform.tag == "badBPlanet" || 
							currentlyHighlightedPlanet.transform.tag == "badCPlanet"){
								currentlyHighlightedPlanet.transform.parent.GetComponent<newHighlightScript>().Highlight(4);
							} */
				}
			}else { // if I am hitting floor
				currentlyHighlightedPlanet = null;
			}
		}
		// I'M NOT HITTING ANYTHING
		else{
			print ("hitting nothing");
			currentlyHighlightedPlanet = null;
		}
		// UNHIGHLIGHT CURRENT HIGHLIGHTED PLANET
		if (lastSelectedPlanet != null && currentlyHighlightedPlanet == null){
			lastSelectedPlanet.transform.parent.GetComponent<newHighlightScript>().StopHighlighting(alignment);
			//lastSelectedPlanet.transform.parent.GetComponent<newHighlightScript>().Highlight(0);//lastSelectedPlanet.transform.parent.GetComponent<newHighlightScript>().StopHighlighting();
			lastSelectedPlanet = null;
		}
	}
	
	void UpdateHighlights(Transform toUpdate){
		
	}
}
