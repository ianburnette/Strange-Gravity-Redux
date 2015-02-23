using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class newSporeLists : MonoBehaviour {
	public int maxListSize = 200;
	public float countUpdateInterval;
	
	public List<Transform> spores = new List<Transform>();
	
	private newSporeCount sporeCountScript;
	private newPlanetAlignment alignmentScript;
	private newPlanetInventory planetInventory;
	
	private int myCurrentAlignment;
	
	public void Start(){
		SetupScripts();
		InvokeRepeating("UpdateCount", 0, countUpdateInterval);
	}	
	
	void Update(){
		myCurrentAlignment = alignmentScript.myCurrentAlignment;
		if (Input.GetKeyDown(KeyCode.P)){
		//print ("Spores.count = " + spores.Count);
		}
	}
	
	public void Remove(Transform sporeToRemove){
		spores.Remove(sporeToRemove);
//print ("removing");
		if (spores.Count == 0){
			ChangeAlignment(4);
		}
	}
	
	public void Add(Transform sporeToAdd, int sporeTypeToAdd){
		int unengagedSpores = CheckUnengaged();
		if (sporeTypeToAdd == myCurrentAlignment){  
			spores.Add(sporeToAdd);
		}
		else if (unengagedSpores == 1){
			Transform sporeToDelete = FindSporeToAttack();
			AttackEachOther(sporeToAdd, sporeToDelete);
			ChangeAlignment(4);
		}
		else if (unengagedSpores != 0){
			Transform sporeToDelete = FindSporeToAttack();
			AttackEachOther(sporeToAdd, sporeToDelete);
		}
		else{
			ChangeAlignment(sporeTypeToAdd);
			spores.Add(sporeToAdd);
		}
	}
	
	int CheckUnengaged(){
		int numberUnengaged = 0;
		foreach (Transform spore in spores){
			if (spore.tag!="engagedSpore"){
				numberUnengaged ++;
			}
		}
		return numberUnengaged;
	}
	
	Transform FindSporeToAttack(){
		Transform sporeToAttack = null;
		foreach (Transform spore in spores){
			if (spore.tag != "engagedSpore"){
				sporeToAttack = spore;
				spore.tag = "engagedSpore";
//	print ("found a spore to attack");
				break;
			}
		}
		return sporeToAttack;
	}
	
	public void AttackEachOther(Transform attacker, Transform defender){
	/* 	defender.rigidbody.useGravity = true;
		attacker.rigidbody.useGravity = true; */
		attacker.tag = "engagedSpore";
		defender.GetComponent<newSporeGravity>().attackTarget = attacker;
		//defender.GetComponent<newSporeGravity>().attackSpeed = 0;
		attacker.GetComponent<newSporeGravity>().attackTarget = defender;
		defender.GetComponent<newSporeGravity>().attacking = true;
		attacker.GetComponent<newSporeGravity>().attacking = true;
		attacker.GetComponent<newSporeGravity>().Detach();// = true;
		defender.GetComponent<newSporeGravity>().Detach();// = true;
	}
	
	public void ChangeAlignment (int newAlignment){
//print ("changing alignment");
		if (myCurrentAlignment == 0){
			GameObject player = GameObject.FindGameObjectWithTag("player1");
			if (player!=null)
				player.BroadcastMessage("RemoveMe", transform);//Find<"Camera">().GetComponent<newSelectedPlanets>().RemoveMe(transform);
		}
		if (myCurrentAlignment == 1){
			GameObject player = GameObject.FindGameObjectWithTag("player2");
			if (player!=null)
				player.BroadcastMessage("RemoveMe", transform);//player.Find("Camera").GetComponent<newSelectedPlanets>().RemoveMe(transform);
		}
		alignmentScript.ChangeAlignment(newAlignment);
		myCurrentAlignment = alignmentScript.myCurrentAlignment;
	}
	
	public void SendHalf(Transform destination){
//print ("going to " + destination);
	/* 	if (spores.Count == 1){
			print ("send single");
		} */
		int numberToIterate = spores.Count-1;
		for (int i = 0; i <= numberToIterate; i+=2){
				Transform currentSpore = spores[0];
				if (currentSpore!=null){
					currentSpore.GetComponent<newSporeGravity>().Transfer(destination);
					
					destination.parent.GetComponent<newSporeLists>().Add(currentSpore, myCurrentAlignment);
				}
				spores.RemoveAt(0);
		}
		if (spores.Count == 0){
			ChangeAlignment(4);
		}
		
		/* int sporesToStart = Mathf.RoundToInt(spores.Count/2);
		for (int i = spores.Count-1; i >= sporesToStart; i--){
			if (spores[i] != null){
				Transform currentSpore = spores[i];
					spores.RemoveAt(i);
				currentSpore.GetComponent<newSporeGravity>().Transfer(destination);
				destination.parent.GetComponent<newSporeLists>().Add(currentSpore, myCurrentAlignment);
			} */
		
//print ("half send " + i + " of " + spores.Count);
		//	if (i%2==0 || i==2 || i == 0){
				
		//	}
		
		planetInventory.Stun();
		destination.parent.GetComponent<newPlanetInventory>().Stun();
	}	
	
	public void SendAll(Transform destination){
		if (destination != transform.GetChild(0)){
			foreach (Transform spore in spores){
				if (spore!=null){
					spore.GetComponent<newSporeGravity>().Transfer(destination);
					destination.parent.GetComponent<newSporeLists>().Add(spore, myCurrentAlignment);
				}
			}
			spores.Clear();
			ChangeAlignment(4);
			planetInventory.Stun();
			destination.parent.GetComponent<newPlanetInventory>().Stun();
		}
		
		/* for (int i = 0; i < spores.Count*2; i++){
			if (spores[i] != null){
				Transform currentSpore = spores[i];
					spores.RemoveAt(i);
				currentSpore.GetComponent<newSporeGravity>().Transfer(destination);
				destination.parent.GetComponent<newSporeLists>().Add(currentSpore, myCurrentAlignment);
			
				
			}
			print ("sending all at " + i);
		}
		ChangeAlignment(4); */
	}
	
	public void SetupScripts(){
		sporeCountScript = GetComponent<newSporeCount>();
		alignmentScript = GetComponent<newPlanetAlignment>();
		planetInventory = GetComponent<newPlanetInventory>();
	}
	
	void UpdateCount(){
		sporeCountScript.currentCount = spores.Count;
	}
	
//	public Transform[] 


}	
/* 	public int maxListSize;
	public float countUpdateInterval;
	
	public Transform[][] sporeLists = new Transform[4][]; //sporeLists[0][i] = sporeInstance;
	public int[] sporeCount = new int[4];
	
	private newSporeCount sporeCountScript;
	private newPlanetAlignment alignmentScript;
	
	private int myCurrentAlignment;
	private newPlanetInventory planetInventory;
	
	void Start(){
		SetupListSizes();
		SetupScripts();
		InvokeRepeating("UpdateCount", 0, countUpdateInterval);
	}
	
	void UpdateCount(){
		if (myCurrentAlignment!=4){
			sporeCountScript.currentCount = sporeCount[myCurrentAlignment];
		}
		else{
			sporeCountScript.currentCount = 0;
		}
	}
	
	public void Add(Transform sporeToAdd, int sporeTypeToAdd){
		if (sporeTypeToAdd == myCurrentAlignment){
			AddToList(sporeToAdd, myCurrentAlignment);
		}
		else if (myCurrentAlignment == 4){
			alignmentScript.ChangeAlignment(sporeTypeToAdd);
			myCurrentAlignment = sporeTypeToAdd;
			AddToList(sporeToAdd, myCurrentAlignment);
		}
		else{
			//AddToList(sporeToAdd, sporeTypeToAdd);
			Attack(sporeToAdd, sporeTypeToAdd);
		}
	}
	
	void AddToList(Transform newSpore, int listToIncrement){
//print ("adding " + newSpore + " to list " + listToIncrement + " of size " + sporeIndices[listToIncrement]);
		for (int i = 0; i<maxListSize; i++){
			if (sporeLists[listToIncrement][i] == null){
				sporeLists[listToIncrement][i] = newSpore;
				sporeCount[listToIncrement] ++;
				break;
			}
		}
		
		// if (newSpore!=null){
			// sporeLists[listToIncrement][sporeIndices[listToIncrement]] = newSpore;
			// sporeIndices[listToIncrement] ++;
		// }
	}
	
	void Attack(Transform attackingSpore, int attackingType){
		if (sporeCount[myCurrentAlignment] <= 0){
			SwitchAlignment(attackingType);
			Add(attackingSpore, myCurrentAlignment);
		}
		else{
			Transform defendingSpore = null;
			for (int i = 0; i<maxListSize; i++){
				if (sporeLists[myCurrentAlignment][i] != null){
					defendingSpore = sporeLists[myCurrentAlignment][i];
					sporeLists[myCurrentAlignment][i] = null;
					sporeCount[myCurrentAlignment] --;
				}
			}
			//RemoveMostRecentSpores(attackingType);
			MakeSporesAttackEachOther(defendingSpore, attackingSpore);	
		}
	}
	
	void SwitchAlignment(int alignmentToSwitchTo){
		alignmentScript.ChangeAlignment(alignmentToSwitchTo);
	}
	
	void MakeSporesAttackEachOther(Transform defendingSpore, Transform attackingSpore){
		Destroy (attackingSpore.gameObject);
		Destroy (defendingSpore.gameObject);
		// if (defendingSpore!=null && attackingSpore!=null){
			// defendingSpore.GetComponent<newSporeGravity>().attackTarget = attackingSpore;
			// defendingSpore.GetComponent<newSporeGravity>().attackSpeed = 0;
			// attackingSpore.GetComponent<newSporeGravity>().attackTarget = defendingSpore;
			// defendingSpore.GetComponent<newSporeGravity>().attacking = true;
			// attackingSpore.GetComponent<newSporeGravity>().attacking = true;
		// }
		CheckForEmpty();
	}
	
	void CheckForEmpty(){
		//print ("I have " + sporeIndices[myCurrentAlignment] + " spores");
		if (sporeCount[myCurrentAlignment] == 0){
			alignmentScript.ChangeAlignment(4);
		}
	}
	
	void RemoveMostRecentSpores (int attackingList){
	
		for (int i = 0; i<maxListSize; i++){
			if (sporeLists[myCurrentAlignment][i] != null){
				sporeLists[myCurrentAlignment][i] = null;
				break;
			}
		}
	
		for (int i = 0; i<maxListSize; i++){
			if (sporeLists[attackingList][i] != null){
				sporeLists[attackingList][i] = null;
				break;
			}
		}
	
		// sporeLists[myCurrentAlignment][sporeIndices[myCurrentAlignment]] = null;
		// sporeIndices[myCurrentAlignment] --;
		// sporeLists[attackingList][sporeIndices[attackingList]] = null;
		// sporeIndices[attackingList] --;
	}
	
	public void SendHalf(Transform destination){
		//int numberBeforeDecrementing = sporeIndices[myCurrentAlignment];
		for (int i = 0; i < maxListSize; i++){
			if (i%2==0){
				if (sporeLists[myCurrentAlignment][i] == null){
				//	print ("skipping");
				}
				else{
//print ("i is " + i + " and the remainder is " + i%2);
					Transform currentSpore = sporeLists[myCurrentAlignment][i];
					
					currentSpore.GetComponent<newSporeGravity>().Transfer(destination);
					print ("transferring");
					destination.parent.GetComponent<newSporeLists>().Add(currentSpore, myCurrentAlignment);
					
					sporeLists[myCurrentAlignment][i] = null;
					sporeCount[myCurrentAlignment] --;
				}
			}
		}
		planetInventory.Stun();
		destination.parent.GetComponent<newPlanetInventory>().Stun();
		//transform.parent.GetComponent<newHighlightScript>().StopHighlighting();
		
		
		// for (int i = mySpores.Length; i > mySpores.Length/2; i--){
			// mySpores[i].GetComponent<newSporeGravity>().Transfer(destination);
			// destination.parent.GetComponent<newPlanetInventory>().SporeAttack(mySpores[i]);
			// sporeCount--;
			// yield return new WaitForSeconds(sporeInterval);
			
		// }
		// for (int i = 0; i < mySpores.Count/2; i++){
			// mySpores.RemoveAt(i);
		// }
	}
	
	public void SendAll(Transform destination){
		//int numberBeforeDecrementing = sporeIndices[myCurrentAlignment];
		planetInventory.Stun();
		destination.parent.GetComponent<newPlanetInventory>().Stun();
		if (destination != transform.GetChild(0)){
			for (int i = 0; i<maxListSize; i++){
				Transform currentSpore = null;
				if (sporeLists[myCurrentAlignment][i]!=null){
					currentSpore = sporeLists[myCurrentAlignment][i];
					currentSpore.GetComponent<newSporeGravity>().Transfer(destination);
					destination.parent.GetComponent<newSporeLists>().Add(currentSpore, myCurrentAlignment);
					sporeLists[myCurrentAlignment][i] = null;
				}
				sporeCount[myCurrentAlignment] = 0;
				alignmentScript.ChangeAlignment(4);	
			}
		}
	}
	
	void Update(){
		myCurrentAlignment = alignmentScript.myCurrentAlignment;
	}

	void SetupListSizes(){
		for (int i = 0; i<sporeLists.Length; i++){
			sporeLists[i] = new Transform[maxListSize];
		}
	}
	
	void SetupScripts(){
		sporeCountScript = GetComponent<newSporeCount>();
		alignmentScript = GetComponent<newPlanetAlignment>();
		planetInventory = GetComponent<newPlanetInventory>();
	} */


/* public class newSporeLists : MonoBehaviour {

	newSporeCount sporeCountScript;
	newPlanetAlignment alignmentScript;
	public int maxListSize;
	public Transform[] goodSpores = new Transform[0];
	public Transform[] badASpores = new Transform[0];
	public Transform[] badBSpores = new Transform[0];
	public Transform[] badCSpores = new Transform[0];
	public int goodSporesIndex, badASporesIndex, badBSporesIndex, badCSporesIndex = 0;
	// public List<Transform> goodSpores = new List<Transform>();
	// public List<Transform> badASpores = new List<Transform>();
	// public List<Transform> badBSpores = new List<Transform>();
	// public List<Transform> badCSpores = new List<Transform>();
	
	
	void Start(){  
		goodSpores = new Transform[maxListSize];
		badASpores = new Transform[maxListSize];
		badBSpores = new Transform[maxListSize];
		badCSpores = new Transform[maxListSize];
		sporeCountScript = GetComponent<newSporeCount>();
		alignmentScript = GetComponent<newPlanetAlignment>();
	}	
	
	public void Add(Transform sporeToAdd, int sporeTypeToAdd){ //THIS SHOULD BE ALL SET, BUT KEEP AN EYE ON THE SPORE COUNT 
		if (sporeTypeToAdd == 0){ //good attacking
//goodSpores.Add(sporeToAdd);
			//if (goodSpores[goodSporesIndex] == null){
				
		//	}
			goodSpores[goodSporesIndex] = sporeToAdd;
				goodSporesIndex++;
			if (alignmentScript.myCurrentAlignment!=sporeTypeToAdd){ 
				if (alignmentScript.myCurrentAlignment == 1){ //badA
					if (badASporesIndex == 0){alignmentScript.ChangeAlignment(sporeTypeToAdd);}
					else if (badASporesIndex == 1){Attack(0, 1, sporeToAdd);
													alignmentScript.ChangeAlignment(4);}
					else{Attack(0, 1, sporeToAdd);}
				}
				else if (alignmentScript.myCurrentAlignment == 2){ //badB
					if (badBSporesIndex == 0){alignmentScript.ChangeAlignment(sporeTypeToAdd);}
					else if (badBSporesIndex == 1){Attack(0, 2, sporeToAdd);
													alignmentScript.ChangeAlignment(4);}
					else{Attack(0, 2, sporeToAdd);}
				}
				else if (alignmentScript.myCurrentAlignment == 3){ //badC
					if (badCSporesIndex == 0){alignmentScript.ChangeAlignment(sporeTypeToAdd);}
					else if (badCSporesIndex == 1){Attack(0, 3, sporeToAdd);
													alignmentScript.ChangeAlignment(4);}
					else{Attack(0, 3, sporeToAdd);}
				}
				else if (alignmentScript.myCurrentAlignment == 4) {// neutral
					alignmentScript.ChangeAlignment(sporeTypeToAdd);
				}
			}
		} else if (sporeTypeToAdd == 1){ //badA attacking
			//if (badASpores[badASporesIndex] == null){
			
			//}
			badASpores[badASporesIndex] = sporeToAdd;
				badASporesIndex++;
			
			//badASpores.Add(sporeToAdd);
			if (alignmentScript.myCurrentAlignment!=sporeTypeToAdd){
				if (alignmentScript.myCurrentAlignment == 0){ //good
					if (goodSporesIndex < 0){alignmentScript.ChangeAlignment(sporeTypeToAdd);}
					else if (goodSporesIndex == 0){Attack(1, 0, sporeToAdd);
													alignmentScript.ChangeAlignment(4);
													print ("reached one");
													}
					else{Attack(1, 0, sporeToAdd);}
				}
				else if (alignmentScript.myCurrentAlignment == 2){ //badB
					if (badBSporesIndex == 0){alignmentScript.ChangeAlignment(sporeTypeToAdd);}
					else if (badBSporesIndex == 1){Attack(1, 2, sporeToAdd);
													alignmentScript.ChangeAlignment(4);}
					else{Attack(1, 2, sporeToAdd);}
				}
				else if (alignmentScript.myCurrentAlignment == 3){ //badC
					if (badCSporesIndex == 0){alignmentScript.ChangeAlignment(sporeTypeToAdd);}
					else if (badCSporesIndex == 1){Attack(1, 3, sporeToAdd);
													alignmentScript.ChangeAlignment(4);}
					else{Attack(1, 3, sporeToAdd);}
				}
				else if (alignmentScript.myCurrentAlignment == 4) {// neutral
					alignmentScript.ChangeAlignment(sporeTypeToAdd);
				}
			}
		} if (sporeTypeToAdd == 2){ //badB attacking
			//if (badBSpores[badBSporesIndex] == null){
			
		//	}
			badBSpores[badBSporesIndex] = sporeToAdd;
				badBSporesIndex++;
			if (alignmentScript.myCurrentAlignment!=sporeTypeToAdd){ 
				if (alignmentScript.myCurrentAlignment == 0){ //good
					if (goodSporesIndex == 0){alignmentScript.ChangeAlignment(sporeTypeToAdd);}
					else if (goodSporesIndex == 1){Attack(2, 0, sporeToAdd);
													alignmentScript.ChangeAlignment(4);}
					else{Attack(2, 0, sporeToAdd);}
				}
				else if (alignmentScript.myCurrentAlignment == 1){ //badA
					if (badASporesIndex == 0){alignmentScript.ChangeAlignment(sporeTypeToAdd);}
					else if (badASporesIndex == 1){Attack(2, 1, sporeToAdd);
													alignmentScript.ChangeAlignment(4);}
					else{Attack(2, 1, sporeToAdd);}
				}
				else if (alignmentScript.myCurrentAlignment == 3){ //badC
					if (badCSporesIndex == 0){alignmentScript.ChangeAlignment(sporeTypeToAdd);}
					else if (badCSporesIndex == 1){Attack(2, 3, sporeToAdd);
													alignmentScript.ChangeAlignment(4);}
					else{Attack(2, 3, sporeToAdd);}
				}
				else if (alignmentScript.myCurrentAlignment == 4) {// neutral
					alignmentScript.ChangeAlignment(sporeTypeToAdd);
				}
			}
		} if (sporeTypeToAdd == 3){ //badC attacking
			//if (badCSpores[badCSporesIndex] == null){
			
			//}
			badCSpores[badCSporesIndex] = sporeToAdd;
				badCSporesIndex++;
			if (alignmentScript.myCurrentAlignment!=sporeTypeToAdd){ 
				if (alignmentScript.myCurrentAlignment == 0){ //good
					if (goodSporesIndex == 0){alignmentScript.ChangeAlignment(sporeTypeToAdd);}
					else if (goodSporesIndex == 1){Attack(3, 0, sporeToAdd);
													alignmentScript.ChangeAlignment(4);}
					else{Attack(3, 0, sporeToAdd);}
				}
				else if (alignmentScript.myCurrentAlignment == 1){ //badA
					if (badASporesIndex == 0){alignmentScript.ChangeAlignment(sporeTypeToAdd);}
					else if (badASporesIndex == 1){Attack(3, 1, sporeToAdd);
													alignmentScript.ChangeAlignment(4);}
					else{Attack(3, 1, sporeToAdd);}
				}
				else if (alignmentScript.myCurrentAlignment == 2){ //badB
					if (badBSporesIndex == 0){alignmentScript.ChangeAlignment(sporeTypeToAdd);}
					else if (badBSporesIndex == 1){Attack(3, 2, sporeToAdd);
													alignmentScript.ChangeAlignment(4);}
					else{Attack(3, 2, sporeToAdd);}
				}
				else if (alignmentScript.myCurrentAlignment == 4) {// neutral
					alignmentScript.ChangeAlignment(sporeTypeToAdd);
				}
			}
		} 
		
	}
	
	void Attack(int attackingList, int defendingList, Transform attackingSpore){ 	//change this to deal with the built-in arrays and the int passed variables, DECREMENTING SPORE COUNT
		Transform defendingSpore = null;
			
		if (defendingList == 0){ //good is defending
			defendingSpore = goodSpores[goodSporesIndex];
			goodSpores[goodSporesIndex] = null;
			goodSporesIndex --;
		}else if (defendingList == 1){ //badA is defending
			defendingSpore = badASpores[badASporesIndex];
			badASpores[badASporesIndex] = null;
			badASporesIndex --;
		}else if (defendingList == 2){ //badB is defending
			defendingSpore = badBSpores[badBSporesIndex];
			badBSpores[badBSporesIndex] = null;
			badBSporesIndex--;
		}else if (defendingList == 3){ //badC is defending
			defendingSpore = badCSpores[badCSporesIndex];
			badCSpores[badCSporesIndex] = null;
			badCSporesIndex--;
		}
		
		if (attackingList == 0){ //good is attacking
			goodSpores[goodSporesIndex] = null;
			goodSporesIndex --;
		}else if (attackingList == 1){ //badA is attacking
			badASpores[badASporesIndex] = null;
			badASporesIndex --;
		}else if (attackingList == 2){ //badB is attacking
			badBSpores[badBSporesIndex] = null;	
			badBSporesIndex--;
		}else if (attackingList == 3){ //badC is attacking
			badCSpores[badCSporesIndex] = null;
			badCSporesIndex--;
		}
		
		defendingSpore.GetComponent<newSporeGravity>().attackTarget = attackingSpore;
		defendingSpore.GetComponent<newSporeGravity>().attackSpeed = 0;
		attackingSpore.GetComponent<newSporeGravity>().attackTarget = defendingSpore;
		defendingSpore.GetComponent<newSporeGravity>().attacking = true;
		attackingSpore.GetComponent<newSporeGravity>().attacking = true;
		
		sporeCountScript.Subtract(defendingList);
		sporeCountScript.Subtract(attackingList); */
		
		/* for (int i=0; i<defendingList.Count; i++){
			if (defendingList[i] != null){
				defendingSpore = defendingList[i];
				break;
			}
		}
		for (int i = 0; i<attackingList.Count; i++){
			if (attackingList[i] != null){
				attackingSpore = attackingList[i];
				break;
			}
		}	
		attackingList.RemoveAt(0);
		defendingList.RemoveAt(0);
		defendingSpore.GetComponent<newSporeGravity>().attackTarget = attackingSpore;
		defendingSpore.GetComponent<newSporeGravity>().attackSpeed = 0;
		attackingSpore.GetComponent<newSporeGravity>().attackTarget = defendingSpore;
		defendingSpore.GetComponent<newSporeGravity>().attacking = true;
		attackingSpore.GetComponent<newSporeGravity>().attacking = true; */
		/* Destroy (defendingSpore.gameObject);
		Destroy (attackingSpore.gameObject); */
	/* 	int defendingInt = 0;
		int attackingInt = 0;
		if (attackingList == goodSpores)
			attackingInt = 0;
		else if (attackingList == badASpores)
			attackingInt = 1;
		else if (attackingList == badBSpores)
			attackingInt = 2;
		else if (attackingList == badCSpores)
			attackingInt = 3;
		if (defendingList == goodSpores)
			defendingInt = 0;
		else if (defendingList == badASpores)
			defendingInt = 1;
		else if (defendingList == badBSpores)
			defendingInt = 2;
		else if (defendingList == badCSpores)
			defendingInt = 3; */
	
	// }
		
/* 	void Attack2(int attackingList, int defendingList){
		Transform defendingSpore;
		Transform attackingSpore;
		if (attackingList == 1){
			if (defendingList ==0){
				print (goodSpores.Count);
				defendingSpore = goodSpores[0];
				attackingSpore = badASpores[0];
				goodSpores.RemoveAt(0);
				badASpores.RemoveAt(0);
				Destroy (defendingSpore.gameObject);
				Destroy (attackingSpore.gameObject);
			}
		}
	} */

