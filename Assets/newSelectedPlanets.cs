using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class newSelectedPlanets : MonoBehaviour {

	public string[] planetTags;
	
	public int planetListSize;

	public float timeBetweenPlanets = 1f;

	public int myPlayerAlignment = 0;

	// public Transform[] selectedPlanetList = new Transform[planetListSize];
	// public Transform[] doubleSelectedPlanetList = new Transform[planetListSize];
	
	public List<Transform> selectedPlanetList = new List<Transform>();
	public List<Transform> doubleSelectedPlanetList = new List<Transform>();

	public int ClickedOn(Transform clickedPlanet){
		/* for (int i = 0; i<planetListSize;i++){
			if (selectedPlanetList[i] == clickedPlanet){
				selectedPlanetList[i] == null
				break;
			}
		}
	 */
	
		if (selectedPlanetList.Contains(clickedPlanet)){
			RemovePlanet(clickedPlanet, 1);
			AddPlanet(clickedPlanet, 2);
			return 2;
		}
		else if (doubleSelectedPlanetList.Contains(clickedPlanet)){
			RemovePlanet(clickedPlanet, 2);
			return 3;
		}
		{
			AddPlanet(clickedPlanet, 1);
			return 1;
		}
	}
	
	public void AddPlanet(Transform planetToAdd, int list){
		if (list == 1){
			selectedPlanetList.Add(planetToAdd);
		}
		else {
			doubleSelectedPlanetList.Add(planetToAdd);
		}
	}
	
	public void RemovePlanet(Transform planetToRemove, int list){
		//print ("removing " + planetToRemove + " from list " + list);
		if (list == 1){
				int index = selectedPlanetList.IndexOf(planetToRemove);
				selectedPlanetList.RemoveAt(index);
		}
		else if (list == 2){
				int index = doubleSelectedPlanetList.IndexOf(planetToRemove);
				doubleSelectedPlanetList.RemoveAt(index);
		}
		else if (list == 3){	
			if (selectedPlanetList.Contains(planetToRemove)){
				int index = selectedPlanetList.IndexOf(planetToRemove);
				selectedPlanetList.RemoveAt(index);
			}
			if (doubleSelectedPlanetList.Contains(planetToRemove)){
				int index = doubleSelectedPlanetList.IndexOf(planetToRemove);
				doubleSelectedPlanetList.RemoveAt(index);
			}
		}
	}
	
	public void SendPlanets (Transform destination){
		StartCoroutine("SendPlanetsCo", destination);
	}
	
	public void RemoveMe (Transform changedPlanet){
		
		if (selectedPlanetList.Contains(changedPlanet)){
			selectedPlanetList.Remove(changedPlanet);
		}
		if (doubleSelectedPlanetList.Contains(changedPlanet)){
			doubleSelectedPlanetList.Remove(changedPlanet);
		}
	}
	
	public void DeselectAll(){
		if (selectedPlanetList!=null){
			foreach (Transform planet in selectedPlanetList){
				planet.parent.GetComponent<newHighlightScript>().StopSelecting(myPlayerAlignment);
				planet.parent.GetComponent<newHighlightScript>().StopHighlighting(myPlayerAlignment);
			}
		}
		if (doubleSelectedPlanetList!=null){
			foreach (Transform planet in doubleSelectedPlanetList){
				planet.parent.GetComponent<newHighlightScript>().StopSelecting(myPlayerAlignment);
				planet.parent.GetComponent<newHighlightScript>().StopHighlighting(myPlayerAlignment);
			}
		}
		selectedPlanetList.Clear();
		doubleSelectedPlanetList.Clear();
	}
	
	IEnumerator SendPlanetsCo(Transform destination){
		destination.parent.GetComponent<newHighlightScript>().StopHighlighting(myPlayerAlignment);
			yield return new WaitForSeconds(0);
			if (selectedPlanetList!=null){
				foreach (Transform planet in selectedPlanetList){
					planet.parent.GetComponent<newHighlightScript>().StopSelecting(myPlayerAlignment);
					planet.parent.GetComponent<newHighlightScript>().StopHighlighting(myPlayerAlignment);
					planet.parent.GetComponent<newSporeLists>().SendHalf(destination);
				}
			}
			if (doubleSelectedPlanetList!=null){
				foreach (Transform planet in doubleSelectedPlanetList){
					planet.parent.GetComponent<newHighlightScript>().StopSelecting(myPlayerAlignment);
					planet.parent.GetComponent<newHighlightScript>().StopHighlighting(myPlayerAlignment);
					planet.parent.GetComponent<newSporeLists>().SendAll(destination);
				}
			}
/* 		if (selectedPlanetList!=null){
			foreach (Transform planet in selectedPlanetList){
				if (planet != null && planet.tag == planetTags[myPlayerAlignment]){
					planet.parent.GetComponent<newSporeLists>().SendHalf(destination);
					yield return new WaitForSeconds(timeBetweenPlanets);
				}
			}
		}
		if (doubleSelectedPlanetList.Count!=0){
			foreach (Transform planet in doubleSelectedPlanetList){
				if (planet != null && planet.tag == planetTags[myPlayerAlignment]){
					planet.parent.GetComponent<newSporeLists>().SendAll(destination);
					yield return new WaitForSeconds(timeBetweenPlanets);
				}
			}
		} */
		selectedPlanetList.Clear();
		doubleSelectedPlanetList.Clear();
	}
	
	public void RemoveAllPlanets(){
		
	}
}
