using UnityEngine;
using System.Collections;

public class jumpAnimation : MonoBehaviour {

	public LayerMask groundDetection;
	public float length;
	public Animator anim;
	public Transform model;

	// Use this for initialization
	void Start () {
		anim = model.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit, length , groundDetection)){
			anim.SetBool("jumping", false);
		}
		else{
			anim.SetBool("jumping", true);
		}
	}
}
