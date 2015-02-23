using UnityEngine;
using System.Collections;

public class multiSelect : MonoBehaviour {

	public LayerMask mask;
	public bool ableToSelect = true;
	public float range = 2000;
	private GameObject currentPlanet;
	public GameObject[] selectedPlanets = new GameObject[10];
	public GameObject[] doubleSelectedPlanets = new GameObject[6];
	private RaycastHit hit;
	playerSounds soundScript;
	bool currentlyHitting = false;
	
	void Start(){
		soundScript = GetComponent<playerSounds>();
	}
	
	// Update is called once per frame
	void Update () {
Debug.DrawRay(transform.position, transform.forward*range, Color.green);
		if (ableToSelect){
			CheckForInput();
		}
		CheckToHighlight();
		KeepSelectedHighlighted();
		print (Input.GetAxisRaw("Fire1"));
	}
	
	public void SetSelect(){
		ableToSelect = !ableToSelect;
	}	
	
	void CheckForInput(){
		if (Input.GetButtonDown("Fire1")){
			CheckToSelect();
		}
		if (Input.GetButtonDown("Fire2")){
			CheckToSend();
		}
	}
	
	void CheckToHighlight(){
				
		RaycastHit highlightHit;
		if (Physics.Raycast (transform.position, transform.forward, out highlightHit, range, mask)){
			if (highlightHit.transform.tag != "floor"){
					highlightHit.transform.GetComponent<highlighter>().SetLit(1);
					if (!currentlyHitting){
						soundScript.PlayRollover();
						currentlyHitting = true;
					}
			}
			//currentlyHitting = true;
		}
		else{
			if (currentlyHitting){
				soundScript.PlayRollover();
				currentlyHitting = false;
			}
		}
	}	
	
	void CheckToSelect(){
		if (Physics.Raycast(transform.position, transform.forward, out hit, range, mask)){
			print ("hit is " + hit.transform.tag);
			if (hit.transform.tag == "goodPlanet"){
				for (int i = 0; i<selectedPlanets.Length;i++){
					if (selectedPlanets[i] == hit.transform.gameObject){
						//print ("double select");
						hit.transform.GetComponent<highlighter>().SetLit(3);
						doubleSelectedPlanets[i] = hit.transform.gameObject;
						soundScript.PlaySelect();
						break;
					}
					else if (selectedPlanets[i] == null){
						hit.transform.GetComponent<highlighter>().SetLit(2);
						selectedPlanets[i] = hit.transform.gameObject;
						soundScript.PlaySelect();
						break;
					}
				}
			}
			else if (hit.transform.tag == "floor"){	
				print ("entered floor loop");
				soundScript.PlayDeselect();
				for (int i = 0; i<selectedPlanets.Length;i++){
				if (selectedPlanets[i] != null){
					selectedPlanets[i].transform.GetComponent<highlighter>().DeactivateHighlight();
					selectedPlanets[i] = null;
				}
				
				}
				for (int i = 0; i<doubleSelectedPlanets.Length;i++){
					if (doubleSelectedPlanets[i] != null){
						doubleSelectedPlanets[i].transform.GetComponent<highlighter>().DeactivateHighlight();
						doubleSelectedPlanets[i] = null;
					}
						
				}
				/* for (int i = 0; i<selectedPlanets.Length;i++){
					selectedPlanets[i].transform.GetComponent<highlighter>().DeactivateHighlight();
					selectedPlanets[i] = null;
				} */
				/* for (int i = 0; i<doubleSelectedPlanets.Length;i++){
					doubleSelectedPlanets[i].transform.GetComponent<highlighter>().DeactivateHighlight();
					doubleSelectedPlanets[i] = null;
				} */
			}
		}
		else{
			/* for (int i = 0; i<selectedPlanets.Length;i++){
				if (selectedPlanets[i] != null){
					selectedPlanets[i].transform.GetComponent<highlighter>().DeactivateHighlight();
					selectedPlanets[i] = null;
				}
				
			}
			for (int i = 0; i<doubleSelectedPlanets.Length;i++){
				if (doubleSelectedPlanets[i] != null){
					doubleSelectedPlanets[i].transform.GetComponent<highlighter>().DeactivateHighlight();
					doubleSelectedPlanets[i] = null;
				}
					
			} */
		}
	}
	
	void KeepSelectedHighlighted(){
		for (int i = 0; i<selectedPlanets.Length;i++){
			if (selectedPlanets[i] != null){
				selectedPlanets[i].transform.GetComponent<highlighter>().SetLit(2);
				if (selectedPlanets[i].transform.tag == "badPlanet"){
					selectedPlanets[i].transform.GetComponent<highlighter>().DeactivateHighlight();
					selectedPlanets[i] = null;
				}
			}
		}
		for (int i = 0; i<doubleSelectedPlanets.Length;i++){
			if (doubleSelectedPlanets[i] != null){
				doubleSelectedPlanets[i].transform.GetComponent<highlighter>().SetLit(3);
				if (doubleSelectedPlanets[i].transform.tag == "badPlanet"){
					doubleSelectedPlanets[i].transform.GetComponent<highlighter>().DeactivateHighlight();
					doubleSelectedPlanets[i] = null;
				}
			}
		}
	}
	
	void CheckToSend(){
		if (Physics.Raycast(transform.position, transform.forward, out hit, range, mask)){

			//if (hit.transform.tag == "goodPlanet"){
				for (int i = 0; i<selectedPlanets.Length; i++){
					if (selectedPlanets[i] == null){
						//print ("breaking at " + i);
					//	break;
					}
					if (selectedPlanets[i] != hit.transform.gameObject && selectedPlanets[i]!=null){
						selectedPlanets[i].GetComponent<InventoryScript>().Transfer(hit.transform,2);
					}
					if (selectedPlanets[i]!=null){
						selectedPlanets[i].transform.GetComponent<highlighter>().DeactivateHighlight();
						selectedPlanets[i] = null;
					}
				}
				for (int i = 0; i<doubleSelectedPlanets.Length; i++){
					if (doubleSelectedPlanets[i] == null){
					//	break;
					}
					if (doubleSelectedPlanets[i] != hit.transform.gameObject && 			doubleSelectedPlanets[i] != null){
						doubleSelectedPlanets[i].GetComponent<InventoryScript>().Transfer(hit.transform,1);
					}
					if (doubleSelectedPlanets[i] != null){
						doubleSelectedPlanets[i].transform.GetComponent<highlighter>().DeactivateHighlight();
						doubleSelectedPlanets[i] = null;
					}
				}
				soundScript.PlaySend();
			/* 	for (int i = 0; i<selectedPlanets.Length; i++){
					if (selectedPlanets[i] == null){
					//	break;
					}
					
				}
				for (int i = 0; i<doubleSelectedPlanets.Length; i++){
					if (doubleSelectedPlanets[i] == null){
					//	break;
					}
					doubleSelectedPlanets[i].transform.GetComponent<highlighter>().DeactivateHighlight();
					doubleSelectedPlanets[i] = null;
				} */
			//}
		}
		else{
		/* 	for (int i = 0; i<selectedPlanets.Length;i++){
				if (selectedPlanets[i] == null){
						break;
				}
				selectedPlanets[i].transform.GetComponent<highlighter>().DeactivateHighlight();
				selectedPlanets[i] = null;
			} */
		}
	}
}
