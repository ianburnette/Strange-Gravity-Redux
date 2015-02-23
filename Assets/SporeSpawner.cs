using UnityEngine;
using System.Collections;

public class SporeSpawner : MonoBehaviour {

	public GameObject goodSpore;
	public GameObject badSpore;
	
	public int startingAlliegence;
	public int maxSpores;
	
	public int divisor = 10;
	public float planetRadius = 3f;
	public LayerMask mask;
	public int stunned = 0;
	
	public bool begun = false;
	public bool started = false;
	
	private int alliegence;
	private InventoryScript inventoryScript;
	
	
	// Use this for initialization
	void Start () {
		/* float sizer = GetComponent<planetProperties>().size;
		sizer *= 100; */
		//maxSpores = Mathf.RoundToInt(sizer);
		
		planetRadius = GetComponent<SphereCollider>().radius/2;
		//planetRadius = transform.localScale.x * 0;
		inventoryScript = GetComponent<InventoryScript>();
		
		if (startingAlliegence!=0){
			if (startingAlliegence==1){
				SpawnOne(goodSpore);
			}
			else if (startingAlliegence==-1){
				SpawnOne(badSpore);
			}
			else {
				int count = Mathf.Abs(startingAlliegence);
				for (int i = 0; i < count; i++){
					if (startingAlliegence>0){
						SpawnOne(goodSpore);
					}
					if (startingAlliegence<0){
						SpawnOne(badSpore);
					}
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (begun && !started){
			InvokeRepeating("SpawnSpore", divisor/planetRadius, divisor/planetRadius);
			started=true;
		}
		if (stunned > 0){
			stunned--;
		}
		alliegence = inventoryScript.alliegence;
	}
	
	void SpawnSpore(){
		if (stunned <= 0){
			if ((alliegence > 0 && alliegence<maxSpores) || 
				(alliegence < 0 && alliegence>-maxSpores)
				){
				if (alliegence>0){
					GameObject newSpore = Instantiate (goodSpore, PointOnSphere(), transform.rotation) as GameObject;
					//inventoryScript.NewGoodSpore(newSpore);
					newSpore.SendMessage("SetPlanet", gameObject.transform);
					newSpore.transform.parent = transform;
				}
				else if (alliegence<0){
					GameObject newSpore = Instantiate (badSpore, PointOnSphere(), transform.rotation) as GameObject;
					//inventoryScript.NewBadSpore(newSpore);
					newSpore.SendMessage("SetPlanet", gameObject.transform);
					newSpore.transform.parent = transform;
				}
			}
		}
	}
	
	Vector3 PointOnSphere(){
		Vector3 point = transform.position;
		point += Random.onUnitSphere * planetRadius/10;
//	print ("point is " + point);
		return point;
	}
	
	void SpawnOne(GameObject sporeToSpawn){
		GameObject newSpore = Instantiate (sporeToSpawn, PointOnSphere(), transform.rotation) as GameObject;
		newSpore.transform.parent = transform;
		//inventoryScript.NewGoodSpore(newSpore);
		newSpore.SendMessage("SetPlanet", gameObject.transform);
	}
}
