using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class newPlanetInventory : MonoBehaviour {

	public float blinkTime, stunLength;
	public Color stunColor, normalColor, goneColor;
	public Text sporeCountTextP1, sporeTotalTextP1;
	public Text sporeCountTextP2, sporeTotalTextP2;
	public bool stunned;
	public float stunCounter;
	newSpawnSpores spawnScript;

	void Start(){
		spawnScript = GetComponent<newSpawnSpores>();
	}
	
	void Update(){
		if (stunCounter > 0){
			stunCounter--;
		}
		else if (stunned){
			UnStun();
		}
	}
	
	public void Stun(){
		if (!stunned){
			stunned = true;
			InvokeRepeating("Blink", 0, blinkTime);
			stunCounter = stunLength;
			spawnScript.stunned=true;
		}
	}
	
	public void UnStun(){
		stunned = false;
		CancelInvoke("Blink");
		sporeCountTextP1.color = normalColor;
		sporeTotalTextP1.color = normalColor;
		sporeCountTextP2.color = normalColor;
		sporeTotalTextP2.color = normalColor;
		spawnScript.stunned=false;
	}
	
	public void Blink(){
		if (sporeCountTextP1.color == goneColor){
			sporeCountTextP1.color = stunColor;
			sporeTotalTextP1.color = stunColor;
			sporeCountTextP2.color = stunColor;
			sporeTotalTextP2.color = stunColor;
		}
		else{
			sporeCountTextP1.color = goneColor;
			sporeTotalTextP1.color = goneColor;
			sporeCountTextP2.color = goneColor;
			sporeCountTextP2.color = goneColor;
		}
		if (!stunned){
			sporeCountTextP1.color = normalColor;
			sporeTotalTextP1.color = normalColor;
			sporeCountTextP2.color = normalColor;
			sporeCountTextP2.color = normalColor;
		}
	}
	
/* 	public Transform arrow;
	public float sporeInterval, blinkTime;
	public Color stunColor, normalColor, goneColor;
	public int sporeCount, totalSpores;
	public Text sporeCountText, sporeTotalText;
	public List<Transform> mySpores = new List<Transform>();
	public List<Transform> attackingSpores = new List<Transform>();
	public bool stunned;
	newSpawnSpores sporeScript;
	newPlanetAlignment alignmentScript;
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("UpdateSporeCount", 0, 1f);
		sporeScript = GetComponent<newSpawnSpores>();
		alignmentScript = GetComponent<newPlanetAlignment>();
	}
	
	void Update(){
		if (stunned && sporeCount<=0){
			
		}
	}
	
	void UpdateSporeCount(){
		sporeCountText.text = "" + sporeCount;
	}
	
	public void Stun(){
		stunned = true;
		InvokeRepeating("Blink", 0, blinkTime);
	}
	
	public void UnStun(){
		stunned = false;
		CancelInvoke("Blink");
		sporeCountText.color = normalColor;
		sporeTotalText.color = normalColor;
	}
	
	public void Blink(){
		if (sporeCountText.color == goneColor){
			sporeCountText.color = stunColor;
			sporeTotalText.color = stunColor;
		}
		else{
			sporeCountText.color = goneColor;
			sporeTotalText.color = goneColor;
		}
		if (!stunned){
			sporeCountText.color = normalColor;
			sporeTotalText.color = normalColor;
		}
	}
	
	public void AddSpore (Transform sporeToAdd) {
		mySpores.Add(sporeToAdd);
		//sporeToAdd.GetComponent<sporeIndex>().myIndex = mySpores.Count;
	}
	
	public void SporeAttack(Transform attackingSpore){
		if (mySpores.Count>0){
			attackingSpore.GetComponent<newSporeGravity>().attackTarget = mySpores[0];
			mySpores.RemoveAt(0);
			sporeCount--;
			if (sporeCount == 0){
				alignmentScript.Change(5);
				
			}
		}else{
		
			ChangeAlignment(attackingSpore);
		}
	}
	
	void ChangeAlignment(Transform attackingSpore){
		mySpores.Add(attackingSpore);
		if (attackingSpore.tag == "goodSpore"){
			
			if (alignmentScript.good == true){
				print ("spore tag matches current alignment!");
			}else {
				print ("changing from inventory");
				alignmentScript.Change(0);
			}
		}else if (attackingSpore.tag == "badASpore"){
			if (alignmentScript.badA == true){
				print ("spore tag matches current alignment!");
			}else {
				alignmentScript.Change(1);
			}
		}else if (attackingSpore.tag == "badBSpore"){
			if (alignmentScript.badB == true){
				print ("spore tag matches current alignment!");
			}else {
				alignmentScript.Change(2);
			}
		}else if (attackingSpore.tag == "badCSpore"){
			if (alignmentScript.badC == true){
				print ("spore tag matches current alignment!");
			}else {
				alignmentScript.Change(3);
			}
		}
	}
	
	public void SendHalf (Transform destination){
		StartCoroutine("SendHalfCo", destination);
		sporeScript.StartCoroutine("Stun");
		destination.parent.GetComponent<newSpawnSpores>().StartCoroutine("Stun");
	}
	
	IEnumerator SendHalfCo (Transform destination){
		for (int i = 0; i < mySpores.Count/2; i++){
			mySpores[i].GetComponent<newSporeGravity>().Transfer(destination);
			destination.parent.GetComponent<newPlanetInventory>().SporeAttack(mySpores[i]);
			sporeCount--;
			yield return new WaitForSeconds(sporeInterval);
			
		}
		for (int i = 0; i < mySpores.Count/2; i++){
			mySpores.RemoveAt(i);
		}
	}
	
	public void SendAll (Transform destination){
		StartCoroutine("SendAllCo", destination);
		sporeScript.StartCoroutine("Stun");
		destination.parent.GetComponent<newSpawnSpores>().StartCoroutine("Stun");
	}
	
	IEnumerator SendAllCo (Transform destination){
		// Transform arrowInstance = Instantiate (arrow, transform.position, Quaternion.identity) as Transform;
		// arrowInstance.parent = transform;
		// arrowInstance.GetComponent<arrowScript>().Initiate(transform, destination);
		float adjustedCount = mySpores.Count;
		// if (adjustedCount % 2 == 1){
			// print ("count adjusted from " + adjustedCount + " to " + adjustedCount+1);
			// adjustedCount += 1;
		// }
		for (int i = 0; i < adjustedCount; i++){
			mySpores[i].GetComponent<newSporeGravity>().Transfer(destination);
			destination.parent.GetComponent<newPlanetInventory>().SporeAttack(mySpores[i]);
			sporeCount--;
			yield return new WaitForSeconds(sporeInterval);
		}
		mySpores.Clear();
		//Destroy(arrowInstance.gameObject);
	}
	
	public void SporeDie (Transform deadSpore){
		// int deadID = deadSpore.GetInstanceID();
		// print ("die " + mySpores.Count);
		// for (int i = 0; i < mySpores.Count; i++){
			print ("" + mySpores[i] + " and " + deadSpore);
			// print (""+mySpores[i].GetInstanceID() + " and " + deadID);
			// if (mySpores[i].GetInstanceID() == deadID){
				// print ("spore dying");
				// mySpores[i].parent = null;
				// mySpores.RemoveAt(i);
				// print("removed?");
				// sporeCount--;
				// break;
			// }
		// } 
	
	
	 //	int i = mySpores.FindIndex(Transform spore => s == deadSpore);
		//print ("id = " + deadSpore.GetInstanceID()); 
		
		//int thisSporeIndex = deadSpore.GetComponent<sporeIndex>().myIndex;
		//mySpores.RemoveAt(i);
		
		
		// if (mySpores.Contains(deadSpore)){
			// print ("contains");
		// }
		
		
	}
	 */
	
}
