using UnityEngine;
using System.Collections;

public class newSpawnSpores : MonoBehaviour {

	public bool startRightAway;
	public int sporesToSpawnToStart;
	public Material[] sporeMaterials;
	public Transform sporePrefab;
	public float secondsBetweenSpores, surfaceModifier;
	public string[] sporeTags;
	public int[] sporeLayers;
	Transform planetBody;
	newPlanetAlignment alignmentScript;
	public int myCurrentAlignment;
	string myCurrentSporeTag;
	int myCurrentSporeLayer;
	newSporeCount sporeCountScript;
	public newSporeLists sporeLists;
	public bool stunned, full;
	
	void Start(){
		planetBody = transform.GetChild(0);
		sporeCountScript = GetComponent<newSporeCount>();
		sporeLists = GetComponent<newSporeLists>();
		alignmentScript = GetComponent<newPlanetAlignment>();
		myCurrentAlignment = alignmentScript.myCurrentAlignment;
		SpawnFirstSpores();
		if (startRightAway){
			BeginSpawning();
		}
	}
	
	public void BeginSpawning(){
		InvokeRepeating("CreateSpore", secondsBetweenSpores, secondsBetweenSpores);
	}
	
	void SpawnFirstSpores(){
		for (int i = 0; i<sporesToSpawnToStart; i++){
			Invoke("CreateSpore", secondsBetweenSpores/2);
		}
	}
	
	void CreateSpore(){
		if (myCurrentAlignment != 4 && !stunned && !full){
			Transform newSpore = (Transform) Instantiate(sporePrefab, transform.position + Random.onUnitSphere * transform.localScale.x * surfaceModifier, Quaternion.identity);
			newSpore.parent = planetBody;
			newSpore.renderer.material = sporeMaterials[myCurrentAlignment];
			newSpore.transform.tag = sporeTags[myCurrentAlignment];
			newSpore.gameObject.layer = sporeLayers[myCurrentAlignment];
			newSpore.transform.parent = planetBody;
			sporeCountScript.Add(myCurrentAlignment);
			sporeLists.Add(newSpore, myCurrentAlignment);
			newSpore.GetComponent<newSporeGravity>().gravityTarget = planetBody;
			newSpore.GetComponent<newSporeGravity>().alignment = myCurrentAlignment;
		/* 	//print ("newspore tag is " + newSpore.transform.tag);
			//RotateAndOrbit rotateScript = newSpore.GetComponent<RotateAndOrbit>();
			//rotateScript.target = planetBody;
			//rotateScript.DesiredMoonDistance = transform.localScale.x * 5;
			//StartCoroutine(BeginOrbit(rotateScript));
			//rotateScript.enabled = true;
			//newSpore.GetComponent<RotateAndOrbit>().target = planetBody;
			//inventoryScript.sporeCount ++;
			//inventoryScript.AddSpore(newSpore.transform); */
		}
	}
	
	void Update(){
		myCurrentAlignment = alignmentScript.myCurrentAlignment;
	}
	

/* 
	//public Transform sporePrefab;

	public Transform planetBody;
	public GameObject spore;
	public float waitTime, spawnTime, stunTime;
	public bool stunned;
	newPlanetInventory inventoryScript;
	Material matForSpores;
	string tagForSpores;
	public int maskForSpores;

	// Use this for initialization
	void Start () {
		inventoryScript = GetComponent<newPlanetInventory>();
		planetBody = transform.GetChild(0);
		//InvokeRepeating("SpawnSpore", 1, 1);
	}
	
	 public void BeginSpawning(Material mat,  string tagName, int mask){
		matForSpores = mat;
		tagForSpores = tagName;
		maskForSpores = mask;	
		InvokeRepeating("SpawnSpore", 0, spawnTime);
	}  
	
	public void ResumeSpawning (Material mat,  string tagName, int mask){
		CancelInvoke("SpawnSpore");
		matForSpores = mat;
		tagForSpores = tagName;
		maskForSpores = mask;	
		InvokeRepeating("SpawnSpore", 0, spawnTime);
	}  
	
	public IEnumerator Stun(){
		stunned = true;
		inventoryScript.Stun();
		yield return new WaitForSeconds(stunTime);
		
		stunned = false;
		inventoryScript.UnStun();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void SpawnSpore(){
		if (inventoryScript.sporeCount < inventoryScript.totalSpores && !stunned){
			//print (""+Random.onUnitSphere);
			GameObject newSpore = Instantiate(spore, transform.position + Random.onUnitSphere * 5, Quaternion.identity) as GameObject;
			//print (newSpore.transform.position);
			newSpore.renderer.material = matForSpores;
			newSpore.transform.tag = tagForSpores;
			print ("newspore tag is " + newSpore.transform.tag);
			newSpore.layer = maskForSpores;
			newSpore.transform.parent = planetBody;
			RotateAndOrbit rotateScript = newSpore.GetComponent<RotateAndOrbit>();
			rotateScript.target = planetBody;
			rotateScript.DesiredMoonDistance = transform.localScale.x * 5;
			StartCoroutine(BeginOrbit(rotateScript));
			//rotateScript.enabled = true;
			//newSpore.GetComponent<RotateAndOrbit>().target = planetBody;
			inventoryScript.sporeCount ++;
			inventoryScript.AddSpore(newSpore.transform);
		}
	}
	
	IEnumerator BeginOrbit(RotateAndOrbit currentRotateScript){
		yield return new WaitForSeconds(waitTime);
		currentRotateScript.enabled = true;
	} */
}
