using UnityEngine;
using System.Collections;

public class autoAim : MonoBehaviour {

	public float slowDivisor;
	MouseLook xMouseScript, yMouseScript;
	float oldXsensitivity, oldYsensitivity;
	float range = 2000;
	public LayerMask mask;

	// Use this for initialization
	void Start () {
		yMouseScript = GetComponent<MouseLook>();
		xMouseScript = transform.parent.gameObject.GetComponent<MouseLook>();
		
		
		oldXsensitivity = xMouseScript.sensitivityX;
		oldYsensitivity = yMouseScript.sensitivityY;
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit highlightHit;
		if (Physics.Raycast (transform.position, transform.forward, out highlightHit, range, mask)){
			xMouseScript.sensitivityX = oldXsensitivity/slowDivisor;
			yMouseScript.sensitivityY = oldYsensitivity/slowDivisor;
		}
		else{
			if (xMouseScript.sensitivityX != oldXsensitivity){
				xMouseScript.sensitivityX = oldXsensitivity;
			}
			if (yMouseScript.sensitivityY != oldYsensitivity){
				yMouseScript.sensitivityY = oldYsensitivity;
			}
		}
	}
}
