using UnityEngine;
using System.Collections;

public class guiScaler : MonoBehaviour {

	public float minWidth;
	public float aspect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		print (Screen.width + " by " + Screen.height);

		aspect = Screen.height / Screen.width;

		if (Screen.width < minWidth) {
			transform.localScale = new Vector3 (Screen.width / minWidth, 1, 1);
		} else {
			transform.localScale = new Vector3(1,1,1);
		}
	}
}
