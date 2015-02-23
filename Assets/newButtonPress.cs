using UnityEngine;
using System.Collections;

public class newButtonPress : MonoBehaviour {

	public Transform gm;
	public int alignment = 0;
	public Shader highlightShader;
	public Shader originalShader;
	int highlightTimer = 0;
	public Transform button;
	bool sink = false;
	public float sinkSpeed = 1f;

	// Use this for initialization
	void Start () {
		originalShader = button.renderer.material.shader;
		GetComponent<buttonUI>().SetAlignment(alignment);
	}
	
	// Update is called once per frame
	void Update () {
		if (highlightTimer>0){
			highlightTimer--;
		}
		else{
			if (button.renderer.material.shader != originalShader){
				button.renderer.material.shader = originalShader;
			}
		}
		if (sink){
			transform.Translate(Vector3.down * sinkSpeed * Time.deltaTime);
		}
	}
	
	public void Press(){
		if (alignment == 0){
			gm.GetComponent<actionStartMultiplayer>().button1Pressed = true;
		}else if (alignment == 1){
			gm.GetComponent<actionStartMultiplayer>().button2Pressed = true;
		}
	}
	
	public void Highlight(){
		button.renderer.material.shader = highlightShader;
		highlightTimer = 20;
	}
	
	public void UnHighlight(){
		button.renderer.material.shader = originalShader;
	}
	
	public void Sink(){
		sink = true;
	}
}
