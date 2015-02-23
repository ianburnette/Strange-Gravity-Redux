using UnityEngine;
using System.Collections;

public class sunScript : MonoBehaviour {

	public int enemySpores;
	public float startScale;
	public float scaleDecrementAmt;
	public bool shrinking = false;
	public Material deadSun;
	public GameObject light, camera, gm;
	bool killedSpores = false;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find("_GM");
		camera = GameObject.Find("Player").transform.GetChild(0).gameObject;
		startScale = transform.localScale.x;
		scaleDecrementAmt = startScale/10;
		light = GameObject.Find("Directional light");
	}
	
	// Update is called once per frame
	void Update () {
		if (enemySpores>10 && !shrinking){
			InvokeRepeating ("Decrement", 0f, .5f);
			shrinking=true;
		}
		if (shrinking && transform.localScale.x<scaleDecrementAmt && !killedSpores){
			renderer.material = deadSun;
			rigidbody.useGravity=true;
			//GetComponent<SphereCollider>().isTrigger=false;
			light.active = false;
			gm.GetComponent<levelProgress>().RestartLevel();
			foreach (Transform child in transform){
				Destroy(child.gameObject);
			}
		}
	}
	
	void OnTriggerEnter (Collider col){
		if (col.transform.tag == "badSpore"){
			enemySpores++;
		}
	}
	
	void OnTriggerExit (Collider col){
		if (col.transform.tag == "badSpore"){
			enemySpores--;
		}
	}
	
	void Decrement(){
		transform.localScale -= new Vector3 (scaleDecrementAmt, scaleDecrementAmt, scaleDecrementAmt);
	}
	
	public IEnumerator SetShrink(){
		yield return new WaitForSeconds (3);
		InvokeRepeating ("Decrement", 0f, .5f);
		shrinking=true;
	}
}
