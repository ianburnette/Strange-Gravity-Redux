using UnityEngine;
using System.Collections;

public class trailSizer : MonoBehaviour {

	public float trailSize;
	TrailRenderer trail;

	// Use this for initialization
	void Start () {	
		trail = GetComponent<TrailRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
			trail.startWidth = transform.localScale.x * trailSize * transform.parent.parent.localScale.x;
	}
}
