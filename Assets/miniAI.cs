using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class miniAI: MonoBehaviour {

	public float decisionTime = 2;
	public int difficulty = 2;
	public GameObject[] myPlanets;
	public GameObject[] opposingPlanets;
	public GameObject[] neutralPlanets;
	Dictionary<GameObject,int> myPlanetSpores = new Dictionary <GameObject,int>();
	Dictionary<GameObject,int> opposingPlanetSpores = new Dictionary <GameObject,int>();
	
	public int reactionTime;
	public int maxReactionTime;
	
	public GameObject sun, gm;
	public bool sunPresent = false;
	
	public GameObject myStrongest;
	public float myStrongestPercentage;
	public int myStrongestIndex;
	
	//public List<GameObject> myPlanets = new List<GameObject>();
	
	/* var children = new List<GameObject>();
		foreach(Transform child in transform){
			children.Add(child.gameObject);
		} */
	
	void OnEnable () {
		if (sunPresent){
			sun = GameObject.Find("sun");
		}
		gm = GameObject.Find("_GM");
		InvokeRepeating("Think", decisionTime/4, decisionTime);
	}
	
	void Think(){
		print ("thinking");
		CountControlledPlanets();
		CountOpposingPlanets();
		CountNeutralPlanets();
		if (myPlanets.Length>0){
			ConsiderAttacking();
			ConsiderColonizingNeutral();
			ConsiderReinforcing();
		}
		if (reactionTime>0){
			reactionTime--;
		}
	}
	
	void CountControlledPlanets(){
		myPlanets = GameObject.FindGameObjectsWithTag("miniBadPlanet");
		for (int i = 0; i<myPlanets.Length; i++){
			int tempAlliegence = myPlanets[i].GetComponent<InventoryScript>().alliegence;
			myPlanetSpores[myPlanets[i]] = Mathf.Abs(tempAlliegence);
 print ("my spores at " + i + " is " + myPlanetSpores[myPlanets[i]]);
		}
		
		if (myPlanets.Length == 0){
			//I've lost
			FinishLevel();
		}
	}
	
	void CountNeutralPlanets(){
		neutralPlanets = GameObject.FindGameObjectsWithTag("miniNeutral");
	}
	
	void CountOpposingPlanets(){
		opposingPlanets = GameObject.FindGameObjectsWithTag("miniGoodPlanet");
		for (int i = 0; i<opposingPlanets.Length; i++){
			int tempAlliegence = opposingPlanets[i].GetComponent<InventoryScript>().alliegence;
			opposingPlanetSpores[opposingPlanets[i]] = Mathf.Abs(tempAlliegence);
print ("opposing spores at " + i + " is " + myPlanetSpores[myPlanets[i]]);
		}
		if (opposingPlanets.Length == 0){
			//I've won
			ExtinguishSun();
		}
	}
	
	void ConsiderAttacking(){
		GameObject weakest;// = new GameObject();
		GameObject strongest;// = new GameObject();
		int strongestIndex = 0;
		int weakestIndex = 0;
		float smallestPercentage = 1f;
		float largestPercentage = 0f;
		for (int i = 0; i<opposingPlanets.Length; i++){
			float percentage = opposingPlanetSpores[opposingPlanets[i]]/opposingPlanets[i].GetComponent<planetProperties>().maxSpores;
			if (percentage<smallestPercentage){
				smallestPercentage = percentage;
				weakest = opposingPlanets[i];
				weakestIndex = i;
			}
			if (percentage > largestPercentage){
				largestPercentage = percentage;
				strongest = opposingPlanets[i];
				strongestIndex = i;
			}
		}
		//print ("weakest is " + weakest + " with " + smallestPercentage);
		
		int rand = Random.Range(0,difficulty);
		if (rand==0){
			AttackWeakest(weakestIndex);
		}
	}
	
	void ConsiderColonizingNeutral(){
		if (neutralPlanets.Length!=0){
			int randNeutral = Random.Range(0,neutralPlanets.Length);
			print ("neutral selected is " + neutralPlanets[randNeutral]);
			ColonizeNeutral(randNeutral);
			int rand = Random.Range(0,difficulty);
			if (rand==0){
				ColonizeNeutral(randNeutral);
			}
		}
	}
	
	void ConsiderReinforcing(){
		if (myPlanets.Length>1){
			GameObject weakest;// = new GameObject();
			GameObject strongest;// = new GameObject();
			int strongestIndex = 0;
			int weakestIndex = 0;
			float smallestPercentage = 1f;
			float largestPercentage = 0f;
			for (int i = 0; i<myPlanets.Length; i++){
				float percentage = myPlanetSpores[myPlanets[i]]/myPlanets[i].GetComponent<planetProperties>().maxSpores;
				if (percentage<smallestPercentage){
					smallestPercentage = percentage;
					weakest = myPlanets[i];
					weakestIndex = i;
				}
				if (percentage > largestPercentage){
					largestPercentage = percentage;
					strongest = myPlanets[i];
					strongestIndex = i;
					SetStrongest(largestPercentage,strongest,strongestIndex);
				}
			}
			//ReinforceWeakest(strongestIndex, weakestIndex);
			if (smallestPercentage<0.25f && largestPercentage> 0.5f){
				//print ("considering reinforcing");
				int rand = Random.Range(0,difficulty);
				if (rand==0){
					ReinforceWeakest(strongestIndex, weakestIndex);
				}
			}	
		}
		else{
			SetStrongest(0,myPlanets[0],0);
		}
	}
	
	void SetStrongest(float percentage, GameObject strongest, int index){
		myStrongest = strongest;
		myStrongestIndex = index;
		myStrongestPercentage = percentage;
	}
	
	void AttackWeakest(int weakestIndex){
		CheckForStrongest();
		if (reactionTime<=0){
			myPlanets[myStrongestIndex].GetComponent<InventoryScript>().Transfer(opposingPlanets[weakestIndex].transform,2);
			reactionTime = maxReactionTime;
		}
	}
	
	void ReinforceWeakest(int strongestIndex, int weakestIndex){
		if (reactionTime<=0){
			myPlanets[strongestIndex].GetComponent<InventoryScript>().Transfer(myPlanets[weakestIndex].transform,2);
			reactionTime = maxReactionTime;
		}
	}
	
	void ColonizeNeutral(int neutralIndex){
		CheckForStrongest();
		int sporesOnStrongest = Mathf.Abs(myPlanets[myStrongestIndex].GetComponent<InventoryScript>().alliegence);
		if (sporesOnStrongest<2){
			return;
		}
		if (reactionTime<=0){
			if (neutralPlanets[neutralIndex]!=null){
			print ("trying to send");
				myPlanets[myStrongestIndex].GetComponent<InventoryScript>().Transfer(neutralPlanets[neutralIndex].transform,2);
				reactionTime = maxReactionTime;
			}
		}
	}
	
	void CheckForStrongest(){
		if (myPlanets.Length==1){
			myStrongestIndex=0;
		}	
	}

	void ExtinguishSun(){
		/* foreach (GameObject planet in myPlanets){
			//print ("extinguishing sun");
			planet.GetComponent<InventoryScript>().Transfer(sun.transform,1);
		} */
	}
	
	void FinishLevel(){
	//	gm.GetComponent<levelProgress>().Toggle(false);
		//gm.GetComponent<levelProgress>().FinishLevel();
	}
	
	}
