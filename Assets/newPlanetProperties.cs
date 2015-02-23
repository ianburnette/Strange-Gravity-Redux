using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class newPlanetProperties : MonoBehaviour {

	public bool overrideCurrentScale;
	public float size;
	Transform planetBody;
	public int sporeCount, totalSpores;
	//public Text sporeCountText, totalSporesText;

	// Use this for initialization
	void Start () {
		planetBody = transform.GetChild(0);
		if (overrideCurrentScale){
			transform.localScale = new Vector3(size, size, size);
		}else{
			size = transform.localScale.x;
		}
		SetupSporeCount();		
		GetComponent<newSporeCount>().SetupTotal(totalSpores);
	}
	
	void SetupSporeCount(){
		if (totalSpores == 0){
			totalSpores = Mathf.RoundToInt(size * 10);
		//	totalSporesText.text = "of "+totalSpores;
		}
		else{
		//	totalSporesText.text = "of "+totalSpores;
		}
	}
	

	
	void Update () {
		//UpdateSporeCount();
	}
}
