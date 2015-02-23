using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class newPlanetStun : MonoBehaviour {

	public Color normalColor, stunColor, goneColor;
	public float maxStunTime, blinkTime;
	public Text countText, totalText;
	private float stunTimeLeft;
	bool stunned;
	newSpawnSpores spawnScript;

	// Use this for initialization
	void Start () {
		spawnScript = GetComponent<newSpawnSpores>();
	}
	
	// Update is called once per frame
	void Update () {
		if (stunTimeLeft > 0){
			stunTimeLeft--;
		}
		else if (stunned){
			stunned = false;
			spawnScript.stunned = false;
			CancelInvoke("Blink");
			ResetColor();
		}
	}
	
	public void Stun(){
		
		stunTimeLeft = maxStunTime;
		if (!stunned){
			InvokeRepeating("Blink", 0, blinkTime);
		}
		stunned = true;
		spawnScript.stunned = true;
	}
	
	void Blink(){
		print ("blink");
		if (countText.color == normalColor){
			countText.color = stunColor;
			totalText.color = stunColor;
		}
		else if (countText.color == stunColor){
			countText.color = goneColor;
			totalText.color = goneColor;
		}
		else if (countText.color == goneColor){
			countText.color = stunColor;
			totalText.color = stunColor;
		}
	}
	
	void ResetColor(){
		countText.color = normalColor;
		totalText.color = normalColor;
	}
}
