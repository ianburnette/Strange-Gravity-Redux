// original by asteins
// adapted by @torahhorse
// http://wiki.unity3d.com/index.php/SmoothMouseLook

// Instructions:
// There should be one MouseLook script on the Player itself, and another on the camera
// player's MouseLook should use MouseX, camera's MouseLook should use MouseY

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseLook : MonoBehaviour
{
	public bool onMac = false;
	public float adjustedHeadFloat;
	public int playerNumber = 0;
 
	public enum RotationAxes { MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseX;
	public bool animateHeadY = false;
	public bool invertY = false;
	
	public float sensitivityX = 10F;
	public float sensitivityY = 9F;
 
	public float minimumX = -360F;
	public float maximumX = 360F;
 
	public float minimumY = -85F;
	public float maximumY = 85F;
 
	float rotationX = 0F;
	float rotationY = 0F;
 
	private List<float> rotArrayX = new List<float>();
	float rotAverageX = 0F;	
 
	private List<float> rotArrayY = new List<float>();
	float rotAverageY = 0F;
 
	public float framesOfSmoothing = 5;
	
	public Animator anim;
	public Transform model;
 
	Quaternion originalRotation;
	
	void Start ()
	{	
		if (animateHeadY == true){
			anim = model.GetComponent<Animator>();
		}
		if (rigidbody)
		{
			rigidbody.freezeRotation = true;
		}
		
		originalRotation = transform.localRotation;
	}
 
	void Update ()
	{
		if (animateHeadY == true && anim != null){
			adjustedHeadFloat = transform.rotation.eulerAngles.x;
			if (adjustedHeadFloat < 270f){
				adjustedHeadFloat += 360;
			}			
			adjustedHeadFloat = (adjustedHeadFloat - 270f) / (431 - 270);
			anim.SetFloat("headAngle", adjustedHeadFloat);
		}
		if (axes == RotationAxes.MouseX)
		{			
			rotAverageX = 0f;
 
			if (playerNumber == 0){
				if (!onMac){
					rotationX += Input.GetAxis("MouseXP1") * sensitivityX * Time.timeScale;
				}else{
					rotationX += Input.GetAxis("MouseXP1MAC") * sensitivityX * Time.timeScale;
				}
			}else if (playerNumber == 1){
				if (!onMac){
					rotationX += Input.GetAxis("MouseXP2") * sensitivityX * Time.timeScale;
				}else{
					rotationX += Input.GetAxis("MouseXP2MAC") * sensitivityX * Time.timeScale;
				}
			}
 
			rotArrayX.Add(rotationX);
 
			if (rotArrayX.Count >= framesOfSmoothing)
			{
				rotArrayX.RemoveAt(0);
			}
			for(int i = 0; i < rotArrayX.Count; i++)
			{
				rotAverageX += rotArrayX[i];
			}
			rotAverageX /= rotArrayX.Count;
			rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);
 
			Quaternion xQuaternion = Quaternion.AngleAxis (rotAverageX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;			
		}
		else
		{			
			rotAverageY = 0f;
 
 			float invertFlag = 1f;
 			if( invertY )
 			{
 				invertFlag = -1f;
 			}
			
			if (playerNumber == 0){
				if (!onMac){
					rotationY += Input.GetAxis("MouseYP1") * sensitivityY * invertFlag * Time.timeScale;
				}else{
					rotationY += Input.GetAxis("MouseYP1MAC") * sensitivityY * invertFlag * Time.timeScale;
				}
			}
			else if (playerNumber == 1){
				if (!onMac){
					rotationY += Input.GetAxis("MouseYP2") * sensitivityY * invertFlag * Time.timeScale;
				}else{
					rotationY += Input.GetAxis("MouseYP2MAC") * sensitivityY * invertFlag * Time.timeScale;
				}
			}
			
			rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
 	
			rotArrayY.Add(rotationY);
 
			if (rotArrayY.Count >= framesOfSmoothing)
			{
				rotArrayY.RemoveAt(0);
			}
			for(int j = 0; j < rotArrayY.Count; j++)
			{
				rotAverageY += rotArrayY[j];
			}
			rotAverageY /= rotArrayY.Count;
 
			Quaternion yQuaternion = Quaternion.AngleAxis (rotAverageY, Vector3.left);
			transform.localRotation = originalRotation * yQuaternion;
		}
	}
	
	public void SetSensitivity(float s)
	{
		sensitivityX = s;
		sensitivityY = s;
	}
 
	public static float ClampAngle (float angle, float min, float max)
	{
		angle = angle % 360;
		if ((angle >= -360F) && (angle <= 360F)) {
			if (angle < -360F) {
				angle += 360F;
			}
			if (angle > 360F) {
				angle -= 360F;
			}			
		}
		return Mathf.Clamp (angle, min, max);
	}
}