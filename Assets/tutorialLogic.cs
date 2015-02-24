using UnityEngine;
using System.Collections;

public class tutorialLogic : MonoBehaviour {

	public int playerNumber;
	public Transform playerCam;
	public Transform planet1, planet2, planet3;
	public Texture[] signTextures;
	public Transform signImage;
	public int textureIndex = 0;
	newSelectedPlanets selectedScript;
	public bool ready = false;

	// Use this for initialization
	void Start () {
		selectedScript = playerCam.GetComponent<newSelectedPlanets>();
	}
	
	void Update(){
		signImage.renderer.material.SetTexture("_MainTex", signTextures[textureIndex]);
		if (planet1.parent.GetComponent<newPlanetAlignment>().myCurrentAlignment != playerNumber 
			&& planet2.parent.GetComponent<newPlanetAlignment>().myCurrentAlignment != playerNumber 
			&& planet3.parent.GetComponent<newPlanetAlignment>().myCurrentAlignment != playerNumber){
			textureIndex = 6;
		}
		else if (selectedScript.selectedPlanetList.Count != 0 && textureIndex == 0){
			print ("more than one");
			if (selectedScript.selectedPlanetList[0] == planet1){
				NextImage();
			}
		}
		else if (textureIndex == 1 && planet2.parent.GetComponent<newPlanetAlignment>().myCurrentAlignment == playerNumber){
			NextImage();
		}
		else if (textureIndex == 2){
			int totalSpores = planet1.parent.GetComponent<newSporeCount>().currentCount + planet2.parent.GetComponent<newSporeCount>().currentCount;
			if (totalSpores > planet3.parent.GetComponent<newSporeCount>().currentCount){
				NextImage();
			}
		}
		else if (textureIndex == 3){
			if (selectedScript.doubleSelectedPlanetList.Count == 2){
				NextImage();
			}
		}
		else if (textureIndex == 4){
			if (planet3.parent.GetComponent<newPlanetAlignment>().myCurrentAlignment == playerNumber){
				NextImage();
				ready = true;
			}	
		}
	}
	
	// Update is called once per frame
	void NextImage() {
		textureIndex++;
	}
}
