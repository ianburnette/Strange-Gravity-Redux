using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour {

	public int nextLevelInt;

	// Use this for initialization
	void OnLevelWasLoaded () {
		GameObject.Find("_gm").GetComponent<scoreManagement>().nextLevel = nextLevelInt;
		if (Application.loadedLevel == 1){Destroy(gameObject);}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
