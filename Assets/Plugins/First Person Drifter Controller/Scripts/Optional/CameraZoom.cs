// by @torahhorse

using UnityEngine;
using System.Collections;

// allows player to zoom in the FOV when holding a button down
[RequireComponent (typeof (Camera))]
public class CameraZoom : MonoBehaviour
{

	public int playerNum;
	public bool onMac;
	public float zoomFOV = 30.0f;
	public float zoomSpeed = 9f;
	
	private float targetFOV;
	private float baseFOV;
	
	void Start ()
	{
		SetBaseFOV(camera.fieldOfView);
	}
	
	void Update ()
	{//print (Input.GetAxis("ZoomP1alt"));
		if (playerNum == 0 && !onMac){
			if( Input.GetButton("ZoomP1") || Input.GetAxis("ZoomP1alt") > .1f ){targetFOV = zoomFOV;}
			else{targetFOV = baseFOV;}
		}else if (playerNum == 0 && onMac){
			if( Input.GetButton("ZoomP1MAC")  || Input.GetAxis("ZoomP1MACalt") > .1f || Input.GetAxis("ZoomP1MACalt") < -.1f ){targetFOV = zoomFOV;}
			else{targetFOV = baseFOV;}
		}else if (playerNum == 1 && !onMac){
			if( Input.GetButton("ZoomP2") || Input.GetAxis("ZoomP2alt") > .1f ) {targetFOV = zoomFOV;}
			else{targetFOV = baseFOV;}
		}else if (playerNum == 1 && onMac){
			if( Input.GetButton("ZoomP2MAC") || Input.GetAxis("ZoomP2MACalt") > .1f ||  Input.GetAxis("ZoomP2MACalt") < -.1f   ){targetFOV = zoomFOV;}
			else{targetFOV = baseFOV;}
		}
		UpdateZoom();
	}
	
	private void UpdateZoom()
	{
		camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
	}
	
	public void SetBaseFOV(float fov)
	{
		baseFOV = fov;
	}
}
