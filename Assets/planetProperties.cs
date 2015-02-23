using UnityEngine;
using System.Collections;

public class planetProperties : MonoBehaviour {

	public float size;
	public float maxSpores;
	public GameObject textCounter;
	public GameObject canvas;
	public float divisor;
	public GameObject myText;

	// Use this for initialization
	void Start () {
		
		size = transform.localScale.x;
		
		float sizer = size;
		sizer = sizer * 100f;
//print ("sizer is " + sizer);
		if (maxSpores == 0){
			GetComponent<SporeSpawner>().maxSpores = Mathf.RoundToInt(sizer);
			GetComponent<InventoryScript>().maxSpores = Mathf.RoundToInt(sizer);
			maxSpores = sizer;
		}
		else{
			//maxSpores = sizer;
			GetComponent<SporeSpawner>().maxSpores = Mathf.RoundToInt(maxSpores);
			GetComponent<InventoryScript>().maxSpores = Mathf.RoundToInt(maxSpores);
			
		}
		
		canvas = GameObject.Find("CounterCanvas");
		GameObject text = Instantiate (textCounter, transform.position + Vector3.up*sizer/2.7f, Quaternion.identity) as GameObject;
		myText = text;
		text.transform.parent = canvas.transform;
		GetComponent<InventoryScript>().numberObject = text;
		GetComponent<InventoryScript>().SetupCounter();
	}
	
	// Update is called once per frame
	void Update () {
		myText.transform.localScale = new Vector3(transform.localScale.x*-divisor,transform.localScale.y*divisor, transform.localScale.z*divisor);
		//myText.transform.localScale.x = -myText.transform.localScale.x;
	}
}
