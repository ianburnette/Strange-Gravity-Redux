using UnityEngine;
using System.Collections;

public class highlighter : MonoBehaviour {

	public bool hightlighted;
	public int highlightCounter;
	public Shader highlightShader;
	private Shader originalShader;
	public Color highlightColor;
	public Color selectedColor;
	//public Halo halo;
	public float haloDecrement = 0.1f;
	public float haloIntensity;// = 0.1f;

	// Use this for initialization
	void Start () {
		originalShader = renderer.material.shader;
		//GetComponent<"Halo">().size = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//if (hak
		if (highlightCounter>0){highlightCounter--;}
		if (highlightCounter>0 && renderer.material.shader != highlightShader ){
			renderer.material.shader = highlightShader;
			highlightCounter--;
		}
		else if (highlightCounter<=0 && renderer.material.shader != originalShader){	
			renderer.material.shader = originalShader;
		}
	}
	
	public void SetLit(int type){
//print ("setlit called with " + type);
		if (type == 1){
			int currentAlliegence = GetComponent<InventoryScript>().alliegence;
			Color colorToTurn = new Color();
			if (currentAlliegence>0){
				colorToTurn = Color.green;
			}
			else if (currentAlliegence<0){
				colorToTurn = Color.red;
			}
			else if (currentAlliegence==0){
				colorToTurn = Color.white;
			}
			renderer.material.SetColor("_OutlineColor", colorToTurn);
			highlightCounter = 5;
		}
		else if (type == 2){
			renderer.material.SetColor("_OutlineColor", Color.blue);
			highlightCounter = 100;
		}
		else if (type == 3){
			renderer.material.SetColor("_OutlineColor", Color.yellow);
			//renderer.material.SetFloat("_Outline", .1f);
			highlightCounter = 100;
		}			
	}
	
	public void DeactivateHighlight(){
//print ("deactivated");
		highlightCounter = 0;
		renderer.material.shader = originalShader;
	}
	
	public void HaloSet(){
		haloIntensity = 12;
	}
}
