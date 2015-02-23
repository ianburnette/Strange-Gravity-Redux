using UnityEngine;
using System.Collections;

public class debugSporeSpawner : MonoBehaviour {

	public Transform sporePrefab;
	public Material[] sporeMaterials;
	public string[] sporeTags;
	public int[] sporeLayers;
	Transform planetBody;
	newSporeCount sporeCountScript;
	newSporeLists sporeLists;

	void Start () {
		sporeCountScript = transform.parent.GetComponent<newSporeCount>();
		sporeLists = transform.parent.GetComponent<newSporeLists>();
		planetBody = transform.parent.GetChild(0);
	}

	void Update () {
		if (Input.GetKey(KeyCode.Alpha1)){
			SpawnSpore(0);
		}else if (Input.GetKey(KeyCode.Alpha2)){
			SpawnSpore(1);
		}else if(Input.GetKey(KeyCode.Alpha3)){
			SpawnSpore(2);
		}else if(Input.GetKey(KeyCode.Alpha4)){
			SpawnSpore(3);
		}
		/* if (Input.GetKeyDown(KeyCode.Alpha1)){
			SpawnSpore(0);
		}else if (Input.GetKeyDown(KeyCode.Alpha2)){
			SpawnSpore(1);
		}else if(Input.GetKeyDown(KeyCode.Alpha3)){
			SpawnSpore(2);
		}else if(Input.GetKeyDown(KeyCode.Alpha4)){
			SpawnSpore(3);
		} */
	}
	
	void SpawnSpore(int alignmentToSpawn){
		Transform newSpore = (Transform) Instantiate(sporePrefab, new Vector3(transform.position.x+alignmentToSpawn, transform.position.y, transform.position.z), Quaternion.identity);
		newSpore.parent = planetBody;
		newSpore.renderer.material = sporeMaterials[alignmentToSpawn];
		newSpore.transform.tag = sporeTags[alignmentToSpawn];
		newSpore.gameObject.layer = sporeLayers[alignmentToSpawn];
		newSpore.transform.parent = planetBody;
		sporeCountScript.Add(alignmentToSpawn);
		sporeLists.Add(newSpore, alignmentToSpawn);
		newSpore.GetComponent<newSporeGravity>().gravityTarget = planetBody;
	}
}
