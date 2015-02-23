using UnityEngine;
using System.Collections;

public class FacePlayer : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		if (transform.name == "goToOther"){
			if (transform.parent.name == "p2Canvas"){
				player = GameObject.FindGameObjectWithTag("player1");
			}else{
				player = GameObject.FindGameObjectWithTag("player2");
			}
		}else{
			if (transform.parent.name == "p2Canvas"){
				player = GameObject.FindGameObjectWithTag("player2");
			}
			else if (transform.parent.name == "p1Canvas"){
				player = GameObject.FindGameObjectWithTag("player1");
			}
			else{
				if (transform.parent.parent.name == "p2Canvas"){
					player = GameObject.FindGameObjectWithTag("player2");
				}
				else if (transform.parent.parent.name == "p1Canvas"){
					player = GameObject.FindGameObjectWithTag("player1");
				}
				else{
					print ("can't find the right player");
					player = GameObject.Find("Player");
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(player.transform);
	}
}
