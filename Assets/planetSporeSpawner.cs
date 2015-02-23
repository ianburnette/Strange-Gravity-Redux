using UnityEngine;
using System.Collections;

public class planetSporeSpawner : MonoBehaviour {

	public GameObject spore;
	public int divisor = 10;
	private int planetSize;
	private planetInventory inventoryScript;
	public int stunned = 0;

	// Use this for initialization
	void Start () {
	//	planetSize = GetComponent<planetProperties>().size;
		inventoryScript = GetComponent<planetInventory>();
		InvokeRepeating("spawnSpore", divisor/planetSize, divisor/planetSize);
	//	InvokeRepeating(spawnSpore, 
	}
	
	// Update is called once per frame
	void Update () {
		if (stunned > 0){
			stunned--;
		}
	}
	
	void spawnSpore(){
		if (stunned<=0){
			GameObject newSpore = Instantiate (spore, transform.position, transform.rotation) as GameObject;
			inventoryScript.AddSpore(newSpore);
			newSpore.SendMessage("SetPlanet", gameObject.transform);//GetComponent<gravity>().planet = transform.gameObject;
		}
	}
}
