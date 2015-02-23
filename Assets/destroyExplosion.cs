using UnityEngine;
using System.Collections;

public class destroyExplosion : MonoBehaviour {

	public float timeToDie;

	// Use this for initialization
	void Start () {
		Invoke("DestroySelf", timeToDie);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void DestroySelf(){
		Destroy(gameObject);
	}
}
