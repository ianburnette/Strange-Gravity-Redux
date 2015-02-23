using UnityEngine;
using System.Collections;

public class ringSpin : MonoBehaviour {

	public float spinSpeed = 5f;
	public bool randomizeRotation = false;
	public bool randomizeSpinSpeed = false;
	public bool horizontalOnly = false;
	
	// Use this for initialization
	void Start () {
		if (!horizontalOnly){
			if (randomizeRotation){
				transform.rotation = Random.rotation;
			}
			if (randomizeSpinSpeed){
				spinSpeed = Random.Range(1f,30f);
				if (spinSpeed > -1f && spinSpeed < 1f){
					spinSpeed += 2f;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
	}
}
