using UnityEngine;
using System.Collections;

public class spinFast : MonoBehaviour {

	spin spinScript;
	public float fastSpeed = 500f;
	public float waitTime = .1f;
	public GameObject trail;
	public GameObject trails;
	
	float initialSpeed;

	// Use this for initialization
	void Start () {
		foreach (Transform planet in transform){
			GameObject newTrail = (GameObject)Instantiate(trail, planet.GetChild(0).transform.position, Quaternion.identity);
			newTrail.transform.parent = trails.transform;
		}
		spinScript = trails.GetComponent<spin>();
		initialSpeed = spinScript.spinSpeed;
		spinScript.spinSpeed = fastSpeed;
		Invoke("ReduceSpeed", waitTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void ReduceSpeed(){
		spinScript.spinSpeed = initialSpeed;
	}
}
