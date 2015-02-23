using UnityEngine;
using System.Collections;

public class buttonDetector : MonoBehaviour {

	public RaycastHit hit;
	public float range = 2f;
	public LayerMask mask1, mask2;
	LayerMask myMask;
	public int alignment;
	bool alignmentSet = false;

	// Use this for initialization
	void Start () {
		
	}
	
	public void SetAlignment(int newAlignment){
		alignment = newAlignment;
		if (alignment == 0){
			myMask = mask1;
		}else{
			myMask = mask2;
		}
		alignmentSet = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (alignmentSet){
			if (Physics.Raycast(transform.position, transform.forward, out hit, range,	myMask)){
				if (hit.transform.tag == "button"){
//print (hit.transform.parent.name);
					hit.transform.GetComponent<newButtonPress>().Highlight();
					if (alignment == 0 && Input.GetButtonDown("Fire1P1")){
						hit.transform.parent.GetComponent<newButtonPress>().Press();
					}
					if (alignment == 1 && Input.GetButtonDown("Fire1P2")){
						hit.transform.parent.GetComponent<newButtonPress>().Press();
					}
				}
			}
		}
	}
}
