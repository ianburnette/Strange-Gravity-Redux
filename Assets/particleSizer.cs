using UnityEngine;
using System.Collections;

public class particleSizer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<ParticleSystem>().startSize = transform.parent.parent.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<ParticleSystem>().startSize != transform.parent.parent.localScale.x){
			GetComponent<ParticleSystem>().startSize = transform.parent.parent.localScale.x;
		}
	}
}
