using UnityEngine;
using System.Collections;

public class newShrinkSpore : MonoBehaviour {

	public Rigidbody opponent;
	public bool victim;
	public Transform explosion;
	public float scale, growTime, scaleSpeed;
	public int particleNumber;

	// Use this for initialization
	void Start () {
	
		//growTime = Random.Range (growTime,growTime*4);
		scale = 0;
		//scaleSpeed = Random.Range (scaleSpeed, ); 
	
	//	transform.localScale = Vector3.zero;
	}
	
	void OnEnable(){
		GetComponent<newGrowSpore>().enabled = false;
		transform.parent.parent.GetComponent<newSporeLists>().Remove(transform);
//print ("removed myself");
//Destroy(gameObject);		
		
		/* if (transform.parent != null){
			transform.parent.parent.GetComponent<newSporeLists>().Remove(transform);
			transform.parent = null;
		} */
	}
	
	// Update is called once per frame
	void Update () {
			
			if (transform.localScale.x > scale){
				transform.localScale -= new Vector3(1,1,1) * (growTime/scaleSpeed) * Time.deltaTime;
			}
			else if (scale == 0){
				if (victim){
					Instantiate(explosion, transform.position, Quaternion.identity);
				}
				if (opponent!=null){
					Destroy(opponent.gameObject);
				}
				Destroy(gameObject);
				/* transform.localScale -= new Vector3(1,1,1) * (growTime/scaleSpeed) * Time.deltaTime/2;
				if (transform.localScale.x <= 0){
				Destroy(gameObject); */
			}
		}	
	}