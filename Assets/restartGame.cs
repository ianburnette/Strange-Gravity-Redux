﻿using UnityEngine;
using System.Collections;

public class restartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp("Fire1")){
			Application.LoadLevel(0);
		}
	}
}
