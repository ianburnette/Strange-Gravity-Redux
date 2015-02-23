using UnityEngine;
using System.Collections;

public class selectionPlanetHighlight : MonoBehaviour {

	public float growSpeed;
	public Vector3 largeScale, smallScale;
	bool grow;

	public void Grow(){
			if (grow == true){
				grow = false;
			}else{
				grow = true;
			}
	}
	
	public void Shrink(){
		grow = false;
	}

	// Use this for initialization
	void Start () {
		smallScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x < largeScale.x & grow){
			transform.localScale = Vector3.Lerp (transform.localScale, largeScale, growSpeed);
		}
		else if (transform.localScale.x > smallScale.x & !grow){
			transform.localScale = Vector3.Lerp (transform.localScale, smallScale, growSpeed);
		}
	}
}
