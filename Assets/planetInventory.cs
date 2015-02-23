using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class planetInventory : MonoBehaviour {

	public int spores = 0;
	public GameObject textObject;
	private Text numberText;
	List<GameObject> sporeGOs = new List<GameObject>();
	List<GameObject> sporesToRemove = new List<GameObject>();
	//public ArrayList sporeGOs = new ArrayList();//GameObject[] sporeGOs;
	
	// Use this for initialization
	void Start () {
		numberText = textObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		numberText.text = ""+sporeGOs.Count;
	}
	
	public void AddSpore (GameObject newSpore){
		sporeGOs.Add(newSpore);
	//	print ("new spore added to array list, which is now size " + sporeGOs.Count);
		newSpore.transform.parent = transform;
	}
	
	public void NewGoodSpore (GameObject newSpore){
		sporeGOs.Add(newSpore);
	}
	
	public void NewBadSpore (GameObject newSpore){
		sporeGOs.Add(newSpore);
	}
	
	void RemoveAlloted(){
		foreach (GameObject spore in sporesToRemove){
			RemoveSpore(spore);
//print ("removed " + spore.name);
		}
	}
	
	public void SendSpores (Transform newPlanet){
		/* foreach (GameObject spore in sporeGOs){
			print ("set to " + newPlanet.name);
			spore.SendMessage("SetPlanet", newPlanet);
			sporeGOs.RemoveAt(i);
		} */
//print ("trying to send from " + transform.tag + " to " + newPlanet.tag);
		if (newPlanet.tag == "goodPlanet" && transform.tag == "badPlanet"){
//print ("enemy attacking good");
			int i = 0;
			while (i < sporeGOs.Count){
				sporeGOs[i].SendMessage("SetPlanet", newPlanet);
//print ("enemy has " + newPlanet.GetComponent<planetInventory>().sporeGOs.Count + " and I have " + sporeGOs.Count);
				if (newPlanet.GetComponent<planetInventory>().sporeGOs[i] != null){
					sporeGOs[i].SendMessage("Attack", newPlanet.GetComponent<planetInventory>().sporeGOs[i]);
//print ("attacking a good guy");
//print ("sent number " + i);	
					sporesToRemove.Add(sporeGOs[i]);
				}
				else{
					sporeGOs[i].SendMessage("Attack", newPlanet);
				}
				sporeGOs.RemoveAt(i);
				i++;
			}
			//RemoveAlloted();
		}
		else if (newPlanet.tag == "badPlanet" && transform.tag == "goodPlanet"){
			print ("good attacking enemy");
			int i = 0;
			while (i < sporeGOs.Count){
				sporeGOs[i].SendMessage("SetPlanet", newPlanet);
//print ("enemy has " + newPlanet.GetComponent<planetInventory>().sporeGOs.Count + " and I have " + sporeGOs.Count);
				if (newPlanet.GetComponent<planetInventory>().sporeGOs[i] != null){
					sporeGOs[i].SendMessage("Attack", newPlanet.GetComponent<planetInventory>().sporeGOs[i]);
//	print ("attacking a bad guy");
					sporesToRemove.Add(sporeGOs[i]);
				}
				else{
					sporeGOs[i].SendMessage("Attack", newPlanet);
				}
				sporeGOs.RemoveAt(i);
				i++;
			}
			//RemoveAlloted();
		}
		else if (newPlanet.tag == "badPlanet" && transform.tag == "badPlanet"){
//print ("bad transferring");
			int i = 0;
			//print (sporeGOs.Count);
			while (i < sporeGOs.Count){
				sporeGOs[i].SendMessage("SetPlanet", newPlanet);
				//newPlanet.GetComponent<planetInventory>().NewSpore(sporeGOs[i]);
				sporeGOs.RemoveAt(i);
//print ("sent one");
				i++;
			} 
		}
		else if (newPlanet.tag == "goodPlanet" && transform.tag == "goodPlanet"){
//print ("good transferring");
			int i = 0;
			//print (sporeGOs.Count);
			while (i < sporeGOs.Count){
				sporeGOs[i].SendMessage("SetPlanet", newPlanet);
				//newPlanet.GetComponent<planetInventory>().NewSpore(sporeGOs[i]);
				sporeGOs.RemoveAt(i);
//print ("sent one");
				i++;
			} 
		}
		
		
		
		
		//for (int i = 0; i < sporeGOs.Count; i++){
			
		//}
	}
	
	public void RemoveSpore (GameObject toRemove){
		foreach (GameObject spore in sporeGOs){
			if (spore == toRemove){
				sporeGOs.Remove(spore);
			}	
		}
	}
}
