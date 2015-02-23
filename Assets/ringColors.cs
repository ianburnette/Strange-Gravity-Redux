using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ringColors : MonoBehaviour {

	public Image controlImage;
	Image myImage;
	
	// Use this for initialization
	void Start () {
		myImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		//myImage.color = controlImage.color;
	}
}
