using UnityEngine;
using System.Collections;

public class RotateAndOrbit : MonoBehaviour {

	public float margin = 1f;
	public float heightCorrection;
	public float sizeMult;
    public float RotationSpeed = 100f;
		public float orbitSpeed = 0;
    public float baseOrbitSpeed = 120;
    public float DesiredMoonDistance = 4;
    public Transform target;
	int randX, randY, randZ;
	public bool reachedTop;
	public float waitToSet, heightBuild;

	void OnEnable(){
		target = transform.parent;
		if (transform.parent.parent.localScale.x < 15){
			DesiredMoonDistance = (transform.parent.parent.localScale.x * Random.Range (sizeMult, sizeMult*2));
		}
		else{
			DesiredMoonDistance = (transform.parent.parent.localScale.x * Random.Range (sizeMult, sizeMult*1));
		}
		orbitSpeed = Random.Range (baseOrbitSpeed/transform.parent.parent.localScale.x, baseOrbitSpeed/transform.parent.parent.localScale.x*3);
		//Invoke("SetAtTop", waitToSet);
	}
	
    void Start () {
		
		orbitSpeed = Random.Range(orbitSpeed/4, orbitSpeed);
		target = transform.parent;
       // DesiredMoonDistance = Vector3.Distance(target.position, transform.position);
		randX = Random.Range(-1,1); 
		randY = Random.Range(-1,1); 
		randZ = Random.Range(-1,1); 
		if (randX == 0 && randY == 0 && randZ == 0){
			randX = 1;
		}
		
    }

	void SetAtTop(){
		reachedTop = true;
	}
	
    void Update () {
		if (target == null && transform.parent != null){
			target = transform.parent;
		}
		
      transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
	
		if (target!=null){
			transform.RotateAround(target.position, new Vector3(randX,randY,randZ), orbitSpeed * Time.deltaTime);
		//fix possible changes in distance
			float currentMoonDistance = Vector3.Distance(target.position, transform.position);
			Vector3 towardsTarget = transform.position - target.position;
			if (currentMoonDistance < DesiredMoonDistance && !reachedTop){
				transform.position += (DesiredMoonDistance - currentMoonDistance) * towardsTarget.normalized * heightCorrection; 	
				heightCorrection+=heightBuild;
			}
			else{
				if (reachedTop==false){
					reachedTop = true;
				}
			}
			if (reachedTop){
				transform.position += (DesiredMoonDistance - currentMoonDistance) * towardsTarget.normalized; 
			}
			 
		}
    }
}