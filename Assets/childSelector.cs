using UnityEngine;
using System.Collections;

public class childSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int randChild = Random.Range(0,4);
		for (int i = 0; i<3; i++){
			if (i!=randChild){
				transform.GetChild(i).active=false;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
