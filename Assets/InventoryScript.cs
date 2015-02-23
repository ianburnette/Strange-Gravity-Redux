using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryScript : MonoBehaviour {

	public Material goodPlanet;
	public Material badPlanet;
	public Material neutralPlanet;
	
	public Color goodColor;
	public Color badColor;
	
	public GameObject numberObject;
	public GameObject subNumberObject;
	public Text numberText;
	private Text subNumberText;
	public string numberString;
	public int numOfGoodChildren;
	public int numOfBadChildren;
	public int alliegence;
	public int maxSpores;
	
	private int everChanged = 60;
	
	public bool beginEarly = false;
	public bool begunEarly = false;
	
	public bool mini = false;

	// Use this for initialization
	void Start () {
		SetupCounter();
		UpdateAlliegence(GetComponent<SporeSpawner>().startingAlliegence);
		//InvokeRepeating("CountChildren", 1, 1);
		
		//maxSpores = GetComponent<SporeSpawner>().maxSpores;
	}
	
	public void BeginCounting(){
		InvokeRepeating("CountChildren", 0, 1);
		GetComponent<SporeSpawner>().begun = true;
	}
	
	public void SetupCounter(){
		if (numberObject!=null){
			subNumberObject = numberObject.transform.GetChild(0).gameObject;
			numberText = numberObject.GetComponent<Text>();
			subNumberText = subNumberObject.GetComponent<Text>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (beginEarly && !begunEarly){
			BeginCounting();
			begunEarly = true;
		}
		//print ("alliegence is " + alliegence + " and numberText is " + numberText.text);
		if (everChanged>0){
			alliegence = GetComponent<SporeSpawner>().startingAlliegence;
			UpdateAlliegence(alliegence);
		//	print ("ever is " + everChanged);
		}	
	}
	
	void CountChildren(){
		int currentGood = 0;
		int currentBad = 0;
		foreach (Transform child in transform){
			if (child.tag == "goodSpore"){
				currentGood++;
			}
			else if (child.tag == "badSpore"){
				currentBad++;
			}
		}
		DetermineAlliegence(currentGood, currentBad);
		numOfBadChildren = currentBad;
		numOfGoodChildren = currentGood;
	}
	
	void DetermineAlliegence(int good, int bad){
		int currentAlliegence;
		currentAlliegence = good - bad;
//print ("I have " + good + " good and " + bad + " bad, so my alliegence is " + currentAlliegence);
		UpdateAlliegence(currentAlliegence);
	}
	
	void UpdateAlliegence(int newAlliegence){
		alliegence = newAlliegence;
		if (!mini){
			if (alliegence>0 && renderer.material!=goodPlanet){
				renderer.material = goodPlanet;
				transform.tag = "goodPlanet";
				gameObject.layer = LayerMask.NameToLayer("goodPlanet");
			}
			if (alliegence < 0 && renderer.material!=badPlanet){
				renderer.material = badPlanet;
				transform.tag = "badPlanet";
				gameObject.layer = LayerMask.NameToLayer("badPlanet");
			}
			if (alliegence == 0 && renderer.material!=neutralPlanet){
				renderer.material = neutralPlanet;
				transform.tag = "planet";
				gameObject.layer = LayerMask.NameToLayer("Planet");
				//stop spore spawning
			}
		}
		else{
			//print ("setting mini alliegence is " + alliegence);
			if (alliegence>0 && renderer.material!=goodPlanet){
				renderer.material = goodPlanet;
				transform.tag = "miniGoodPlanet";
				gameObject.layer = LayerMask.NameToLayer("goodPlanet");
			}
			if (alliegence < 0 && renderer.material!=badPlanet){
				renderer.material = badPlanet;
				transform.tag = "miniBadPlanet";
				gameObject.layer = LayerMask.NameToLayer("badPlanet");
			}
			if (alliegence == 0 && renderer.material!=neutralPlanet){
				renderer.material = neutralPlanet;
				transform.tag = "miniNeutral";
				gameObject.layer = LayerMask.NameToLayer("Planet");
				//stop spore spawning
			}
		}
		UpdateCounter();
	}
	
	void UpdateCounter(){
//		print("updating counter" + alliegence);
	/* 	if (alliegence<0 && numberText.color != badColor){
			//numberText.color = badColor;
		}
		if (alliegence>0 && numberText.color != goodColor){
			//numberText.color = goodColor;
		}
		if (alliegence==0 && numberText.color != Color.white){
			//numberText.color = Color.white;
		} */
		
		
		if (numberText!=null){
			if(alliegence >= 0){
				numberText.text = ""+(alliegence);
				subNumberText.text = "of " + maxSpores;
			}
			else if (alliegence < 0){
				numberText.text = ""+(alliegence);
				subNumberText.text = "of " + -maxSpores;
			}
			else{
				numberText.text = ""+(alliegence);
				subNumberText.text = "of " + maxSpores;
			}
		}
		if ((alliegence>0 && alliegence>=maxSpores) ||
			(alliegence<0 && alliegence<=maxSpores)){
				
		}
		
		if (everChanged>0 ){
			//print ("numtext is " + numberText.text);
			everChanged--;
		}
		
		if (numOfBadChildren != 0 && numOfGoodChildren!=0){
			Battle(1);
		}
		//Battle();
	}
	
	void Battle(int typeOfBattle){
//print ("battling with " + typeOfBattle);
		//if (numOfBadChildren != 0 && numOfGoodChildren!=0){ //have some of both
		var goodChildren = new List<GameObject>();
		var badChildren = new List<GameObject>();
		foreach (Transform child in transform){
			if (child.tag == "badSpore"){
				badChildren.Add(child.gameObject);
			}
			if (child.tag == "goodSpore"){
				goodChildren.Add(child.gameObject);
			}
		}
		if (typeOfBattle==1){ //bad attacking good 
//print ("bad attacking good");
			int smallerAmt = new int();
			if (goodChildren.Count>badChildren.Count){ //good will defend
				smallerAmt=badChildren.Count;
			}
			else if (goodChildren.Count<badChildren.Count){ //good will not defend
				smallerAmt=goodChildren.Count;
			}
			else{ //mutually assured destruction
				smallerAmt = goodChildren.Count;
			}
			for (int i = 0; i<smallerAmt; i++){
				badChildren[i].GetComponent<attack>().Attack(goodChildren[i]);
			}
		}
		else if (typeOfBattle==2){ //good attacking bad
//print ("good attacking bad");
			int smallerAmt = new int();
			if (goodChildren.Count>badChildren.Count){ //bad will defend
				smallerAmt=badChildren.Count;
			}
			else if (goodChildren.Count<badChildren.Count){ //bad will not defend
				smallerAmt=goodChildren.Count;
			}
			else{ //mutually assured destruction
				smallerAmt = goodChildren.Count;
			}
			for (int i = 0; i<smallerAmt; i++){
				goodChildren[i].GetComponent<attack>().Attack(badChildren[i]);
				//print ("attacking " + i);
			}
		}
		//}
	}
	
	public void Transfer(Transform target, int divisor){
		print ("transferring to " + target);
		GetComponent<SporeSpawner>().stunned = 500;
		if (target.tag!="sun"){target.GetComponent<SporeSpawner>().stunned = 200;}
		var children = new List<GameObject>();
		foreach(Transform child in transform){
			if (child.tag!="ring"){
				children.Add(child.gameObject);
			}
			//child.GetComponent<attack>().target = null;
		}
		if (target.transform.tag == "goodPlanet" && transform.tag == "badPlanet"){
			//children.ForEach(child=> child.transform.GetComponent<gravityScript>().SetPlanet(target));
			//print ("I have " + children.Count + " children");
			for (int i = 0; i<children.Count/divisor; i++){
			//	if (children[i].tag != "Untagged"){
					children[i].transform.GetComponent<gravityScript>().SetPlanet(target);
					target.GetComponent<InventoryScript>().Battle(1);
			//	}
			}
		}
		else if (target.transform.tag == "badPlanet" && transform.tag == "goodPlanet"){
			for (int i = 0; i<children.Count/divisor; i++){
			//	if (children[i].tag != "Untagged"){
					children[i].transform.GetComponent<gravityScript>().SetPlanet(target);
					target.GetComponent<InventoryScript>().Battle(2);
			//	}
			}
		}
		else if (target.transform.tag == "badPlanet" && transform.tag == "badPlanet"){
			for (int i = 0; i<children.Count/divisor; i++){
				//if (children[i].tag != "Untagged"){
					children[i].transform.GetComponent<gravityScript>().SetPlanet(target);
				//}
			}
		}
		else if (target.transform.tag == "goodPlanet" && transform.tag == "goodPlanet"){
			for (int i = 0; i<children.Count/divisor; i++){
				//if (children[i].tag != "ring"){
					children[i].transform.GetComponent<gravityScript>().SetPlanet(target);
				//}
			}
		}
		else if (target.transform.tag == "planet"){
			for (int i = 0; i<children.Count/divisor; i++){
				//if (children[i].transform.tag != "ring"){
					children[i].transform.GetComponent<gravityScript>().SetPlanet(target);
				//}
			}
		}
		else if (target.transform.tag == "sun"){
			for (int i = 0; i<children.Count/divisor; i++){
				//if (children[i].tag != "Untagged"){
					children[i].transform.GetComponent<gravityScript>().SetPlanet(target);
				//}
			}
		}
		else if (target.transform.tag == "miniGoodPlanet" || target.transform.tag == "miniBadPlanet" || target.transform.tag == "miniNeutral"){
			for (int i = 0; i<children.Count/divisor; i++){
				//if (children[i].tag != "Untagged"){
					children[i].transform.GetComponent<gravityScript>().SetPlanet(target);
				//}
			}
		}
		//children.ForEach(child=>child.transform.GetComponent<die>().Die());
	}
	
	
	
	/* public GameObject GetTarget(int typeOfEnemy){
		if (typeOfEnemy == 1){ //bad spore attacking
		
		}
		else { //good spore attacking
		
		}
	} */
}
