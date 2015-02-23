using UnityEngine;
using System.Collections;

public class pointAtPlanet : MonoBehaviour {

	public Transform planet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (planet != null){
			transform.LookAt(planet);
		}
	}
}
