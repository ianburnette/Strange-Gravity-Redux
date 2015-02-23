using UnityEngine;
using System.Collections;

public class sizeModifier : MonoBehaviour {

	//public GameObject moveMod;
	//public Vector3 posMod;
	public GameObject gm;
	public float mySize;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find("_GM");
		mySize = transform.parent.localScale.x/4;
		GetComponent<TrailRenderer>().startWidth = mySize;
		int randChild = Random.Range(0,4);
		//moveMod = gm.transform.GetChild(randChild).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		//posMod = moveMod.transform.position;
		//Vector3 temp = new Vector3 (posMod.x-1, posMod.y-1, posMod.z-1);
	//	temp = temp*mySize;
		
		//transform.position += temp;
	}
}
