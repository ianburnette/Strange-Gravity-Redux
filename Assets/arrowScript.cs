using UnityEngine;
using System.Collections;

public class arrowScript : MonoBehaviour {

	bool begun = false;
	public Transform planet1, planet2;
	public Transform stem, point, actualStem;
	public float distanceDivisor, waitTime;
	Rect stemmy;

	void Start(){
		Initiate (planet1, planet2);
	}

	// Use this for initialization
	public void Initiate (Transform originPlanet, Transform destinationPlanet) {
		transform.position = (originPlanet.position + destinationPlanet.position)/2;
		stemmy =  actualStem.GetComponent<RectTransform>().rect;
		planet1 = originPlanet;
		planet2 = destinationPlanet;
		InvokeRepeating("Launch", 0, waitTime);
		begun = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (begun){
			stem.transform.position = (planet1.position + planet2.position)/2;
			transform.LookAt(planet2.position);
			stemmy.width = Vector3.Distance(planet1.position, planet2.position) / distanceDivisor;
			actualStem.GetComponent<RectTransform>().sizeDelta = new Vector2 (stemmy.width, stemmy.height);
		}
	}
	
	void Launch(){
		Transform newPoint = Instantiate(point, planet1.transform.position, Quaternion.identity) as Transform;
		newPoint.parent = stem.transform;
		newPoint.GetComponent<moveArrowPoint>().destination = planet2.transform;
	}
}
