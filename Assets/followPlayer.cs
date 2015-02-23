using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class followPlayer : MonoBehaviour {

	public GameObject player;
	private Text numberOfSpores;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		numberOfSpores = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(player.transform);
	}
}
