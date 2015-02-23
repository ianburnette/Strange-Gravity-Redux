using UnityEngine;
using System.Collections;

public class newPlanetAlignment : MonoBehaviour {

	public Transform player; //change this with more players
	public GameObject gm;
	scoreManagement scoreScript;

	public string[] planetTags;
	public int[] planetLayers;

	public int myCurrentAlignment;
	public float transitionTime;
	public Material[] alignmentMats;
	
	private Transform planetBody;
	private bool transitioning, ending;
	public int newMaterialIndex;
	
	void Start(){
		player = GameObject.Find("Player").transform;
		planetBody = transform.GetChild(0);
		SetMaterial(myCurrentAlignment);
		planetBody.tag = planetTags[myCurrentAlignment];
		planetBody.gameObject.layer = planetLayers[myCurrentAlignment];
		gm = GameObject.Find("_gm");
		scoreScript = gm.GetComponent<scoreManagement>();
		InvokeRepeating("DoubleCheckAlignment", 1f, 1f);
	}
	
	void DoubleCheckAlignment(){
	//print ("new Mat ind " + newMaterialIndex + " and current is " + myCurrentAlignment);
		if (newMaterialIndex != myCurrentAlignment){
			SetMaterial(myCurrentAlignment);
			//newMaterialIndex = myCurrentAlignment;
			//transitioning = true;
		}
	}
	
	public void ChangeAlignment(int newAlignment){
		if (newAlignment == 4){
			GetComponent<newHighlightScript>().StopSelecting(0);			
			GetComponent<newHighlightScript>().StopSelecting(1);			
			GetComponent<newHighlightScript>().StopHighlighting(0);	
			GetComponent<newHighlightScript>().StopHighlighting(1);	
			//player.GetComponent<newSelectedPlanets>().RemovePlanet(transform, 3);
		}
		scoreScript.Change(myCurrentAlignment, newAlignment);
		myCurrentAlignment = newAlignment;
		SetMaterial(myCurrentAlignment);
		planetBody.tag = planetTags[newAlignment];
		planetBody.gameObject.layer = planetLayers[newAlignment];
	}
	
	void SetMaterial(int alignmentNumber){//starts the material transitioning 
		transitioning = true;
		newMaterialIndex = alignmentNumber;
	}
	
	void Update(){
		if (transitioning){ //lerps between current material and newMaterialIndex material
			planetBody.renderer.material.Lerp(planetBody.renderer.material, alignmentMats[newMaterialIndex], Time.deltaTime * transitionTime);
			/* if (!ending){
				StartCoroutine("FinishTransition");
				ending=true;
			} */
		}
		//DoubleCheckAlignment();
	}
	
	IEnumerator FinishTransition(){ //officially sets the material to new one and resets bools
		yield return new WaitForSeconds(transitionTime*4);
		/* transitioning = false;
		planetBody.renderer.material = alignmentMats[newMaterialIndex];
		ending = false; */
	}

	/* public bool good, badA, badB, badC, neutral = false;
	public Material myMat, goodMat, badAMat, badBMat, badCMat, neutralMat;
	public string[] planetTags;
	public int[] planetMasks;
	public string[] mySporeTags;
	public int[] mySporeLayers;
	//public Material[] mats;
	string mySporeTag;
	int mySporeLayer;
	Transform childBody;
	
	// Use this for initialization
	void Start () {
		childBody = transform.GetChild(0);
		DetermineAlignment();
		SendSpawnCommand();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void TurnGood(){
		good = true;
		transform.tag = "goodPlanet";
		gameObject.layer = planetMasks[0];
		childBody.gameObject.layer = planetMasks[0];
		childBody.tag = "goodPlanet";
		mySporeTag = mySporeTags[0];
		mySporeLayer = mySporeLayers[0];
		myMat = goodMat;
		transform.GetChild(0).renderer.material = myMat;
	}
	
	void TurnBadA(){
		badA = true;
		transform.tag = "badAPlanet";
		gameObject.layer = planetMasks[1];
		childBody.gameObject.layer = planetMasks[1];
		childBody.tag = "badAPlanet";
		mySporeTag = mySporeTags[1];
		mySporeLayer = mySporeLayers[1];	
		myMat = badAMat;
		transform.GetChild(0).renderer.material = myMat;
	}
	
	void TurnBadB(){
		badB = true;
		transform.tag = "badBPlanet";
		gameObject.layer = planetMasks[2];
		childBody.gameObject.layer = planetMasks[2];
		childBody.tag = "badBPlanet";
		mySporeTag = mySporeTags[2];
		mySporeLayer = mySporeLayers[2];
		myMat = badBMat;
		transform.GetChild(0).renderer.material = myMat;
	}
	
	void TurnBadC(){
		badC = true;
		transform.tag = "badCPlanet";
		gameObject.layer = planetMasks[3];
		childBody.gameObject.layer = planetMasks[3];
		childBody.tag = "badCPlanet";
		mySporeTag = mySporeTags[3];
		mySporeLayer = mySporeLayers[3];
		myMat = badCMat;
		transform.GetChild(0).renderer.material = myMat;
	}
	
	void TurnNeutral(){
		neutral = true;
		transform.tag = "neutralPlanet";
		gameObject.layer = planetMasks[4];
		childBody.gameObject.layer = planetMasks[4];
		childBody.tag = "neutralPlanet";
		myMat = neutralMat;
		transform.GetChild(0).renderer.material = myMat;
	}
	
	void DetermineAlignment(){
		myMat = transform.GetChild(0).renderer.material;
		//print (myMat.name + " " + goodMat.name + " (Instance)");
		
		if (myMat.name == ""+goodMat.name + " (Instance)"){
			TurnGood();
		}else if (myMat.name == ""+badAMat.name + " (Instance)"){
			TurnBadA();
		}else if (myMat.name == ""+badBMat.name + " (Instance)"){
			TurnBadB();
		}else if (myMat.name == ""+badCMat.name + " (Instance)"){
			TurnBadC();
		}else if (myMat.name == ""+neutralMat.name + " (Instance)"){
			TurnNeutral();
		}
	}
	
	public void Change (int newAlignment){
		print ("changing to " + newAlignment);
		if (newAlignment==0){ // turn good
			NullAll();
			good=true;
			TurnGood();
		}else if (newAlignment == 1){ //turn badA
			NullAll();
			badA=true;
			TurnBadA();
		}
		else if (newAlignment == 2){ //turn badB
			NullAll();
			badB=true;
			TurnBadB();
		}
		else if (newAlignment == 3){ // turn badC
			NullAll();
			badC=true;
			TurnBadC();
		}
		else if (newAlignment == 4){ // turn neutral
			NullAll();
			neutral=true;
			TurnNeutral();
		}
		GetComponent<newSpawnSpores>().ResumeSpawning(myMat, mySporeTag, mySporeLayer);
	}
	
	void NullAll(){
		good=false;
		badA=false;
		badB=false;
		badC=false;
		neutral=false;
	}
	
	void SendSpawnCommand(){
		if (!neutral){
			GetComponent<newSpawnSpores>().BeginSpawning(myMat, mySporeTag, mySporeLayer);
		}
	} */
}
