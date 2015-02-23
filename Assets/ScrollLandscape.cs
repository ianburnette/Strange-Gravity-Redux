using UnityEngine;
using System.Collections;

public class ScrollLandscape : MonoBehaviour {

	public float scrollSpeed;
	float xOffset;
	SurfaceCreator creatorScript;

	// Use this for initialization
	void Start () {
		creatorScript = GetComponent<SurfaceCreator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		xOffset += scrollSpeed;
		creatorScript.offset.x = xOffset * Time.deltaTime;
		creatorScript.Refresh();
	}
}
