using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class newSporeCount : MonoBehaviour {

	public int goodSporeCount, badASporeCount, badBSporeCount, badCSporeCount;
	newPlanetAlignment alignmentScript;
	newPlanetStun stunScript;
	newSpawnSpores spawnScript;
	public Text countTextP1, totalTextP1, countTextP2, totalTextP2;
	public int currentCount, totalCount;
	
	void Start (){
		alignmentScript = GetComponent<newPlanetAlignment>();
		stunScript = GetComponent<newPlanetStun>();
		spawnScript = GetComponent<newSpawnSpores>();
	}
	
	public void SetupTotal(int myTotal){
		totalCount = myTotal;
		totalTextP1.text = "of "+totalCount;
		totalTextP2.text = "of "+totalCount;
	}
	
	void Update(){
		if (alignmentScript.myCurrentAlignment == 0){
			countTextP1.text = ""+currentCount;
			countTextP2.text = ""+currentCount;
		//	currentCount = goodSporeCount;
		} else if (alignmentScript.myCurrentAlignment == 1){
			countTextP1.text = ""+currentCount;
			countTextP2.text = ""+currentCount;
			//currentCount = badASporeCount;
		} else if (alignmentScript.myCurrentAlignment == 2){
			countTextP1.text = ""+currentCount;
			countTextP2.text = ""+currentCount;
			//currentCount = badBSporeCount;
		} else if (alignmentScript.myCurrentAlignment == 3){
			countTextP1.text = ""+currentCount;
			countTextP2.text = ""+currentCount;
			//currentCount = badCSporeCount;
		} else if (alignmentScript.myCurrentAlignment == 4){
			countTextP1.text = "0";
			countTextP2.text = "0";
		}
		CheckForFullness();
	}
	
	void CheckForFullness(){
		if (currentCount >= totalCount){
			spawnScript.full = true;
		}
		else{
			spawnScript.full = false;
		}
	}

	public void Add(int countToIncrement) {
		if (countToIncrement == 0){
			goodSporeCount++;
		} else if (countToIncrement == 1){
			badASporeCount++;
		} else if (countToIncrement == 2){
			badBSporeCount++;
		} else if (countToIncrement == 3){
			badCSporeCount++;
		}
	}
	
	public void Subtract(int countToDecrement){
		if (countToDecrement == 0){
			goodSporeCount--;
		} else if (countToDecrement == 1){
			badASporeCount--;
		} if (countToDecrement == 2){
			badBSporeCount--;
		} if (countToDecrement == 3){
			badCSporeCount--;
		}
		stunScript.Stun();
	}
}
